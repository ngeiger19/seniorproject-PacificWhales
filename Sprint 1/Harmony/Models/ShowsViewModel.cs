using Google.GData.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Harmony.DAL;

namespace Harmony.Models
{
    public class ShowsViewModel
    {
        HarmonyContext db = new HarmonyContext();

        public ShowsViewModel() { }

        public ShowsViewModel(User_Show show)
        {
            Title = show.Show.Title;
            MusicianID = show.MusicianID;
            VenueID = show.VenueOwnerID;
            ShowID = show.ShowID;
            StartTime = show.Show.StartDateTime;
            EndTime = show.Show.EndDateTime;
            DateBooked = show.Show.DateBooked;
            Description = show.Show.Description;

            VenueName = (from v in db.Venues
                         where v.UserID == show.VenueOwnerID
                         select v).First().VenueName;

            MusicianName = (from u in db.Users
                            where u.ID == show.MusicianID
                            select u).First().FirstName;
            MusicianRated = show.MusicianRated;
            VenueRated = show.VenueRated;
        }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Musician ID")]
        public int MusicianID { get; set; }

        [Display(Name = "Venue ID")]
        public int VenueID { get; set; }

        [Display(Name = "Show ID")]
        public int ShowID { get; set; }

        public bool? MusicianRated { get; set; }

        public bool? VenueRated { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartTime { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndTime { get; set; }

        [Display(Name = "Date Booked")]
        public DateTime DateBooked { get; set; }

        [Display(Name = "Venue")]
        public string VenueName { get; set; }

        [Display(Name = "Musician")]
        public string MusicianName { get; set; }

        public string Description { get; set; }

        public string RatingValue { get; set; }

        public string Status { get; set; }
    }
}