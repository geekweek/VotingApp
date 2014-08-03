using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechTuesdayFeedbackTool.Domain;
using TechTuesdayFeedbackTool.Repository;

namespace TechTuesdayFeedbackTool.Controllers
{
    public class PresentationController : Controller
    {
        private IRepository<PresentationDetails> presentationsList;
        //
        // GET: /Presentation/
        public ActionResult Index()
        {
            presentationsList = new Repository<PresentationDetails>();
            return View(presentationsList.Query().ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PresentationDetails presentation)
        {
            presentationsList = new Repository<PresentationDetails>();
            presentation.Date = DateTime.Now;
            if(presentationsList.Save(presentation))
            {
                return RedirectToAction("Index", new { creationSuccess = "true" });
            }
            else
            {
                ModelState.AddModelError("", "Creation failed!");
            }
            return View();
        }
	}
}