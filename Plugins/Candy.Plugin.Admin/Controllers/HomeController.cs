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