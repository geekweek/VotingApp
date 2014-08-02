using System.ComponentModel.DataAnnotations.Schema;

namespace TechTuesdayFeedbackTool.Domain
{
    public class UserAssociationWithPresentation : Entity
    {
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }
        //Presentation
        public int PresentationID { get; set; }
        [ForeignKey("PresentationID")]
        public PresentationDetails Presentation { get; set; }
    }
}