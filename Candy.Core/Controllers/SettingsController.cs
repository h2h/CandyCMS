using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;
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
