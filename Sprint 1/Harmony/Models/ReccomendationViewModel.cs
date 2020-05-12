using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Harmony.Models;
using Calendar.ASP.NET.MVC5.Models;
using System.Web.Mvc;
using Harmony.DAL;

namespace Harmony.Models
{
    public class VenueReccViewModel
    {
        public VenueReccViewModel() { }

        public VenueReccViewModel(Venue venue)
        {
            VenueName = venue.VenueName;
            Rating = venue.User.AveRating;
            City = venue.City;
            State = venue.State;
            ShowsBooked = venue.Shows.Count();
        }

        [StringLength(120)]
        [Display(Name = "Venue Name")]
        public string VenueName { get; set; }
        public float? Rating { get; set; }

        [StringLength(20)]
        public string City { get; set; }

        [StringLength(20)]
        public string State { get; set; }

        [Display(Name = "Shows Booked")]
        public int ShowsBooked { get; set; }
    }

    public class MusicianReccViewModel
    {
        HarmonyContext db = new HarmonyContext();
        public MusicianReccViewModel() { }
        public MusicianReccViewModel(User user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Rating = user.AveRating;
            City = user.City;
            State = user.State;
            ShowsBooked = (from s in db.User_Show
                           where s.MusicianID == user.ID
                           select s).Count();
        }
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public float? Rating { get; set; }

        [StringLength(20)]
        public string City { get; set; }

        [StringLength(20)]
        public string State { get; set; }

        [Display(Name = "Shows Booked")]
        public int ShowsBooked { get; set; }
    }
}