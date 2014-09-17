using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechTuesdayFeedbackTool.Domain
{
    public class Comments : Entity
    {
        //Users        
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }
        //Presentation Details        
        public int PresentationID { get; set; }
        [ForeignKey("PresentationID")]
        public PresentationDetails Presentation { get; set; }
        //Comments
        public string CommentText { get; set; }
        public DateTime TimeStamp { get; set; }
        public int ParentCommentID { get; set; }
        public int Likes { get; set; }
        public int DisLikes { get; set; }
    }
}