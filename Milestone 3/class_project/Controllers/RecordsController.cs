using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using class_project.DAL;
using class_project.Models;

namespace class_project.Controllers
{
    public class RecordsController : Controller
    {
        private ClassprojectContext db = new ClassprojectContext();

        // GET: Records
        /*public ActionResult Index()
        {
            var records = db.Records.Include(r => r.Athlete).Include(r => r.Event);
            return View(records.ToList());
        }*/

        // GET: Records/Details/5
        /*public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Record record = db.Records.Find(id);
            if (record == null)
            {
                return HttpNotFound();
            }
            return View(record);
        }*/

        // GET: Records/Create
        public ActionResult Create()
        {
            ViewBag.AthleteID = new SelectList(db.Athletes.Select(s => new { AthleteID = s.ID, FullName = s.FirstName + " " + s.LastName }).OrderBy(n => n.FullName), "AthleteID", "FullName");
            ViewBag.EventID = new SelectList(db.Events.Select(e => new { EventID = e.ID, EventInfo = e.Stroke + " " + e.Distance }), "EventID", "EventInfo");
            return View();
        }

        // POST: Records/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Location,AthleteID,EventID,RaceTime,Date")] Record record)
        {
            if (ModelState.IsValid)
            {
                db.Records.Add(record);
                db.SaveChanges();
                return RedirectToAction("Search", "Home");
            }

            ViewBag.AthleteID = new SelectList(db.Athletes.Select(s => new {AthleteID = s.ID, FullName = s.FirstName + " " + s.LastName}).OrderBy(n => n.FullName), "AthleteID", "FullName", record.AthleteID);
            ViewBag.EventID = new SelectList(db.Events.Select(e => new {EventID = e.ID, EventInfo = e.Stroke + " " + e.Distance }), "EventID", "EventInfo", record.EventID);
            return View(record);
        }

        // GET: Records/Edit/5
       /* public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Record record = db.Records.Find(id);
            if (record == null)
            {
                return HttpNotFound();
            }
            ViewBag.AthleteID = new SelectList(db.Athletes, "ID", "FirstName", record.AthleteID);
            ViewBag.EventID = new SelectList(db.Events, "ID", "Stroke", record.EventID);
            return View(record);
        }*/

        // POST: Records/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
       /* [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Location,AthleteID,EventID,RaceTime,Date")] Record record)
        {
            if (ModelState.IsValid)
            {
                db.Entry(record).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AthleteID = new SelectList(db.Athletes, "ID", "FirstName", record.AthleteID);
            ViewBag.EventID = new SelectList(db.Events, "ID", "Stroke", record.EventID);
            return View(record);
        }*/

        // GET: Records/Delete/5
       /* public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Record record = db.Records.Find(id);
            if (record == null)
            {
                return HttpNotFound();
            }
            return View(record);
        }*/

        // POST: Records/Delete/5
        /*[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Record record = db.Records.Find(id);
            db.Records.Remove(record);
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
        }*/
    }
}
