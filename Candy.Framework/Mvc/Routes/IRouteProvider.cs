using System.Web.Routing;

namespace Candy.Framework.Mvc.Routes
{
    public interface IRouteProvider
    {
        /// <summary>
        /// 注册路由
        /// </summary>
        /// <param name="routes"></param>
        void RegisterRoutes(RouteCollection routes);
    }
}
