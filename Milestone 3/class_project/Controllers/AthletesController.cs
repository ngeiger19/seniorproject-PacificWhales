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
using class_project.Models.ViewModels;

namespace class_project.Controllers
{
    public class AthletesController : Controller
    {
        private ClassprojectContext db = new ClassprojectContext();

        // GET: Athletes
        
        public ActionResult Index()
        {
            var athletes = db.Athletes.Include(a => a.Coach).Include(a => a.Team);
            return View(athletes.ToList());
        }
        

        // GET: Athletes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Athlete athlete = db.Athletes.Find(id);
            if (athlete == null)
            {
                return HttpNotFound();
            }
            AthleteDetailsViewModel viewModel = new AthleteDetailsViewModel(athlete);
            return View(viewModel);//passing the viewModel here
        }
 

        // GET: Athletes/Create
        public ActionResult Create()
        {
            ViewBag.CoachID = new SelectList(db.Coaches, "ID", "CoachName");
            ViewBag.TeamID = new SelectList(db.Teams, "ID", "TeamName");
            return View();
        }

        // POST: Athletes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,CoachID,TeamID")] Athlete athlete)
        {
            if (ModelState.IsValid)
            {
                db.Athletes.Add(athlete);
                db.SaveChanges();
                return RedirectToAction("Details", athlete);
            }

            ViewBag.CoachID = new SelectList(db.Coaches, "ID", "CoachName", athlete.CoachID);
            ViewBag.TeamID = new SelectList(db.Teams, "ID", "TeamName", athlete.TeamID);
            return View(athlete);
        }

        // GET: Athletes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Athlete athlete = db.Athletes.Find(id);
            if (athlete == null)
            {
                return HttpNotFound();
            }
            ViewBag.CoachID = new SelectList(db.Coaches, "ID", "CoachName", athlete.CoachID);
            ViewBag.TeamID = new SelectList(db.Teams, "ID", "TeamName", athlete.TeamID);
            return View(athlete);
        }

        // POST: Athletes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,CoachID,TeamID")] Athlete athlete)
        {
            if (ModelState.IsValid)
            {
                db.Entry(athlete).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", athlete);
            }
            ViewBag.CoachID = new SelectList(db.Coaches, "ID", "CoachName", athlete.CoachID);
            ViewBag.TeamID = new SelectList(db.Teams, "ID", "TeamName", athlete.TeamID);
            return View(athlete);
        }

        // GET: Athletes/Delete/5
        /*
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Athlete athlete = db.Athletes.Find(id);
            if (athlete == null)
            {
                return HttpNotFound();
            }
            return View(athlete);
        }

        // POST: Athletes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Athlete athlete = db.Athletes.Find(id);
            db.Athletes.Remove(athlete);
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
        } */
    }
}
