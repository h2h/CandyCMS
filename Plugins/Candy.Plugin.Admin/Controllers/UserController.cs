using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Candy.Core.Domain;
using Candy.Core.Services;

namespace Candy.Plugin.Admin.Controllers
{
    public class UserController : BaseAdminController
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;
        public UserController(IUserService userService,
            IAuthenticationService authenticationService)
        {
            this._userService = userService;
            this._authenticationService = authenticationService;
        }
        public new ActionResult Profile()
        {
            var user = this._authenticationService.GetAuthenticatedUser();
            return View(user);
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}