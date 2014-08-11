using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechTuesdayFeedbackTool.Domain;
using TechTuesdayFeedbackTool.Models;
using TechTuesdayFeedbackTool.Repository;

namespace TechTuesdayFeedbackTool.Controllers
{
    public class PresentationController : Controller
    {
        private IRepository<PresentationDetails> presentationsList;
        private IRepository<User> usersList;
        private IRepository<UserAssociationWithPresentation> userAssociationsList;
        //
        // GET: /Presentation/
        public ActionResult Index()
        {
            presentationsList = new Repository<PresentationDetails>();
            PresentationIndexViewModel vm = new PresentationIndexViewModel();
            vm.PresentationList = presentationsList.Query().ToList();
            if (TempData["CreationSuccess"] != null && TempData["CreationSuccess"] == "true")
            {
                vm.IsReturnedFromCreate = true;
            }
            return View(vm);
        }

        public ActionResult Create()
        {
            
            CreatePresentaionViewModel vm = new CreatePresentaionViewModel();
            usersList = new Repository<User>();
            vm.ListOfUsers = usersList.Query().ToList();
            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(CreatePresentaionViewModel viewModel)
        {
            presentationsList = new Repository<PresentationDetails>();
            if (viewModel.Presentation.Date == null || viewModel.Presentation.Date.Equals(DateTime.MinValue))
            {
                viewModel.Presentation.Date = DateTime.Now;
            }
            if (presentationsList.Save(viewModel.Presentation))
            {
                int presentationId = viewModel.Presentation.ID;
                userAssociationsList = new Repository<UserAssociationWithPresentation>();
                if(userAssociationsList.Save(new UserAssociationWithPresentation() { PresentationID = presentationId, UserID = Convert.ToInt32(viewModel.SelectedUserId) }))
                {
                    TempData["CreationSuccess"] = "true";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Creation failed!");
                }
            }
            //viewModel.Date = DateTime.Now;
            //if(presentationsList.Save(presentation))
            //{
            //    return RedirectToAction("Index", new { creationSuccess = "true" });
            //}
            //else
            //{
            //    ModelState.AddModelError("", "Creation failed!");
            //}
            return View();
        }
	}
}