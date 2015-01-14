using System.Web.Mvc;

using Candy.Core.Services;
using Candy.Framework.Infrastructure;

namespace Candy.Core.Extensions
{
    public static class LocalizedExtensions
    {
        public static string Lang(this HtmlHelper htmlHelper, string text)
        {
            var localizedService = EngineContext.Current.Resolve<ILocalizationService>();
            var localeResource = localizedService.GetByKey(text);
            if (localeResource == null)
                return text;
            else
                return localeResource.Value;
        }
        public static string Lang(this HtmlHelper htmlHelper, string text, params object[] args)
        {
            var localizedService = EngineContext.Current.Resolve<ILocalizationService>();
            var localeResource = localizedService.GetByKey(text);

            try
            {
                if (localeResource == null)
                    return string.Format(text, args);
                else
                    return string.Format(localeResource.Value, args);
            }
            catch
            {
                return text;
            }
        }
    }
}
