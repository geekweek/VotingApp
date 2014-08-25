using System.Web;
using System.Web.Mvc;

namespace Accolite.GeekWeek.Voting
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}