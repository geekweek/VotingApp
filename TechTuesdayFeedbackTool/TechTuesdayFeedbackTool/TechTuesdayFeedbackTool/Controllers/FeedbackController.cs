using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechTuesdayFeedbackTool.Models;
using TechTuesdayFeedbackTool.Domain;

namespace TechTuesdayFeedbackTool.Controllers
{
    public class FeedbackController : Controller
    {
        //
        // GET: /Feedback/
        public ActionResult Index()
        {
            ViewBag.Message = "Please provide feedback";
            //ScoresMaster sm = new ScoresMaster
            //{
            //    Score_ID = 1,
            //    Presentation_ID = 1,
            //    Score_1 = 5,
            //    Score_2 = 4,
            //    Score_3 = 3,
            //    Score_4 = 2,
            //    Score_5 = 1,
            //    Average_Score = 3
            //};

            PresentationDetails presentationDetails = new PresentationDetails
            {
                ID=1,
                Name = "Design patterns",
                Description = "This is a PPT",
                Date = DateTime.Now.Date
            };
            return View(presentationDetails);
        }
    }
}