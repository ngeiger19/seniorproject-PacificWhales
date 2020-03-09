﻿using System;
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
    [Authorize]
    public class UsersController : Controller
    {
        private HarmonyContext db = new HarmonyContext();

        // GET: Users
        public ActionResult MusicianIndex()
        {
            // return View(db.Users.ToList());
            return View(db.Users.Where(u => u.Genres.Count() != 0).ToList());
        }

        // GET: Users/Details/5
        public ActionResult MusicianDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            MusicianDetailViewModel viewModel = new MusicianDetailViewModel(user);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        public ActionResult CreateShow()
        {
            var IdentityID = User.Identity.GetUserId();
            ViewBag.VenueID = new SelectList(db.Venues.Where(v => v.User.ASPNetIdentityID == IdentityID).Select(v => new { VenueID = v.ID, VenueName = v.VenueName }), "VenueID", "VenueName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateShow([Bind(Include = "ShowID,Date,VenueID,ShowDescription,DateBooked")] Show show)
        {
            
            if (ModelState.IsValid)
            {
                show.DateBooked = DateTime.Now;
                db.Shows.Add(show);
                db.SaveChanges();
                return View();
            }
            var IdentityID = User.Identity.GetUserId();
            ViewBag.VenueID = new SelectList(db.Venues.Where(v => v.User.ASPNetIdentityID == IdentityID).Select(v => new {VenueID = v.ID, VenueName = v.VenueName }), "VenueID", "VenueName", show.VenueID);
            
            return View(show);
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
