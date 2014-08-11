using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechTuesdayFeedbackTool.Domain;

namespace TechTuesdayFeedbackTool.Models
{
    public class CreatePresentaionViewModel
    {
        public PresentationDetails Presentation { get; set; }
        public List<User> ListOfUsers { get; set; }
        public int MyProperty { get; set; }
        public string SelectedUserId { get; set; }
    }
}