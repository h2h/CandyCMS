using System.Web.Mvc;

namespace Candy.Core.Controllers
{
    public class SettingsController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}