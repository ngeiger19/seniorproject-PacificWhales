using Google.GData.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Harmony.Models
{
    public class ShowsViewModel
    {
        public ShowsViewModel(User_Show show)
        {
            MusicianID = show.MusicianID;
            VenueID = show.VenueOwnerID;
            ShowID = show.ShowID;
            StartTime = show.Show.StartDateTime;
            EndTime = show.Show.EndDateTime;
            DateBooked = show.Show.DateBooked;
        }

        public int ID { get; set; }

        [Required]
        [Display(Name = "Musician ID")]
        public int MusicianID { get; set; }

        [Required]
        [Display(Name = "Venue ID")]
        public int VenueID { get; set; }

        [Required]
        [Display(Name = "Show ID")]
        public int ShowID { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartTime { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public DateTime EndTime { get; set; }

        [Required]
        [Display(Name = "Date Booked")]
        public DateTime DateBooked { get; set; }
    }
}