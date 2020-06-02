using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Harmony.Models;
using Calendar.ASP.NET.MVC5.Models;
using System.Web.Mvc;
using System.Web.WebPages.Html;

namespace Harmony.Models
{
    public class VenueOwnerDetailViewModel
    {
        public VenueOwnerDetailViewModel() { }
        public VenueOwnerDetailViewModel(Venue venue)
        {
            ID = venue.ID;
            Owner = venue.User.FirstName + " " + venue.User.LastName;
            City = venue.City;
            State = venue.State;
            OwnerEmail = venue.User.Email;
            Description = venue.User.Description;
            VenueName = venue.VenueName;
            AddressLine1 = venue.AddressLine1;
            AddressLine2 = venue.AddressLine2;
            ZipCode = venue.ZipCode;
            Type = venue.VenueType.TypeName;
            UserID = venue.UserID;
            AveRating = venue.User.AveRating;
            Facebook = venue.User.Facebook;
            Instagram = venue.User.Instagram;
            Twitter = venue.User.Twitter;
        }

        public int ID { get; set; }

        [StringLength(50)]
        public string Owner { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(24)]
        public string State { get; set; }

        [StringLength(100)]
        public string OwnerEmail { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        [StringLength(50)]
        [Display(Name = "Venue Name")]
        public string VenueName { get; set; }

        [StringLength(50)]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [StringLength(50)]
        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [StringLength(10)]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        public string Type { get; set; }

        public int UserID { get; set; }

        public List<Show> UpcomingShows { get; set; }

        // This section is for the calendar event form
        [Display(Name = "Show Title")]
        public string Title { get; set; }

        [Display(Name = "Start DateTime")]
        public DateTime StartDateTime { get; set; }

        [Display(Name = "End DateTime")]
        public DateTime EndDateTime { get; set; }

        [Display(Name = "Show Description")]
        public string ShowDescription { get; set; }

        public DateTime DateBooked { get; set; }
        // public List<SelectListItem> VenueList { get; set; }
        [Display(Name = "Rating")]
        public double AveRating { get; set; }

        [StringLength(100)]
        public string Facebook { get; set; }
        [StringLength(100)]
        public string Instagram { get; set; }
        [StringLength(100)]
        public string Twitter { get; set; }
    }

    public class MusicianDetailViewModel
    {
        public MusicianDetailViewModel() { }
        public MusicianDetailViewModel(User user)
        {
            ID = user.ID;
            FirstName = user.FirstName;
            LastName = user.LastName;
            City = user.City;
            State = user.State;
            Email = user.Email;
            Description = user.Description;
            Genres = user.Genres.ToList();
            AveRating = user.AveRating;
            Facebook = user.Facebook;
            Instagram = user.Instagram;
            Twitter = user.Twitter;
            Spotify = user.Spotify;
            AppleMusic = user.AppleMusic;
            Youtube = user.Youtube;
        }

        public int ID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Email { get; set; }

        public string Description { get; set; }

        public List<Genre> Genres { get; set; }

        public List<Show> UpcomingShows { get; set; }
        // This section is for the calendar event form
        [Display(Name = "Show Title")]
        public string Title { get; set; }

        [Display(Name = "Start Time")]
        public DateTime StartDateTime { get; set; }

        [Display(Name = "End Time")]
        public DateTime EndDateTime { get; set; }

        [Display(Name = "Venue Name")]
        public int VenueID { get; set; }

        public string ShowDescription { get; set; }

        public DateTime DateBooked { get; set; }
        public SelectList VenueList { get; set; }

        [Display(Name = "Rating")]
        public double AveRating { get; set; }

        [StringLength(100)]
        public string Facebook { get; set; }

        [StringLength(100)]
        public string Instagram { get; set; }

        [StringLength(100)]
        public string Twitter { get; set; }

        [StringLength(100)]
        public string Spotify { get; set; }

        [StringLength(100)]
        [Display(Name = "Apple Music")]
        public string AppleMusic { get; set; }

        [StringLength(100)]
        public string Youtube { get; set; }

        public List<string> stateList { get; set; }
    }
}