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

namespace Harmony.Controllers
{
    public class ShowsController : Controller
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

        // GET: Shows
        public ActionResult MyShows()
        {
            var identityID = User.Identity.GetUserId();
            List<Show> FinishedShows = db.Shows.Where(s => (s.EndDateTime < DateTime.Now) && (s.Status == "Accepted" || s.Status == "Pending")).ToList();
            foreach(var finishedshow in FinishedShows)
            {
                finishedshow.Status = "Finished";
            }
            db.SaveChanges();
            if (User.IsInRole("Musician"))
            {
                User musician = db.Users.Where(u => u.ASPNetIdentityID == identityID).FirstOrDefault();
                return View(db.User_Show.Where(u => u.MusicianID == musician.ID).Select(s => s.Show).OrderByDescending(s => s.EndDateTime).ToList());
            }
            if (User.IsInRole("VenueOwner"))
            {
                User venueOwner = db.Users.Where(u => u.ASPNetIdentityID == identityID).FirstOrDefault();
                return View(db.User_Show.Where(u => u.VenueOwnerID == venueOwner.ID).Select(s => s.Show).OrderByDescending(s => s.EndDateTime).ToList());
            }
            
            return View(db.Shows.ToList());
        }

        // GET: Shows/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Show show = db.Shows.Find(id);
            if (show == null)
            {
                return HttpNotFound();
            }
            User_Show user_Show = db.User_Show.Where(u => u.ShowID == id).First();
            ShowsViewModel viewModel = new ShowsViewModel(user_Show);
            
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Accept(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Show show = db.Shows.Find(id);
            if (show == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                show.Status = "Accepted";
                db.SaveChanges();
                return RedirectToAction("MyShows");

            }
            return RedirectToAction("MyShows");
        }

        [HttpPost]
        public async Task<ActionResult> Decline(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Show show = db.Shows.Find(id);
            if (show == null)
            {
                return HttpNotFound();
            }

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
                    show.Status = "Declined";
                    db.SaveChanges();

                    var DeleteRequest = service.Events.Delete("primary", show.GoogleEventID);
                    // This allows attendees to get email notification
                    DeleteRequest.SendNotifications = true;
                    DeleteRequest.SendUpdates = 0;
                    var eventResult = DeleteRequest.ExecuteAsync();

                    
                }
                return RedirectToAction("MyShows");
                
            }
            return RedirectToAction("MyShows");
        }

        /*********************************
         *          RATE SHOWS
         * ******************************/
        public int getRating(string ratingStr)
        {
            if (ratingStr == "star1")
            {
                return 1;
            }
            else if (ratingStr == "star2")
            {
                return 2;
            }
            else if (ratingStr == "star3")
            {
                return 3;
            }
            else if (ratingStr == "star4")
            {
                return 4;
            }
            else
            {
                return 5;
            }
        }
        public ActionResult RateUser(int? id)
        {
            User_Show show = db.User_Show.Where(s => s.ShowID == id).FirstOrDefault();
            ShowsViewModel viewModel = new ShowsViewModel(show);
            return View(viewModel);
        }

        public double CalcAveRating(int userId, int numStars)
        {
            User user = db.Users.Where(u => u.ID == userId).FirstOrDefault();

            var ratings = from r in db.Ratings
                          where r.UserID == user.ID
                          select r;

            double aveRating = 0;
            double numRatings = 1;
            double totalRating = numStars;

            foreach (var r in ratings)
            {
                numRatings++;
                totalRating += r.Value;
            }

            aveRating = totalRating / numRatings;

            aveRating = Math.Round(aveRating, 2);

            return aveRating;
        }

        [HttpPost]
        public ActionResult RateUser(int? id, ShowsViewModel model)
        {
            User_Show show = db.User_Show.Where(s => s.ShowID == id).FirstOrDefault();
            ShowsViewModel viewModel = new ShowsViewModel(show);
            // Converting string into int
            int numStars = getRating(model.RatingValue);

            Models.Rating userRating = new Models.Rating();

            if (User.IsInRole("VenueOwner"))
            {
                User user = db.Users.Where(u => u.ID == viewModel.MusicianID).FirstOrDefault();
                userRating.UserID = viewModel.MusicianID;
                userRating.Value = numStars;
                show.VenueRated = true;
                user.AveRating = CalcAveRating(user.ID, numStars);
            }
            else if (User.IsInRole("Musician"))
            {
                User user = db.Users.Where(u => u.ID == viewModel.VenueID).FirstOrDefault();
                userRating.UserID = viewModel.VenueID;
                userRating.Value = numStars;
                show.MusicianRated = true;
                user.AveRating = CalcAveRating(user.ID, numStars);
            }

            db.Ratings.Add(userRating);
            db.SaveChanges();

            return RedirectToAction("MyShows");
        }

        // GET: Shows/Create
        public ActionResult Create()
        {
            ViewBag.VenueID = new SelectList(db.Venues, "ID", "VenueName");
            return View();
        }

        // POST: Shows/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,StartDateTime,EndDateTime,VenueID,Description,DateBooked")] Show show)
        {
            if (ModelState.IsValid)
            {
                db.Shows.Add(show);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VenueID = new SelectList(db.Venues, "ID", "VenueName", show.VenueID);
            return View(show);
        }

        // GET: Shows/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Show show = db.Shows.Find(id);
            if (show == null)
            {
                return HttpNotFound();
            }
            ViewBag.VenueID = new SelectList(db.Venues, "ID", "VenueName", show.VenueID);
            return View(show);
        }

        // POST: Shows/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,StartDateTime,EndDateTime,VenueID,Description,DateBooked")] Show show)
        {
            if (ModelState.IsValid)
            {
                db.Entry(show).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VenueID = new SelectList(db.Venues, "ID", "VenueName", show.VenueID);
            return View(show);
        }

        // GET: Shows/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Show show = db.Shows.Find(id);
            if (show == null)
            {
                return HttpNotFound();
            }
            return View(show);
        }

        // POST: Shows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Show show = db.Shows.Find(id);
            db.Shows.Remove(show);
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
