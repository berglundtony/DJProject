using System.Web.Mvc;
using System.Web.Routing;

namespace SongWishing
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Wishes", action = "Create", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "ArtistSearch",
                "Wishes/Search/{searchName}",
                new
                {
                    controller = "Wishes",
                    action = "Search",
                    searchName = ""
                });

            routes.MapRoute(
                "ArtistAjaxSearch",
                "Wishes/getAjaxResult/",
                new
                {
                    controller = "Wishes",
                    action = "getAjaxResult"
                });
        }
    }
}
