using System;
using System.Web.Mvc;
using Candy.Framework.Infrastructure;
using Candy.Framework.Themes;
using Candy.Framework.Utility.Extensions;

namespace Candy.Framework.Mvc.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string ThemePath(this UrlHelper urlHelper, string url)
        {
            var themeContext = EngineContext.Current.Resolve<IThemeContext>();
            var baseUrl = MakeAbsolute(urlHelper, string.Format("~/Themes/{0}/", themeContext.WorkingThemeName));
            return MakeAbsolute(urlHelper, url, baseUrl);
        }

        public static string AbsoluteAction(this UrlHelper urlHelper, Func<string> urlAction)
        {
            return string.Empty;
        }

        public static string MakeAbsolute(this UrlHelper urlHelper, string url, string baseUrl = null)
        {
            if (url != null && url.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                return url;

            if (string.IsNullOrEmpty(baseUrl))
                baseUrl = urlHelper.RequestContext.HttpContext.Request.ToApplicationRootUrlString();

            if (string.IsNullOrEmpty(url))
                return baseUrl;

            var applicationPath = urlHelper.RequestContext.HttpContext.Request.ApplicationPath ?? string.Empty;

            if (url.StartsWith("~/", StringComparison.OrdinalIgnoreCase))
                url = url.Substring(1);

            if (!url.StartsWith("/"))
                url = "/" + url;

            if (url.StartsWith(applicationPath, StringComparison.OrdinalIgnoreCase))
                url = url.Substring(applicationPath.Length);

            baseUrl = baseUrl.TrimEnd('/');
            url = url.TrimEnd('/');

            return baseUrl + "/" + url;
        }
    }
}