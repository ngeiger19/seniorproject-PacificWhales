using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;


namespace class_project.Models.ViewModels
{
    public class AthleteDetailsViewModel
    {
        public AthleteDetailsViewModel(Athlete athlete)
        {
            AthleteID = athlete.ID;
            AthleteFirstName = athlete.FirstName;
            AthleteLastName = athlete.LastName;
            string coach = athlete.Coach.CoachName;
            CoachName = coach;
            string team = athlete.Team.TeamName;
            TeamName = team;
            string stroke = athlete.Meets.First().Event.Stroke;
            Stroke = stroke;
            string distance = athlete.Meets.First().Event.Distance;
            Distance = distance;
            string racetime = athlete.Meets.First().Records.First().RaceTime;
            RaceTime = racetime;
            string location = athlete.Meets.First().Location;
            Location = location;
        }
        public int AthleteID { get; private set; }
        public string AthleteFirstName { get; private set; }
        public string AthleteLastName { get; private set; }
        public string CoachName { get; private set; }
        public string TeamName { get; private set; }
        public string Stroke { get; private set; }
        public string Distance { get; private set; }
        public string RaceTime { get; private set; }
        public string Location { get; private set; }

    }
}