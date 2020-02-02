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
        public string RaceTime { get; set; }

        public int MeetID { get; set; }

        public virtual Meet Meet { get; set; }
    }
}
