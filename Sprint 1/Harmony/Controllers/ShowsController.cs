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
using Microsoft.AspNet.Identity;

namespace Harmony.Controllers
{
    public class ShowsController : Controller
    {
        private HarmonyContext db = new HarmonyContext();

        // GET: Shows
        public ActionResult MyShows()
        {
            var identityID = User.Identity.GetUserId();
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
        public ActionResult RateUser(User_Show show)
        {
            ShowsViewModel viewModel = new ShowsViewModel(show);
            return View(viewModel);
        }

        public ActionResult RateUser(User_Show show, string rating)
        {
            // Converting string into int
            int numStars = getRating(rating);

            Models.Rating userRating = new Models.Rating
            {
                UserID = show.MusicianID,
                Value = numStars
            };

            show.MusicianRated = 1;

            db.Ratings.Add(userRating);
            db.SaveChanges();

            ShowsViewModel viewModel = new ShowsViewModel(show);

            return View(viewModel);
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
