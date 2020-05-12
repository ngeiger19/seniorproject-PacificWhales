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
using Microsoft.AspNet.Identity;

namespace Harmony.Controllers
{
    public class HomeController : Controller
    {

        private HarmonyContext db = new HarmonyContext();

        // Queries for Venue FIlters
        /* public IQueryable<Venue> VenueCityQuery(IQueryable<Venue> venues, string city)
        {
            if (city != null && city != "")
            {
                var cityQuery =
                    from venue in venues
                    where venue.City.Contains(city)
                    select venue;
                ViewBag.City = city;
                return cityQuery;
            }
            ViewBag.City = null;
            return venues;
        }
        public IQueryable<Venue> StateQuery(IQueryable<Venue> venues, string state)
        {
            if (state != null && state != "")
            {
                var stateQuery =
                    from venue in venues
                    where venue.State.Contains(state)
                    select venue;
                ViewBag.State = state;
                return stateQuery;
            }
            ViewBag.State = null;
            return venues;
        } */

        // Queries for Musician Filters
        public IEnumerable<User> CityQuery(IEnumerable<User> users, string city)
        {
            if (city != null && city != "")
            {
                IEnumerable<User> cityQuery =
                    from user in users
                    where user.City.Contains(city)
                    select user;

                ViewBag.City = city;
                return cityQuery;
            }

            ViewBag.City = null;
            return users;
        }

        public IEnumerable<User> StateQuery(IEnumerable<User> users, string state)
        {
            if (state != null && state != "")
            {
                IEnumerable<User> stateQuery =
                    from user in users
                    where user.State.Contains(state)
                    select user;

                ViewBag.State = state;
                return stateQuery;
            }

            ViewBag.State = null;
            return users;
        }

        public IEnumerable<User> GenreQuery(IEnumerable<User> users, string genre)
        {
            if (genre != null && genre != "")
            {
                Genre g = new Genre();

                foreach (Genre x in db.Genres)
                {
                    g = x;
                }

                var genreQuery =
                    from musician in users
                    where musician.Genres.Contains(g)
                    select musician;

                ViewBag.Genre = genre;
                return genreQuery;
            }

            ViewBag.Genre = null;
            return users;
        }

        // Determines a user's role
        public bool IsVenueOwner(User user)
        {
            Venue venue =
                (from v in db.Venues
                 where v.UserID == user.ID
                 select v).FirstOrDefault();

            if (venue != null)
            {
                return true;
            }

            return false;
        }

        // Assigns points to users based on
        // how many shows played and average rating
        public double GetPoints(User user, string role)
        {
            int numShows = 0;
            if (role == "VenueOwner")
            {
                numShows =
                    (from s in db.User_Show
                     where s.MusicianID == user.ID
                     select s).Count();
                // Leave out if user has same role as current user
                if (IsVenueOwner(user))
                {
                    return -1.0;
                }
            }
            else if (role == "Musician")
            {
                numShows =
                    (from s in db.User_Show
                     where s.VenueOwnerID == user.ID
                     select s).Count();
                // Leave out if user has same role as current user
                if (!IsVenueOwner(user))
                {
                    return -1.0;
                }
            }

            double numPoints = user.AveRating * numShows + numShows;

            return (numPoints);
        }

        // Gets popular musicians or venues based on the
        // number of shows they've play and their
        // average rating
        public IEnumerable<User> GetReccs(string role, User currentUser)
        {
            // Number of recommendations
            int numReccs = 10;

            // Getting users in current user's area
            IEnumerable<User> users =
                from u in db.Users
                where u.State == currentUser.State && u.ID != currentUser.ID && u.AveRating >= 3
                select u;

            // Filter out users in same role as current user
            // and sort by number of points
            IEnumerable<User> userPoints = users.Where(u => GetPoints(u, role) > -1.0).
                OrderBy(u => GetPoints(u, role));

            // Return top users
            return userPoints.Take(numReccs);
        }


        public ActionResult Index()
        {
            string userid = User.Identity.GetUserId();
            IEnumerable<User> emptyReccs = Enumerable.Empty<User>();

            // Get top users for venue owners
            if (User.IsInRole("VenueOwner"))
            {
                IEnumerable<User> reccs = GetReccs("VenueOwner", db.Users.Where(u => u.ASPNetIdentityID == userid).First());
                return View(reccs);
            }
            // Get top users for musicians
            else if (User.IsInRole("Musician"))
            {
                IEnumerable<User> reccs = GetReccs("Musician", db.Users.Where(u => u.ASPNetIdentityID == userid).First());
                return View(reccs);
            }
            return View(emptyReccs);
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

            string cityFilter = Request.QueryString["cityFilter"];
            string stateFilter = Request.QueryString["stateFilter"];
            string genreFilter = Request.QueryString["genreFilter"];

            // search for musicians
            if (searchOption == "option1")
            {
                return RedirectToAction("MusicianSearchResults", new { musicianSearch = search, city = cityFilter, state = stateFilter, genre = genreFilter });
            }
            else if (searchOption == "option2")
            {
                return RedirectToAction("VenueSearchResults", new { venueSearch = search, city = cityFilter, state = stateFilter });
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

                ViewBag.City = city;
                // State and City filters active
                if (state != null)
                {
                    var stateQuery =
                        from venue in cityQuery
                        where venue.State == state
                        select venue;

                    ViewBag.State = state;
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

                ViewBag.State = state;
                return View(stateQuery);
            }

            // No filters active
            ViewData.Clear();
            return View(venueQuery);
        }

        // MUSICIAN SEARCH RESULTS
        public ActionResult MusicianSearchResults(string musicianSearch, string city, string state, string genre)
        {
            IEnumerable<User> musicians =
                from musician in db.Users
                where musician.FirstName.Contains(musicianSearch)
                select musician;

            musicians = CityQuery(musicians, city);
            musicians = StateQuery(musicians, state);
            musicians = GenreQuery(musicians, genre);

            return View(musicians);
        }
    }
}