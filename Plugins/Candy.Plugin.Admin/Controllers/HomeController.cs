using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Candy.Core.Controllers;
using System.Web.Mvc;

namespace Candy.Plugin.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        public HomeController()
        { }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult App()
        {
            return View();
        }
        public ActionResult Header()
        {
            return View();
        }
        public ActionResult Aside()
        {
            return View();
        }
        public ActionResult Nav()
        {
            return View();
        }
    }
}
