namespace TechTuesdayFeedbackTool.Domain
{
    public class ScoresMaster : Entity
    {
        public int PresentationID { get; set; }
        //Scores Master
        public int Score1 { get; set; }
        public int Score2 { get; set; }
        public int Score3 { get; set; }
        public int Score4 { get; set; }
        public int Score5 { get; set; }
        public decimal AverageScore { get; set; }
    }
}