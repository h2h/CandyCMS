using System;
using System.Web.Mvc;

using Candy.Framework;
using Candy.Framework.Infrastructure;
using Candy.Framework.Themes;

namespace Candy.Core.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
