using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using class_project.Models;


namespace class_project.Models.ViewModels
{
    public class AthleteDetailsViewModel
    {
        public class MeetRecord
        { 
            public double? RaceTime { get; set; }

            public DateTime MeetDate { get; set; }

            public string Stroke { get; set; }

            public string Distance { get; set; }

            public string MeetLocation { get; set; }

        }

        public IEnumerable<MeetRecord> Records { get; private set; }
        public AthleteDetailsViewModel(Athlete athlete)
        {
            AthleteID = athlete.ID;
            AthleteFirstName = athlete.FirstName;
            AthleteLastName = athlete.LastName;
            CoachName = athlete.Coach.CoachName;
            TeamName = athlete.Team.TeamName;
            Records = athlete.Records.Select(m => new MeetRecord 
            { 
                MeetDate = m.Date, 
                MeetLocation = m.Location, 
                Distance = m.Event.Distance, 
                Stroke = m.Event.Stroke, 
                RaceTime = m.RaceTime
            }).OrderBy(r => r.MeetDate).ToList();
        }
        public int AthleteID { get; private set; }
        public string AthleteFirstName { get; private set; }
        public string AthleteLastName { get; private set; }
        public string CoachName { get; private set; }
        public string TeamName { get; private set; }
    }
}