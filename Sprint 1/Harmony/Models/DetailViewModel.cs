using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Harmony.Models;

namespace Harmony.Models
{
    public class VenueOwnerDetailViewModel
    {
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

        public int TypeName { get; set; }

    }

    public class MusicianDetailViewModel
    {
        MusicianDetailViewModel(User user)
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

        public List<string> Genres { get; set; }
        
        public List<string> BandMembers { get; set; }

        public List<string> Instruments { get; set; }
    }
}