namespace class_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Record
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Location { get; set; }

        public int AthleteID { get; set; }

        public int EventID { get; set; }

        public double? RaceTime { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public virtual Athlete Athlete { get; set; }

        public virtual Event Event { get; set; }
    }
}
