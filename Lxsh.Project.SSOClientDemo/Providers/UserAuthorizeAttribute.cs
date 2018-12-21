using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Configuration;
using Flurl;
using Flurl.Http;

namespace Lxsh.Project.SSOClientDemo
{
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {
        private string TicketParamName = "UserAuthTicket";
        private string UserNameParamName = "UserName";
        private string ValidateUrl = ConfigurationManager.AppSettings["ValidateUrl"];
        private string LoginUrl = ConfigurationManager.AppSettings["LoginUrl"];

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)) return;
            
            //认证中心重定向过来的 需要保存Ticket至Cookie
            var ticketCookieValue = filterContext.HttpContext.Request.QueryString[TicketParamName];
            var userNameCookieValue = filterContext.HttpContext.Request.QueryString[UserNameParamName];
            if (!string.IsNullOrEmpty(ticketCookieValue))
            {
                HttpCookie cookie = new HttpCookie(TicketParamName, ticketCookieValue)
                {
                    Expires = DateTime.Now.AddDays(7),
                };
                filterContext.HttpContext.Response.AppendCookie(cookie);
            }
            if (!string.IsNullOrEmpty(userNameCookieValue))
            {
                HttpCookie cookie = new HttpCookie(UserNameParamName, userNameCookieValue)
                {
                    Expires = DateTime.Now.AddDays(7),
                };
                filterContext.HttpContext.Response.AppendCookie(cookie);
            }
            base.OnAuthorization(filterContext);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //取本地Cookie的Ticket
            var ticketCookie = httpContext.Request.Cookies[TicketParamName];
            if (ticketCookie == null || string.IsNullOrEmpty(ticketCookie.Value)) return false;
            var userNameCookie = httpContext.Request.Cookies[UserNameParamName];
            if (userNameCookie == null || string.IsNullOrEmpty(userNameCookie.Value)) return false;

            //从服务器验证当前是否登录
            var result = ValidateUrl.SetQueryParams(new
            {
                UserAuthTicket = ticketCookie.Value,
                UserName = userNameCookie.Value
            })
            .GetJsonAsync<bool>().Result;
            return result;
        }
      
        /// <summary>
        /// 验证失败重定向到SSO认证中心
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //string str = string.Concat(FormsAuthentication.LoginUrl,
            //                 "?ReturnUrl=",
            //                 filterContext.HttpContext.Server.UrlEncode(filterContext.HttpContext.Request.Url.AbsoluteUri));
            filterContext.Result = new RedirectResult(
                string.Concat(LoginUrl,
                             "?ReturnUrl=",
                             filterContext.HttpContext.Server.UrlEncode(filterContext.HttpContext.Request.Url.AbsoluteUri)));
        }
    }
}