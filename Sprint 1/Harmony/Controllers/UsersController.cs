using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Harmony.DAL;
using Harmony.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Calendar.ASP.NET.MVC5;
using System.IO;
using Google.GData.Extensions;
using Calendar.ASP.NET.MVC5.Models;

namespace Harmony
{
    [Authorize]
    public class UsersController : Controller
    {

        private HarmonyContext db = new HarmonyContext();

        private readonly IDataStore dataStore = new FileDataStore(GoogleWebAuthorizationBroker.Folder);

        // Get user's Google Calendar info
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
            return new UserCredential(flow, userId, token);
        }

        // GET: Users
        public ActionResult MusicianIndex()
        {
            // return View(db.Users.ToList());
            return View(db.Users.Where(u => u.Genres.Count() != 0).ToList());
        }

        /*******************************************
         *          MUSICIAN PROFILE
         *  *************************************/
        // GET: Users/Details/5
        public async Task<ActionResult> MusicianDetails(int? id)
        {
            // No user id passed through
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Viewmodel for Musician
            User user = db.Users.Find(id);

            // If users doesn't exisit
            if (user == null)
            {
                return HttpNotFound();
            }

            MusicianDetailViewModel viewModel = new MusicianDetailViewModel(user);

            // Get user's calendar credentials
            const int MaxEventsPerCalendar = 20;
            const int MaxEventsOverall = 40;

            var credential = await GetCredentialForApiAsync();

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
            viewModel.UpcomingEvents = eventGroups;

            return View(viewModel);
        }

        public ActionResult CreateShow()
        {
            var IdentityID = User.Identity.GetUserId();
            List<Venue> venues = db.Venues.ToList();
            List<SelectListItem> venueList = new List<SelectListItem>();
            for (int i = 0; i < venues.Count(); i++)
            {
                venueList.Add(new SelectListItem { Text = venues[i].VenueName, Value = venues[i].ID.ToString() });
            }
            ViewBag.VenueID = new SelectList(db.Venues, "ID", "VenueName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateShow(MusicianDetailViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                var credential = await GetCredentialForApiAsync();

                var initializer = new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "ASP.NET MVC5 Calendar Sample",
                };
                var service = new CalendarService(initializer);

                var calendars = await service.CalendarList.List().ExecuteAsync();

                // add the new show to db
                Show newShow = new Show
                {
                    ID = model.ShowID,
                    Date = model.DateTime,
                    Description = model.ShowDescription,
                    DateBooked = DateTime.Now,
                    VenueID = model.VenueID
                };
                db.Shows.Add(newShow);

                // create a new event to google calendar
                if (calendars != null)
                {
                    Event newEvent = new Event()
                    {
                        Summary = newShow.Description,
                        Location = "VenueName: " + newShow.Venue.VenueName + " Address: " + newShow.Venue.AddressLine1 + " " + newShow.Venue.AddressLine2 + " " + newShow.Venue.City + ", " + newShow.Venue.State + " " + newShow.Venue.ZipCode,
                        Start = new EventDateTime()
                        {
                            DateTime = newShow.Date
                        },
                        End = new EventDateTime()
                        {
                            DateTime = newShow.Date.Value.AddHours(1.0)
                        },
                        Attendees = new List<EventAttendee>()
                        {
                            new EventAttendee(){Email = model.Email}
                        }

                    };
                    var newEventRequest = service.Events.Insert(newEvent, calendars.Items.First().Id);
                    // This allow attendees to get email notification
                    newEventRequest.SendNotifications = true;
                    var eventResult = newEventRequest.ExecuteAsync();
                }
                await db.SaveChangesAsync();
                return RedirectToAction("Welcome", "Home");
            }
            var IdentityID = User.Identity.GetUserId();
            List<Venue> venues = db.Venues.ToList();
            for(int i = 0; i < venues.Count(); i++)
            {
                model.VenueList.Add(new SelectListItem { Text = venues[i].VenueName, Value = venues[i].ID.ToString() });
            }
            ViewBag.VenueID = new SelectList(db.Venues, "ID", "VenueName");
            return View(model);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,City,State,Email,Description,ASPNetIdentityID")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,City,State,Email,Description,ASPNetIdentityID")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
