using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using Candy.Core.Domain;


namespace Candy.Core.Services
{
    public partial class FormsAuthenticationService : IAuthenticationService
    {
        private readonly HttpContextBase _httpContext;
        private readonly IUserService _userService;

        private User _cachedUser;

        public FormsAuthenticationService(HttpContextBase httpContext,
            IUserService userService)
        {
            this._httpContext = httpContext;
            this._userService = userService;
        }
        public virtual void SignIn(User user, bool createPersistentCookie)
        {
            var ticket = new FormsAuthenticationTicket(
                1,
                user.UserName,
                DateTime.Now,
                DateTime.Now.Add(FormsAuthentication.Timeout),
                createPersistentCookie,
                user.UserName,
                FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            cookie.HttpOnly = true;

            if (ticket.IsPersistent)
                cookie.Expires = ticket.Expiration;

            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Path = FormsAuthentication.FormsCookiePath;

            if (FormsAuthentication.CookieDomain != null)
                cookie.Domain = FormsAuthentication.CookieDomain;

            this._httpContext.Response.Cookies.Add(cookie);
            this._cachedUser = user;
        }
        public virtual void SignOut()
        {
            _cachedUser = null;
            FormsAuthentication.SignOut();
        }

        public virtual User GetAuthenticatedUser()
        {
            if (_cachedUser != null)
                return _cachedUser;

            if (this._httpContext == null ||
                this._httpContext.Request == null ||
                !this._httpContext.Request.IsAuthenticated ||
                !(_httpContext.User.Identity is FormsIdentity))
                return null;

            var formsIdentity = (FormsIdentity)_httpContext.User.Identity;
            var user = GetAuthenticatedUserFromTicket(formsIdentity.Ticket);
            if (user != null)
                this._cachedUser = user;

            return _cachedUser;
        }

        public virtual User GetAuthenticatedUserFromTicket(FormsAuthenticationTicket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");

            var username = ticket.UserData;

            if (string.IsNullOrWhiteSpace(username))
                return null;

            var user = this._userService.GetByUserName(username);
            return user;
        }
    }
}
