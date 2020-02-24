using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;
using Harmony.Models;
using Harmony.DAL;
using System.Net;

namespace Harmony.Controllers
{
    public class HomeController : Controller
    {

        private HarmonyContext db = new HarmonyContext();

        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Welcome()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Search(string searchOption)
        {
            string search = Request.QueryString["search"];
            
            // If nothing was typed into search bar
            if (search == null || search == "")
            {
                return View();
            }

            // search for musicians
            if (searchOption == "option1")
            {
                return RedirectToAction("MusicianSearchResults", new { musicianSearch = search });
            }

            // search for venues
            else
            {
                return RedirectToAction("VenueSearchResults", new { venueSearch = search });
            }
        }

        public ActionResult MusicianSearchResults(string musicianSearch)
        {
            var userQuery =
                from user in db.Users
                where user.FirstName.Contains(musicianSearch)
                select user;

            return View(userQuery);
        }

        public ActionResult VenueSearchResults(string venueSearch)
        {
            var venueQuery =
                from venue in db.Venues
                where venue.VenueName.Contains(venueSearch)
                select venue;

            return View(venueQuery);
        }
    }
}