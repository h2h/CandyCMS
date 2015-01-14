using System.Web.Routing;
using System.Web.Mvc;

namespace Candy.Framework.Mvc.Routes
{
    public static class RouteCollectionExtensions
    {
        /// <summary>
        /// 添加区域路由注册
        /// </summary>
        /// <param name="routes"></param>
        /// <param name="name"></param>
        /// <param name="url"></param>
        /// <param name="defaults"></param>
        /// <param name="namespaces"></param>
        /// <param name="areaName"></param>
        /// <returns></returns>
        public static Route MapRoute(this RouteCollection routes, string name, string url, object defaults, string[] namespaces, string areaName)
        {
            var route = routes.MapRoute(name, url, defaults, namespaces);
            route.DataTokens["area"] = areaName;
            return route;
        }
    }
}
