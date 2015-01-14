using System.Web.Mvc;
using System.Web.Routing;

namespace Candy.Framework.Mvc.Extensions
{
    public static class RouteExtension
    {
        public static string GetAreaName(this RouteBase route)
        {
            var area = route as IRouteWithArea;
            if (area != null)
            {
                return area.Area;
            }
            var route2 = route as Route;
            if ((route2 != null) && (route2.DataTokens != null))
            {
                return (route2.DataTokens["area"] as string);
            }
            return null;
        }
        public static string GetAreaName(this RouteData routeData)
        {
            object area;
            if (routeData.DataTokens.TryGetValue("area", out area))
                return area as string;

            return GetAreaName(routeData.Route);
        }
    }
}
