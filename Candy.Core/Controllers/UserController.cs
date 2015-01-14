using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using Candy.Core.Domain;

using Candy.Core.Services;
using Candy.Framework.Mvc.Extensions;

namespace Candy.Core.Controllers
{
    public partial class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;
        public UserController(IUserService userService,
            IAuthenticationService authenticationService)
        {
            this._userService = userService;
            this._authenticationService = authenticationService;
        }
        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(LoginUserModel model)
        {
            if (this._userService.Validate(model))
            {
                var user = model.UserName.Contains("@")
                    ? this._userService.GetByEmail(model.UserName)
                    : this._userService.GetByUserName(model.UserName);

                this._authenticationService.SignIn(user, model.Remember);
            }
            return View();
        }
        [HttpPost]
        public ActionResult SignInForJson(LoginUserModel model)
        {
            if (this._userService.Validate(model))
            {
                var user = model.UserName.Contains("@")
                    ? this._userService.GetByEmail(model.UserName)
                    : this._userService.GetByUserName(model.UserName);

                this._authenticationService.SignIn(user, model.Remember);

                return Json(new
                {
                    Result = true,
                    ReturnUrl = string.IsNullOrEmpty(model.ReturnUrl) ? Url.MakeAbsolute("~/") : Url.MakeAbsolute(model.ReturnUrl)
                });
            }
            else
            {
                return Json(new
                {
                    Result = false,
                    Message = "用户名或密码错误"
                });
            }
        }
        public ActionResult SignOut()
        {
            this._authenticationService.SignOut();
            return RedirectToAction("SignIn");
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterUserModel model)
        {
            this._userService.Register(model);
            return View();
        }
    }
}
