using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
using System.Threading.Tasks;

namespace Harmony
{
    public class VenuesController : Controller
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
        
        // GET: Venues
        public ActionResult Index()
        {
            var venues = db.Venues.Include(v => v.User).Include(v => v.VenueType);
            return View(venues.ToList());
        }

        /************************************
         *           VENUE PROFILE
         * *********************************/
        // GET: Venues/Details/5
        public ActionResult Details(int? id)
        {
            // If no user is passed through
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venue venue = db.Venues.Find(id);
            // If user doesn't exisit
            if (venue == null)
            {
                return HttpNotFound();
            }

            var identityID = User.Identity.GetUserId();

            VenueOwnerDetailViewModel viewModel = new VenueOwnerDetailViewModel(venue);
            viewModel.UpcomingShows = db.User_Show.Where(u => u.VenueOwnerID == venue.UserID).Select(s => s.Show).Where(s => s.StartDateTime > DateTime.Now && s.Status == "Accepted").OrderByDescending(s => s.EndDateTime).ToList();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PerformanceRequest(int? id, VenueOwnerDetailViewModel viewModel)
        {
            // No user id passed through
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Venue venue = db.Venues.Find(id);

            // If users doesn't exisit
            if (venue == null)
            {
                return HttpNotFound();
            }

            // Viewmodel for VenueOwner
            VenueOwnerDetailViewModel model = new VenueOwnerDetailViewModel(venue);

            var IdentityID = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                // Get user's calendar credentials
                UserCredential credential = await GetCredentialForApiAsync();
                // Create Google Calendar API service.
                var service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Harmony",
                });

                // Fetch the list of calendars.
                var calendars = await service.CalendarList.List().ExecuteAsync();
                // create a new event to google calendar
                if (calendars != null)
                {
                    Event newEvent = new Event()
                    {
                        Id = Guid.NewGuid().ToString().Replace('-', '0'),
                        Summary = viewModel.Title,
                        Description = viewModel.ShowDescription,
                        Location = model.VenueName,
                        Start = new EventDateTime()
                        {
                            DateTime = viewModel.StartDateTime.AddHours(7.0),
                            TimeZone = "America/Los_Angeles"
                        },
                        End = new EventDateTime()
                        {
                            DateTime = viewModel.EndDateTime.AddHours(7.0),
                            TimeZone = "America/Los_Angeles"
                        },
                        Attendees = new List<EventAttendee>()
                        {
                            new EventAttendee(){Email = model.OwnerEmail}
                        }
                    };
                    var newEventRequest = service.Events.Insert(newEvent, "primary");
                    // This allows attendees to get email notification
                    newEventRequest.SendNotifications = true;
                    var eventResult = newEventRequest.Execute();

                    // add the new show to db
                    Show newShow = new Show
                    {
                        Title = viewModel.Title,
                        StartDateTime = viewModel.StartDateTime,
                        EndDateTime = viewModel.EndDateTime,
                        Description = viewModel.ShowDescription,
                        DateBooked = newEvent.Created ?? DateTime.Now,
                        VenueID = model.ID,
                        Status = "Pending",
                        GoogleEventID = newEvent.Id
                    };
                    db.Shows.Add(newShow);
                    User_Show user_Show = new User_Show
                    {
                        MusicianID = db.Users.Where(u => u.ASPNetIdentityID == IdentityID).First().ID,
                        VenueOwnerID = model.UserID,
                        ShowID = newShow.ID,
                        MusicianRated = false,
                        VenueRated = false
                    };
                    db.User_Show.Add(user_Show);
                    db.SaveChanges();

                }

                return RedirectToAction("Details", new { id = model.ID});
            }

            return View(model);
        }

        // GET: Venues/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "ID", "FirstName");
            ViewBag.VenueTypeID = new SelectList(db.VenueTypes, "ID", "TypeName");
            return View();
        }

        // POST: Venues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,VenueName,AddressLine1,AddressLine2,City,State,ZipCode,VenueTypeID,UserID")] Venue venue)
        {
            if (ModelState.IsValid)
            {
                db.Venues.Add(venue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "ID", "FirstName", venue.UserID);
            ViewBag.VenueTypeID = new SelectList(db.VenueTypes, "ID", "TypeName", venue.VenueTypeID);
            return View(venue);
        }

        // GET: Venues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venue venue = db.Venues.Find(id);
            if (venue == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "ID", "FirstName", venue.UserID);
            ViewBag.VenueTypeID = new SelectList(db.VenueTypes, "ID", "TypeName", venue.VenueTypeID);
            return View(venue);
        }

        // POST: Venues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,VenueName,AddressLine1,AddressLine2,City,State,ZipCode,VenueTypeID,UserID")] Venue venue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "ID", "FirstName", venue.UserID);
            ViewBag.VenueTypeID = new SelectList(db.VenueTypes, "ID", "TypeName", venue.VenueTypeID);
            return View(venue);
        }

        // GET: Venues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venue venue = db.Venues.Find(id);
            if (venue == null)
            {
                return HttpNotFound();
            }
            return View(venue);
        }

        // POST: Venues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Venue venue = db.Venues.Find(id);
            db.Venues.Remove(venue);
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
