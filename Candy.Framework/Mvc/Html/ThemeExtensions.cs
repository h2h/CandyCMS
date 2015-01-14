using System.Web.Mvc;
using System.Web.Mvc.Html;
using Candy.Framework.Themes;

namespace Candy.Framework.Mvc.Html
{
    public static class ThemeExtensions
    {
        public static string ThemePath(this HtmlHelper helper, ThemeDescriptor theme, string path)
        {
            return string.Format("~/Themes/{1}/{2}", theme.PackageName, path);
        }
    }
}
