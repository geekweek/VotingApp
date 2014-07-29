using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

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