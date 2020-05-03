using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Harmony.Models;
using Calendar.ASP.NET.MVC5.Models;
using System.Web.Mvc;

namespace Harmony.Models
{
    public class VenueReccomendationViewModel
    {
        public VenueReccomendationViewModel() { }

        public VenueReccomendationViewModel(Venue venue)
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
}