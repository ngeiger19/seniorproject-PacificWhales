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
    public class BandMembersController : Controller
    {
        private HarmonyContext db = new HarmonyContext();

        // GET: BandMembers
        public ActionResult Index()
        {
            var bandMembers = db.BandMembers.Include(b => b.User);
            return View(bandMembers.ToList());
        }

        // GET: BandMembers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BandMember bandMember = db.BandMembers.Find(id);
            if (bandMember == null)
            {
                return HttpNotFound();
            }
            return View(bandMember);
        }

        // GET: BandMembers/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "ID", "FirstName");
            return View();
        }

        // POST: BandMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,BandMemberName,UserID")] BandMember bandMember)
        {
            if (ModelState.IsValid)
            {
                db.BandMembers.Add(bandMember);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "ID", "FirstName", bandMember.UserID);
            return View(bandMember);
        }

        // GET: BandMembers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BandMember bandMember = db.BandMembers.Find(id);
            if (bandMember == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "ID", "FirstName", bandMember.UserID);
            return View(bandMember);
        }

        // POST: BandMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BandMemberName,UserID")] BandMember bandMember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bandMember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "ID", "FirstName", bandMember.UserID);
            return View(bandMember);
        }

        // GET: BandMembers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BandMember bandMember = db.BandMembers.Find(id);
            if (bandMember == null)
            {
                return HttpNotFound();
            }
            return View(bandMember);
        }

        // POST: BandMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BandMember bandMember = db.BandMembers.Find(id);
            db.BandMembers.Remove(bandMember);
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
