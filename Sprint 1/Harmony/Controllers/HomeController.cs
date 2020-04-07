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
using Newtonsoft.Json.Linq;
using System.IO;

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
            // For state dropdown list
            StreamReader sr = new StreamReader(Server.MapPath("~/Content/states_hash.json"));
            string data = sr.ReadToEnd();
            JArray arr = JArray.Parse(data);
            List<SelectListItem> stateList = new List<SelectListItem>();
            for (int i = 0; i < arr.Count(); i++)
            {
                stateList.Add(new SelectListItem { Text = (string)arr[i]["name"], Value = (string)arr[i]["name"] });
            }
            ViewData.Clear();
            // ViewBag.State = stateList;
            ViewData["State"] = stateList;
            sr.Dispose();

            string search = Request.QueryString["search"];
            string cityFilter = Request.QueryString["cityFilter"];
            string stateFilter = Request.QueryString["stateFilter"];

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

                return RedirectToAction("VenueSearchResults", new { venueSearch = search , city = cityFilter, state = stateFilter});
            }

            return View();

        }

        // VENUE SEARCH RESULTS
        public ActionResult VenueSearchResults(string venueSearch, string city, string state)
        {
            var venueQuery =
                from venue in db.Venues
                where venue.VenueName.Contains(venueSearch)
                select venue;

            // City filter active
            if (city != null)
            {
                var cityQuery =
                from venue in venueQuery
                where venue.City == city
                select venue;

                // State and City filters active
                if (state != null)
                {
                    var stateQuery =
                        from venue in cityQuery
                        where venue.State == state
                        select venue;
                    return View(stateQuery);
                }
                return View(cityQuery);
            }
            // State filter active
            else if (state != null)
            {
                var stateQuery =
                    from venue in venueQuery
                    where venue.State == state
                    select venue;
                return View(stateQuery);
            }

            // No filters active
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