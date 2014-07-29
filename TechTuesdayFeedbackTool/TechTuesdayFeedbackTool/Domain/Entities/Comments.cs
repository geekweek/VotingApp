using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TechTuesdayFeedbackTool.Domain
{
    public class Comments : Entity
    {
        //Users
        public int UserID { get; set; }
        //Presentation Details
        public int PresentationID { get; set; }
        //Comments
        public string CommentText { get; set; }
        public DateTime TimeStamp { get; set; }
        public int ParentCommentID { get; set; }
        public int Likes { get; set; }
        public int DisLikes { get; set; }
    }
}