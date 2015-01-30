using System.Web.Mvc;
using System.Web.Routing;
using Candy.Framework.Mvc.Routes;

namespace Candy.Plugin.Admin
{
    public partial class RouteProvider : IRouteProvider
    {
        private string AreaName
        {
            get { return "Candy.Plugin.Admin"; }
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            var route = routes.MapRoute(
                "Candy.Plugin.Admin.Route",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "Candy.Plugin.Admin.Controllers" },
                AreaName
             );
        }
    }
}