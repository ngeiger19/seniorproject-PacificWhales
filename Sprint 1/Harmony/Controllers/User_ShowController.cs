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

namespace Harmony.Controllers
{
    public class User_ShowController : Controller
    {
        private HarmonyContext db = new HarmonyContext();

        // GET: User_Show
        public ActionResult Index()
        {
            var user_Show = db.User_Show.Include(u => u.Show).Include(u => u.User).Include(u => u.User1);
            return View(user_Show.ToList());
        }

        // GET: User_Show/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Show user_Show = db.User_Show.Find(id);
            if (user_Show == null)
            {
                return HttpNotFound();
            }
            return View(user_Show);
        }

        // GET: User_Show/Create
        public ActionResult CreateUserShow()
        {
            ViewBag.ShowID = new SelectList(db.Shows, "ID", "Title");
            ViewBag.MusicianID = new SelectList(db.Users, "ID", "FirstName");
            ViewBag.VenueOwnerID = new SelectList(db.Users, "ID", "FirstName");
            return View();
        }

        // POST: User_Show/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUserShow([Bind(Include = "MusicianID,VenueOwnerID,ShowID")] User_Show user_Show)
        {
            if (ModelState.IsValid)
            {
                db.User_Show.Add(user_Show);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ShowID = new SelectList(db.Shows, "ID", "Title", user_Show.ShowID);
            ViewBag.MusicianID = new SelectList(db.Users, "ID", "FirstName", user_Show.MusicianID);
            ViewBag.VenueOwnerID = new SelectList(db.Users, "ID", "FirstName", user_Show.VenueOwnerID);
            return View(user_Show);
        }

        // GET: User_Show/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Show user_Show = db.User_Show.Find(id);
            if (user_Show == null)
            {
                return HttpNotFound();
            }
            ViewBag.ShowID = new SelectList(db.Shows, "ID", "Title", user_Show.ShowID);
            ViewBag.MusicianID = new SelectList(db.Users, "ID", "FirstName", user_Show.MusicianID);
            ViewBag.VenueOwnerID = new SelectList(db.Users, "ID", "FirstName", user_Show.VenueOwnerID);
            return View(user_Show);
        }

        // POST: User_Show/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MusicianID,VenueOwnerID,ShowID")] User_Show user_Show)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user_Show).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ShowID = new SelectList(db.Shows, "ID", "Title", user_Show.ShowID);
            ViewBag.MusicianID = new SelectList(db.Users, "ID", "FirstName", user_Show.MusicianID);
            ViewBag.VenueOwnerID = new SelectList(db.Users, "ID", "FirstName", user_Show.VenueOwnerID);
            return View(user_Show);
        }

        // GET: User_Show/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Show user_Show = db.User_Show.Find(id);
            if (user_Show == null)
            {
                return HttpNotFound();
            }
            return View(user_Show);
        }

        // POST: User_Show/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User_Show user_Show = db.User_Show.Find(id);
            db.User_Show.Remove(user_Show);
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
