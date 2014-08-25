using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Accolite.GeekWeek.Voting.Models;

namespace Accolite.GeekWeek.Voting.DataLayer
{
    public class Utility
    {
        public static void SetUserSession(string userID)
        {
            UserDetails userdata = new UserDetails();
            userdata.UserID = userID;

            var names = userID.Split('.');
            if (names != null && names.Length > 0)
                userdata.Name = names.FirstOrDefault();

            if (HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session["UserDetails"] = userdata;
            }
        }

        public static UserDetails GetUserDetailsFromSession()
        {
            UserDetails user = null;

            if (HttpContext.Current.Session != null && HttpContext.Current.Session["UserDetails"] != null)
            {
                user = new UserDetails();
                user = HttpContext.Current.Session["UserDetails"] as UserDetails;
            }
            return user;
        }
    }
}