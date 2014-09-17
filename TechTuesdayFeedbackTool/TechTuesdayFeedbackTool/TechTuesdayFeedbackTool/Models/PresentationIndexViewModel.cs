using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechTuesdayFeedbackTool.Domain;

namespace TechTuesdayFeedbackTool.Models
{
    public class PresentationIndexViewModel
    {
        public List<PresentationDetails> PresentationList { get; set; }
        public bool IsReturnedFromCreate { get; set; }
        public bool IsReturnedFromEdit { get; set; }
    }
}