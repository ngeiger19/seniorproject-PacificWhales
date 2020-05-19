namespace Harmony.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Show
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Show()
        {
            User_Show = new HashSet<User_Show>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(64)]
        public string Title { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public int? VenueID { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public DateTime DateBooked { get; set; }

        [Required]
        [StringLength(64)]
        public string Status { get; set; }

        [Required]
        [StringLength(500)]
        public string GoogleEventID { get; set; }

        [Required]
        public int ShowOwnerID { get; set; }
        public virtual Venue Venue { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User_Show> User_Show { get; set; }
    }
}
