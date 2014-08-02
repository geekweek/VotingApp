using System.ComponentModel.DataAnnotations.Schema;

namespace TechTuesdayFeedbackTool.Domain
{
    public class PresentationsRepository : Entity
    {
        public int PresentationID { get; set; }
        [ForeignKey("PresentationID")]
        public PresentationDetails Presentation { get; set; }
        //Presentation Repository
        public string FileLocation { get; set; }
        public string Category { get; set; }
    }
}