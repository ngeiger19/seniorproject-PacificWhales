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

        public ActionResult Search()
        {

            string search = Request.QueryString["search"];

            if (search == null || search == "")
            {
                return View();
            }

            // search for musicians
            var searchQuery =
                from user in db.Users
                where user.FirstName.Contains(search)
                select user;

            // search for venues

            var searchQuery =
                from venue in db.Venues
                where venue.VenueName.Contains(search)
                select venue;

            return View(searchQuery);

            
        }
    }
}