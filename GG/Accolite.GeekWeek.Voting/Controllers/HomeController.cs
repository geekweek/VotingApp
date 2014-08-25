using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetOpenAuth.AspNet.Clients;
using System.Web.Security;
using Accolite.GeekWeek.Voting.Models;
using System.Web.SessionState;
using Accolite.GeekWeek.Voting.DataLayer;

namespace Accolite.GeekWeek.Voting.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home

        public ActionResult Login()
        {

            DotNetOpenAuth.AspNet.Clients.GoogleOpenIdClient client = new GoogleOpenIdClient();

            UrlHelper helper = new UrlHelper(this.ControllerContext.RequestContext);
            var result = helper.Action("Callback", "Home", null, "http");

            client.RequestAuthentication(this.HttpContext, new Uri(result));

            return new EmptyResult();
        }

        // Callback after Login
        public ActionResult Callback()
        {
            DotNetOpenAuth.AspNet.Clients.GoogleOpenIdClient client = new GoogleOpenIdClient();
            var result = client.VerifyAuthentication(this.HttpContext);

            if (result.UserName.ToUpperInvariant().Contains("@ACCOLITE.COM"))
            {
                
                Utility.SetUserSession(result.UserName);
                return RedirectToAction("Landing", "VotingDashboard");
            }
            else
            {
                HttpContext.Session.Abandon();
                FormsAuthentication.SignOut();
                return new EmptyResult();
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Abandon();
            FormsAuthentication.SignOut();
            
            return Redirect("https://www.google.com/accounts/Logout");

        }

    }
}
