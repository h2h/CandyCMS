using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Candy.Framework.Configuration;
using Candy.Framework.Infrastructure;
using Candy.Framework.Localization;
using Candy.Framework.Mvc.Routes;
using Candy.Framework.Themes;

namespace Candy.Web
{
    public class MvcApplication : HttpApplication
    {
        public static void RegisterRoute(RouteCollection routes)
        {
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // 实现注册所有插件路由
            var routePublisher = EngineContext.Current.Resolve<IRoutePublisher>();
            routePublisher.RegisterRoutes(routes);

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "Candy.Core.Controllers" }
            );
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            EngineContext.Initialize(false);

            LocalizerManager.Initialize();

            // 清除所有视图引擎
            ViewEngines.Engines.Clear();
            // 添加自定义视图引擎
            ViewEngines.Engines.Add(new ThemeableRazorViewEngine());

            AreaRegistration.RegisterAllAreas();
            RegisterRoute(RouteTable.Routes);

            var config = EngineContext.Current.Resolve<CandyConfig>();

            if (config.IsInstalled)
            {
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            EngineContext.Current.RunBeginRequestTasks();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
        }

        protected void Session_End(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }
    }
}