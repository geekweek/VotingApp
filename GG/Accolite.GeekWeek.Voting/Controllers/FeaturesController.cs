using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetOpenAuth.AspNet.Clients;
using Accolite.GeekWeek.Voting.DataLayer;


namespace Accolite.GeekWeek.Voting.Controllers
{
    public class FeaturesController : Controller
    {
        //
        // GET: /Features/

        public ActionResult Presentations()
        {
            return View();
        }
        // Point Login URL to this Action
        public ActionResult Login()
        {
            DotNetOpenAuth.AspNet.Clients.GoogleOpenIdClient client = new GoogleOpenIdClient();
            UrlHelper helper = new UrlHelper(this.ControllerContext.RequestContext);
            var result = helper.Action("Presentations", "Features", null, "http");
            client.RequestAuthentication(this.HttpContext, new Uri(result));

            return new EmptyResult();
        }

    }
}
