using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Candy.Core.Controllers;

namespace Candy.Plugin.Admin.Controllers
{
    [AdminAuthorize]
    public class BaseAdminController : BaseController
    {
    }
} 