using System;

namespace TechTuesdayFeedbackTool.Domain
{
    public class PresentationDetails : Entity
    {   
        //Presentation details
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}