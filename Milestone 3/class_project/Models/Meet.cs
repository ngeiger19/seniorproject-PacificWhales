namespace class_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Meet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Meet()
        {
            Records = new HashSet<Record>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Location { get; set; }

        public int AthleteID { get; set; }

        public int EventID { get; set; }

        public virtual Athlete Athlete { get; set; }

        public virtual Event Event { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Record> Records { get; set; }
    }
}
