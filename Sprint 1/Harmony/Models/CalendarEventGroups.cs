using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Google.Apis.Calendar.v3.Data;

namespace Calendar.ASP.NET.MVC5.Models
{
    public class CalendarEventGroup
    {
        [Required]
        public string GroupTitle { get; set; }

        [Required]
        public IEnumerable<Event> Events { get; set; }
    }
}