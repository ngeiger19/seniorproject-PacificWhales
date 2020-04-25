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
        }

        public int ID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(50)]
<<<<<<< HEAD
        public string Owner { get; set; }
=======
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }
>>>>>>> dev0.3

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(24)]
        public string State { get; set; }

        [Required]
        [StringLength(100)]
        public string OwnerEmail { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Venue Name")]
        public string VenueName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [StringLength(50)]
        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Venue City")]
        public string VenueCity { get; set; }

        [Required]
        [StringLength(24)]
        [Display(Name = "Venue State")]
        public string VenueState { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        public string Type { get; set; }

        public int UserID { get; set; }

        // This section is for the calendar event form
        [Display(Name = "ShowTitle")]
        public string Title { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "StartDateTime")]
        public DateTime StartDateTime { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "EndDateTime")]
        public DateTime EndDateTime { get; set; }

        public string ShowDescription { get; set; }

        public DateTime DateBooked { get; set; }
        public List<SelectListItem> VenueList { get; set; }
        public IEnumerable<CalendarEventGroup> UpcomingEvents { get; set; }
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
            Genres = user.Genres.Select(g => g.GenreName).ToList();
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

        public List<string> Genres { get; set; }

        // This section is for the calendar event form
        [Display(Name = "Show Title")]
        public string Title { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Start Time")]
        public DateTime StartDateTime { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "End Time")]
        public DateTime EndDateTime { get; set; }

        [Display(Name = "Venue Name")]
        public int VenueID { get; set; }

        public string ShowDescription { get; set; }

        public DateTime DateBooked { get; set; }
        public List<SelectListItem> VenueList { get; set; }
        public IEnumerable<CalendarEventGroup> UpcomingEvents { get; set; }
    }
}