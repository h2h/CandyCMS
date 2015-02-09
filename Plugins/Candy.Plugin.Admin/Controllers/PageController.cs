using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Candy.Plugin.Admin.Controllers
{
    [AdminAuthorize]
    public class PageController : BaseAdminController
    {
        // GET: Page
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}