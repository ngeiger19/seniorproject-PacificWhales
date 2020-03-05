using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Calendar.ASP.NET.MVC5.Models
{
    public class UpcomingEventsViewModel
    {
        [Required]
        public IEnumerable<CalendarEventGroup> EventGroups { get; set; }
    }
}