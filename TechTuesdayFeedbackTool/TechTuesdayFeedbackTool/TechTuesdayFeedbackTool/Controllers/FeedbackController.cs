using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechTuesdayFeedbackTool.Models;
using TechTuesdayFeedbackTool.Domain;
using TechTuesdayFeedbackTool.Repository;

namespace TechTuesdayFeedbackTool.Controllers
{
    public class FeedbackController : Controller
    {
        //
        // GET: /Feedback/
        public ActionResult Index()
        {
            ViewBag.Message = "Please provide feedback";

            PresentationDetails presentationDetails = new PresentationDetails
            {
                ID=1,
                Name = "Design patterns",
                Description = "This is a PPT",
                Date = DateTime.Now.Date
            };
            return View(presentationDetails);
        }

        public ActionResult UserForm()
        {
            return View(new User());
        }

        public ActionResult CreateUser(User newUser)
        {
            var r = new Repository<User>();
            r.Save(newUser);
            //var d = new DatabaseContext();

            //var roles = d.UserRolesMasterContext.Where(r => r.RoleName.Equals("ADMIN", StringComparison.CurrentCultureIgnoreCase)).ToList();
            //d.UsersContext.Add(newUser);
            //d.SaveChanges();
            //d.Dispose();

            return RedirectToAction("Index");
        }
    }
}