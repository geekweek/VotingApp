using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TechTuesdayFeedbackTool.Domain
{
    public class Ratings : Entity
    {
        public int UserID { get; set; }
        //Presentation
        public int PresentationID { get; set; }
        //Scores Master
        public int ScoreID { get; set; }
        //Ratings
        public DateTime VotedOn { get; set; }
    }
}