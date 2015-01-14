using System.Web.Mvc;
using System.Web.Routing;
using Candy.Framework.Mvc.Routes;

namespace Candy.Plugin.Install
{
    public partial class RouteProvider : IRouteProvider
    {
        public static string AreaName
        {
            get { return "Candy.Plugin.Install"; }
        }
        public void RegisterRoutes(RouteCollection routes)
        {
            // 安装路由
            routes.MapRoute(
                "Candy.Plugin.Install.Install.Route",               // 路由名称
                "Install",                                          // 路由 URL
                new { controller = "Install", action = "Index"},    // 默认配置
                new[] { "Candy.Plugin.Install.Controllers" },       // 命名空间
                AreaName                                            // Area Name 必须
            );

            // 重新安装路由
            routes.MapRoute(
                "Candy.Plugin.Install.Upgrade.Route",
                "Upgrade",
                new { controller = "Install", action = "Upgrade" },
                new[] { "Candy.Plugin.Install.Controllers" },
                AreaName
            );

            // 重新安装路由
            routes.MapRoute(
                "Candy.Plugin.Install.Reinstall.Route",
                "Reinstall",
                new { controller = "Install", action = "Reinstall" },
                new[] { "Candy.Plugin.Install.Controllers" },
                AreaName
            );

            routes.MapRoute(
                "Candy.Plugin.Install.Connection.Route",
                "Install/Connection/",
                new { controller = "Install", action = "Connection" },
                new[] { "Candy.Plugin.Install.Controllers" },
                AreaName
            );

            routes.MapRoute(
                "Candy.Plugin.Install.CreateDatabase.Route",
                "Install/CreateDatabase/",
                new { controller = "Install", action = "CreateDatabase" },
                new[] { "Candy.Plugin.Install.Controllers" },
                AreaName
            );

            routes.MapRoute(
                "Candy.Plugin.Install.Configure.Route",
                "Install/Configure/",
                new { controller = "Install", action = "Configure" },
                new[] { "Candy.Plugin.Install.Controllers" },
                AreaName
            );

            routes.MapRoute(
                "Candy.Plugin.Install.Route",
                "Install/{controller}/{action}/{id}",
                new { controller = "Install", action = "Index", id = UrlParameter.Optional },
                new[] { "Candy.Plugin.Install.Controllers" },
                AreaName
             );
        }
    }
}
