using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SongWishing
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /// <summary>
        /// Here we set _UserSession to se wich user to send message to
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void Session_Start(Object sender, EventArgs e)
        {
            HttpContext.Current.Session.Add("_UserSession", string.Empty);

            string userSession = Session["_UserSession"] as string;

            if (string.IsNullOrEmpty(userSession))
            {
                Session["_UserSession"] = HttpContext.Current.User.Identity.Name.ToString();
            }




        }
    }
}
