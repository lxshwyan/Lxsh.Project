
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Lxsh.Project.SSOLoginCenter
{
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)) return;
            base.OnAuthorization(filterContext);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var basicAuth = base.AuthorizeCore(httpContext);

            if (!string.IsNullOrEmpty(httpContext.User.Identity.Name))
            {
                var info = Constants.ICacheManager.Get(httpContext.User.Identity.Name);
                if (info == null || string.IsNullOrEmpty(info.ToString())) return false;

                var cookie = httpContext.Request.Cookies[Constants.CookieName];
                if (cookie == null || string.IsNullOrEmpty(cookie.Value)) return false;
                if (!info.ToString().Equals(cookie.Value)) return false;
            }
            
            return basicAuth;
        }

        /// <summary>
        /// 未登录则跳转到登录页
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult(
                string.Concat(FormsAuthentication.LoginUrl,
                             "?ReturnUrl=",
                             filterContext.HttpContext.Server.UrlEncode(filterContext.HttpContext.Request.Url.AbsoluteUri)));
        }
    }
}