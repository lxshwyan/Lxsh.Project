using Lxsh.Project.Common;
using Lxsh.Project.Common.Log;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace Lxsh.Project.SSOLoginCenter
{ 
    public class HandleCustomExceptionAttribute : ExceptionFilterAttribute
    {
        private static Logger logger = Logger.CreateLogger(typeof(HandleCustomExceptionAttribute));
        public override void OnException(HttpActionExecutedContext filterContext)
        {
            var ex = filterContext.Exception;
            if (ex is LoginFaildException)
            {
                filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.OK, new AjaxReturnInfo(ex.Message));
            }
            else if (ex is BaseCustomException)
            {
                filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.NotImplemented, new AjaxReturnInfo(ex.Message));
            }
            else
                filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.ServiceUnavailable, new AjaxReturnInfo(ex.Message));
            logger.Error($"请求地址:{filterContext.Request.RequestUri}  发生错误:{ex}");
            base.OnException(filterContext);
        }
    }
}