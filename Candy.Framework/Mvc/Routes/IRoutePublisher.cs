using System.Web.Routing;

namespace Candy.Framework.Mvc.Routes
{
    public interface IRoutePublisher
    {
        /// <summary>
        /// 注册路由
        /// </summary>
        /// <param name="routes">路由集合</param>
        void RegisterRoutes(RouteCollection routes);
    }
}