using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechTuesdayFeedbackTool.Domain
{
    public class Ratings : Entity
    {
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }
       //Scores Master        
        public int ScoreID { get; set; }
        [ForeignKey("ScoreID")]
        public ScoresMaster Score { get; set; }
        //Ratings
        public DateTime VotedOn { get; set; }
    }
}