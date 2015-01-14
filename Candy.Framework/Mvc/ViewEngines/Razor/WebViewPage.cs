using System;
using System.IO;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Runtime.CompilerServices;

using Candy.Framework.Localization;
using Candy.Framework.Mvc.Extensions;
using Candy.Framework.Infrastructure;
using Candy.Framework.Configuration;
using Candy.Framework.Themes;

namespace Candy.Framework.Mvc.ViewEngines.Razor
{
    public abstract class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        public string T
        {
            get
            {
                return string.Empty;
            }
        }

        public override void InitHelpers()
        {
            base.InitHelpers();
        }

        public MvcHtmlString Style(string path)
        {
            var areaName = ViewContext.RouteData.GetAreaName();
            if (path.StartsWith("~/", StringComparison.OrdinalIgnoreCase))
                path = path.Substring(1);

            if (path.StartsWith("/", StringComparison.OrdinalIgnoreCase))
                path = path.Substring(1);

            var themeContext = EngineContext.Current.Resolve<IThemeContext>();

            if (!string.IsNullOrEmpty(areaName))
                path = Url.MakeAbsolute(string.Format("~/Plugins/{0}/{1}", areaName, path));
            else
                path = Url.MakeAbsolute(string.Format("~/Themes/{0}/{1}", themeContext.WorkingThemeName, path));

            var style = new TagBuilder("link");
            style.MergeAttribute("href", path);
            style.MergeAttribute("rel", "stylesheet");

            return MvcHtmlString.Create(style.ToString());
        }
        public MvcHtmlString Script(string path)
        {
            var areaName = ViewContext.RouteData.GetAreaName();

            if (path.StartsWith("~/", StringComparison.OrdinalIgnoreCase))
                path = path.Substring(1);

            if (path.StartsWith("/", StringComparison.OrdinalIgnoreCase))
                path = path.Substring(1);

            var themeContext = EngineContext.Current.Resolve<IThemeContext>();

            if (!string.IsNullOrEmpty(areaName))
                path = Url.MakeAbsolute(string.Format("~/Plugins/{0}/{1}", areaName, path));
            else
                path = Url.MakeAbsolute(string.Format("~/Themes/{0}/{1}", themeContext.WorkingThemeName, path));

            var script = new TagBuilder("script");
            script.MergeAttribute("src", path);

            return MvcHtmlString.Create(script.ToString());
        }
        public override string Layout
        {
            get
            {
                var layout = base.Layout;

                if (!string.IsNullOrEmpty(layout))
                {
                    var filename = Path.GetFileNameWithoutExtension(layout);
                    ViewEngineResult viewResult = System.Web.Mvc.ViewEngines.Engines.FindView(ViewContext.Controller.ControllerContext, filename, "");

                    if (viewResult.View != null && viewResult.View is RazorView)
                    {
                        layout = (viewResult.View as RazorView).ViewPath;
                    }
                }

                return layout;
            }
            set
            {
                base.Layout = value;
            }
        }
    }

    public abstract class WebViewPage : WebViewPage<dynamic>
    {
    }
}
