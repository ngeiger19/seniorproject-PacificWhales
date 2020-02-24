namespace Harmony.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Venue
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string VenueName { get; set; }

        [Required]
        [StringLength(50)]
        public string Street { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(24)]
        public string State { get; set; }

        public int VenueTypeID { get; set; }

        public virtual VenueType VenueType { get; set; }
    }
}
