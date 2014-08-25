using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Accolite.GeekWeek.Voting.Models;
using Accolite.GeekWeek.Voting.DataLayer;
using Newtonsoft.Json;

namespace Accolite.GeekWeek.Voting.Controllers
{
    public class VotingDashboardController : Controller
    {
        DataAccess dataAccess = new DataAccess();

        public ActionResult Landing()
        {  
            VotingModel viewmodel = new VotingModel();
            viewmodel.UserData = Utility.GetUserDetailsFromSession();
            viewmodel.ViewPresentationsDetails.EndDate = DateTime.Now.Date;
            viewmodel.ViewPresentationsDetails.StartDate = viewmodel.ViewPresentationsDetails.EndDate.AddDays(-30).Date;

            if (viewmodel.UserData != null)
            {
                viewmodel.NewPresentation.Presenter = viewmodel.UserData.Name;
                return View(viewmodel);
            }
            else
                return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult GetPresentations(object VotingModel)
        {
            VotingModel Model = (VotingModel)JsonConvert.DeserializeObject(VotingModel.ToString(), typeof(VotingModel));
            List<PresentationDetails> ResultedPresentations = new List<PresentationDetails>();
            
            //if date not selected show presentation from last 30 days
            if (Model.ViewPresentationsDetails.EndDate == DateTime.Parse("01/01/0001") || Model.ViewPresentationsDetails.EndDate == null)
            {
                Model.ViewPresentationsDetails.EndDate = DateTime.Now;
            }
            if (Model.ViewPresentationsDetails.StartDate ==  DateTime.Parse("01/01/0001") || Model.ViewPresentationsDetails.StartDate == null)
            {
                Model.ViewPresentationsDetails.StartDate = Model.ViewPresentationsDetails.EndDate.AddDays(-30);
            }

            dataAccess.GetPresentationsWithinPeriod("Completed", ref ResultedPresentations, Model.ViewPresentationsDetails.StartDate, Model.ViewPresentationsDetails.EndDate);
            Model.ViewPresentationsDetails.SearchedPresentationDetails = ResultedPresentations;
            return Json(Model);
        }

        public void GetUpComingPresentations(VotingModel Model)
        {
            List<PresentationDetails> ResultedSessions = new List<PresentationDetails>();
            dataAccess.GetPresentationsWithinPeriod("Upcoming", ref ResultedSessions);
            Model.UpcomingPresentationDetails.UpcomingSessions = ResultedSessions;
        }

        [HttpPost]
        public ActionResult AddNewUserRating(object VotingModel)
        {
            VotingModel Model = (VotingModel)JsonConvert.DeserializeObject(VotingModel.ToString(), typeof(VotingModel));

            dataAccess.AddNewUserRating(Model.RatingData);   

            return Json(Model);
        }

        [HttpPost]
        public ActionResult AddNewPresentations(object VotingModel)
        {
            VotingModel Model = (VotingModel)JsonConvert.DeserializeObject(VotingModel.ToString(), typeof(VotingModel));

            dataAccess.AddNewPresentations(Model.NewPresentation);
            Model.NewPresentation = new PresentationDetails();

            return Json(Model);

        }
    }
}
