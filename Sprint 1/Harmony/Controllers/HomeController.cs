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

                foreach(Genre x in db.Genres)
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

        // Gets popular musicians or venues based on the
        // number of shows they've play and their
        // average rating
        public IEnumerable<User> GetReccs(string role)
        {
            // Getting users
            IEnumerable<User> users =
                from u in db.Users
                select u;

            // Assigning point value to each user
            // based on their rating and shows played
            List<Tuple<User, float?>> usersPoints = new List<Tuple<User, float?>>();
            int numShows = 0;
            foreach (var u in users)
            {
                
                if (role == "VenueOwner")
                {
                    numShows =
                        (from s in db.User_Show
                        where s.MusicianID == u.ID
                        select s).Count();
                }
                else if (role == "Musician")
                {
                    numShows =
                        (from s in db.User_Show
                         where s.VenueOwnerID == u.ID
                         select s).Count();
                }
                
                float? numPoints = u.AveRating * numShows + numShows;
                usersPoints.Add(Tuple.Create(u, numPoints));
            }

            // Sorting users by number of points
            IEnumerable<User> result = Enumerable.Empty<User>();
            User selected = new User();
            if (usersPoints.Count() > 0)
            {
                for (int i = 0; i < usersPoints.Count() - 1; i++)
                {
                    selected = usersPoints[i].Item1;
                    for (int j = i + 1; j < usersPoints.Count(); i++)
                    {
                        if (usersPoints[j].Item2 > usersPoints[i].Item2)
                        {
                            selected = usersPoints[j].Item1;
                        }
                    }
                    if (result.Count() < 10)
                    {
                        result.Append(selected);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (result.Count() > 10)
            {
                result.Take(10);
            }
            return result;
        }


        public ActionResult Index()
        {
            IEnumerable<User> reccs = Enumerable.Empty<User>();
            if (User.IsInRole("VenueOwner"))
            {
                reccs = GetReccs("VenueOwner");
            }
            else if (User.IsInRole("Musician")) 
            {
                reccs = GetReccs("Musician");
            }
            return View(reccs);
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
                return RedirectToAction("VenueSearchResults", new { venueSearch = search, city = cityFilter, state = stateFilter});
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