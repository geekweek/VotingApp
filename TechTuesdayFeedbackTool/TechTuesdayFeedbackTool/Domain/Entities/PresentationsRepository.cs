using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TechTuesdayFeedbackTool.Domain
{
    public class PresentationsRepository : Entity
    {
        public int PresentationID { get; set; }
        //Presentation Repository
        public string FileLocation { get; set; }
        public string Category { get; set; }
    }
}