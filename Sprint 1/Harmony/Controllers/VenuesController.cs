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
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Calendar.ASP.NET.MVC5.Models;
using System.Threading.Tasks;

namespace Calendar.ASP.NET.MVC5
{
    public class VenuesController : Controller
    {
        private HarmonyContext db = new HarmonyContext();

        
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
            VenueOwnerDetailViewModel viewmodel = new VenueOwnerDetailViewModel(venue);
            // If user doesn't exisit
            if (venue == null)
            {
                return HttpNotFound();
            }

            VenueOwnerDetailViewModel viewModel = new VenueOwnerDetailViewModel(venue);

            return View(viewModel);
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
