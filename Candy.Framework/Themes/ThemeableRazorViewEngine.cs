using System.Collections.Generic;
using System.Web.Mvc;

namespace Candy.Framework.Themes
{
    public class ThemeableRazorViewEngine : ThemeableVirtualPathProviderViewEngine
    {
        public ThemeableRazorViewEngine()
        {
            AreaViewLocationFormats = new[]
            {
                //主题
                "~/Themes/{2}.Theme/Views/{1}/{0}.cshtml",
                "~/Themes/{2}.Theme/Views/Shared/{0}.cshtml",
                                              
                //默认
                "~/Plugins/{2}/Views/{1}/{0}.cshtml",
                "~/Plugins/{2}/Views/Shared/{0}.cshtml",
            };

            AreaMasterLocationFormats = new[]
            {
                //themes
                "~/Themes/{2}.Theme/Views/{1}/{0}.cshtml",
                "~/Themes/{2}.Theme/Views/Shared/{0}.cshtml",

                //default
                "~/Plugins/{2}/Views/{1}/{0}.cshtml",
                "~/Plugins/{2}/Views/Shared/{0}.cshtml",
            };

            AreaPartialViewLocationFormats = new[]
            {
                //themes
                "~/Themes/{2}.Theme/Views/{1}/{0}.cshtml",
                "~/Themes/{2}.Theme/Views/Shared/{0}.cshtml",
                                                    
                //default
                "~/Plugins/{2}/Views/{1}/{0}.cshtml",
                "~/Plugins/{2}/Views/Shared/{0}.cshtml"
            };


            ViewLocationFormats = new[]
            {
                "~/Themes/{2}/Views/{1}/{0}.cshtml", 
                "~/Themes/{2}/Views/Shared/{0}.cshtml"
            };
            MasterLocationFormats = new[]
            {
                "~/Themes/{2}/Views/{1}/{0}.cshtml", 
                "~/Themes/{2}/Views/Shared/{0}.cshtml"
            };
            PartialViewLocationFormats = new[]
            {
                "~/Themes/{2}/Views/{1}/{0}.cshtml",
                "~/Themes/{2}/Views/Shared/{0}.cshtml"
            };

            FileExtensions = new[] { "cshtml" };
            
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            IEnumerable<string> fileExtensions = base.FileExtensions;
            return new RazorView(controllerContext, partialPath, null, false, fileExtensions);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            IEnumerable<string> fileExtensions = base.FileExtensions;
            return new RazorView(controllerContext, viewPath, masterPath, true, fileExtensions);
        }
    }
}
