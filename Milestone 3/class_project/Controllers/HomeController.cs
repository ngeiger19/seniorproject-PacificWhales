using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Net;
using class_project.DAL;
using class_project.Models;

namespace class_project.Controllers
{
    public class HomeController : Controller
    {
        private ClassprojectContext db = new ClassprojectContext();

        public ActionResult Search(string q)
        {
            return View(db.Athletes.Where(s => s.FirstName.Contains(q) || s.LastName.Contains(q)).ToList());
        }
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
    }
}