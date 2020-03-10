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
        public VenueOwnerDetailViewModel(Venue venue)
        {
            ID = venue.ID;
            FirstName = venue.User.FirstName;
            LastName = venue.User.LastName;
            City = venue.City;
            State = venue.State;
            Email = venue.User.Email;
            Description = venue.User.Description;
            VenueName = venue.VenueName;
            AddressLine1 = venue.AddressLine1;
            AddressLine2 = venue.AddressLine2;
            ZipCode = venue.ZipCode;
            TypeName = venue.VenueType.TypeName;
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(24)]
        public string State { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string VenueName { get; set; }

        [Required]
        [StringLength(50)]
        public string AddressLine1 { get; set; }

        [StringLength(50)]
        public string AddressLine2 { get; set; }

        [Required]
        [StringLength(50)]
        public string VenueCity { get; set; }

        [Required]
        [StringLength(24)]
        public string VenueState { get; set; }

        [Required]
        [StringLength(10)]
        public string ZipCode { get; set; }

        public string TypeName { get; set; }

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
            BandMembers = user.BandMembers.Select(b => b.BandMemberName).ToList();
        }

        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Email { get; set; }

        public string Description { get; set; }

        public List<string> Genres { get; set; }
        
        public List<string> BandMembers { get; set; }

        public List<string> Instruments { get; set; }

        // This section is for the calendar event form

        public int ShowID { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "DateTime")]
        public DateTime DateTime { get; set; }

        [Display(Name = "VenueName")]
        public int VenueID { get; set; }

        public string ShowDescription { get; set; }

        public DateTime DateBooked { get; set; }
        public List<SelectListItem> VenueList { get; set; }
        public IEnumerable<CalendarEventGroup> UpcomingEvents { get; set; }
    }
}