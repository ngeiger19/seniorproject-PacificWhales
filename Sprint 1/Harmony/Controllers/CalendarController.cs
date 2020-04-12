using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Calendar.ASP.NET.MVC5.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using Harmony.Models;
using Harmony.DAL;

/** Just here to test the API -
 * Should allow us to see all the user's events **/

namespace Calendar.ASP.NET.MVC5.Controllers
{
    [Authorize]
    public class CalendarController : Controller
    {
        private readonly IDataStore dataStore = new FileDataStore(GoogleWebAuthorizationBroker.Folder);

        private HarmonyContext db = new HarmonyContext();

        private async Task<UserCredential> GetCredentialForApiAsync()
        {
            var initializer = new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = MyClientSecrets.ClientId,
                    ClientSecret = MyClientSecrets.ClientSecret,
                },
                Scopes = MyRequestedScopes.Scopes,
            };
            var flow = new GoogleAuthorizationCodeFlow(initializer);

            var identity = await HttpContext.GetOwinContext().Authentication.GetExternalIdentityAsync(
                DefaultAuthenticationTypes.ApplicationCookie);
            var userId = identity.FindFirstValue(MyClaimTypes.GoogleUserId);

            var token = await dataStore.GetAsync<TokenResponse>(userId);
            // await dataStore.ClearAsync();
            return new UserCredential(flow, userId, token);
        }

        // GET: /Calendar/UpcomingEvents
        public async Task<ActionResult> Schedule()
        {
            // Get user's calendar credentials
            const int MaxEventsPerCalendar = 20;
            const int MaxEventsOverall = 40;

            var credential = await GetCredentialForApiAsync();

            UpcomingEventsViewModel viewModel = new UpcomingEventsViewModel();

            var initializer = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "ASP.NET MVC5 Calendar Sample",
            };
            var service = new CalendarService(initializer);

            // Fetch the list of calendars.
            var calendars = await service.CalendarList.List().ExecuteAsync();

            // Fetch some events from each calendar.
            var fetchTasks = new List<Task<Google.Apis.Calendar.v3.Data.Events>>(calendars.Items.Count);
            foreach (var calendar in calendars.Items)
            {
                var request = service.Events.List(calendar.Id);
                request.MaxResults = MaxEventsPerCalendar;
                request.SingleEvents = true;
                request.TimeMin = DateTime.Now;
                fetchTasks.Add(request.ExecuteAsync());
            }
            var fetchResults = await Task.WhenAll(fetchTasks);

            // Sort the events and put them in the model.
            var upcomingEvents = from result in fetchResults
                                 from evt in result.Items
                                 where evt.Start != null
                                 let date = evt.Start.DateTime.HasValue ?
                                     evt.Start.DateTime.Value.Date :
                                     DateTime.ParseExact(evt.Start.Date, "yyyy-MM-dd", null)
                                 let sortKey = evt.Start.DateTimeRaw ?? evt.Start.Date
                                 orderby sortKey
                                 select new { evt, date };
            var eventsByDate = from result in upcomingEvents.Take(MaxEventsOverall)
                               group result.evt by result.date into g
                               orderby g.Key
                               select g;

            // Days in the next week
            int thisWeek = DateTime.Now.DayOfYear + 7;
            var eventGroups = new List<CalendarEventGroup>();
            foreach (var grouping in eventsByDate)
            {
                // Adding event to model if they are scheduled for the next week
                if (grouping.Key.DayOfYear <= thisWeek)
                {
                    eventGroups.Add(new CalendarEventGroup
                    {
                        GroupTitle = grouping.Key.ToLongDateString(),
                        Events = grouping,
                    });
                }
            }
            viewModel.EventGroups = eventGroups;
            return View(viewModel);
        } 
        public async Task<ActionResult> CreateShow()
        {
            // Get user's calendar credentials

            UserCredential credential = await GetCredentialForApiAsync();
            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Harmony",
            });

            // Define parameters of request.
            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // Fetch the list of calendars.
            var calendars = await service.CalendarList.List().ExecuteAsync();

            // create a new event to google calendar
            if (calendars != null)
            {
                Event newEvent = new Event()
                {
                    Summary = "Something",
                    Location = "Somewhere",
                    Start = new EventDateTime()
                    {
                        DateTime = DateTime.Now
                    },
                    End = new EventDateTime()
                    {
                        DateTime = DateTime.Now.AddHours(1.0)
                    },
                    Attendees = new List<EventAttendee>()
                    {
                        new EventAttendee(){Email = "lawyunho@gmail.com"}
                    }

                };
                var newEventRequest = service.Events.Insert(newEvent, calendars.Items.First().Id);
                newEventRequest.SendNotifications = true; // This allow attendees to get email notification
                var eventResult = newEventRequest.ExecuteAsync();
            }

            return RedirectToAction("Welcome", "Home");
        }
    }

   

    /*public async Task<ActionResult> CreateShow()
    {

    }*/
}