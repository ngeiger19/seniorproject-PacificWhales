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


        // GET INFO FROM SEARCH PAGE
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
            else if (searchOption == "option2")
            {

                return RedirectToAction("VenueSearchResults", new { venueSearch = search });
            }

            return View();

        }
        
        // VENUE SEARCH RESULTS
        public ActionResult VenueSearchResults(string venueSearch)
        {
            var venueQuery =
                from venue in db.Venues
                where venue.VenueName.Contains(venueSearch)
                select venue;

            return View(venueQuery);
        }

        // MUSICIAN SEARCH RESULTS
        public ActionResult MusicianSearchResults(string musicianSearch)
        {
            var musicianQuery =
                from musician in db.Users
                where musician.FirstName.Contains(musicianSearch)
                select musician;

            return View(musicianQuery);
        }
    }
}