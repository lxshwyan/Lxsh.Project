using Lxsh.Project.Common;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Security;

namespace Lxsh.Project.SSOLoginCenter
{
    public class ApiAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //标注AllowAnonymousAttribute时不做认证
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
                return;

            //判断用户是否登录
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                throw new LoginFaildException("当前未登录！");
        }
        
        //protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        //{
        //    var challengeMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized);
        //    challengeMessage.Headers.Add("WWW-Authenticate", "Basic");
        //    throw new HttpResponseException(challengeMessage);
        //}
    }
}