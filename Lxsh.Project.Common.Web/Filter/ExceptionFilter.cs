using Lxsh.Project.Common.Log;
using Lxsh.Project.Common.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Lxsh.Project.Common.Web.Filter
{
    /// <summary>
    /// http://www.cnblogs.com/Showshare/p/exception-handle-with-mvcfilter.html
    /// LogExceptionAttribute设置了自己的AttributeUsage特性，
    /// AttributeTargets.Class指定该过滤器只能用于类一级，即Controller；
    /// AllowMultiple = false设置不允许多次执行，即仅在Controller级执行一次。
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class LogExceptionFilter : HandleErrorAttribute
    {
        private Logger logger = Logger.CreateLogger(typeof(LogExceptionFilter));
        public override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)//异常有没有被处理过
            {
                string controllerName = (string)filterContext.RouteData.Values["controller"];
                string actionName = (string)filterContext.RouteData.Values["action"];
                string msgTemplate = "在执行 controller[{0}] 的 action[{1}] 时产生异常";
                logger.Error(string.Format(msgTemplate, controllerName, actionName), filterContext.Exception);

                if (filterContext.HttpContext.Request.IsAjaxRequest())//检查请求头 是不是XMLHttpRequest
                {
                    filterContext.Result = new JsonResult()
                    {
                        Data = new AjaxResult()
                        {
                            Result = DoResult.Failed,
                            PromptMsg = "系统出现异常，请联系管理员",
                            DebugMessage = filterContext.Exception.Message
                        }//这个就是返回的结果
                    };
                }
                else
                {
                    filterContext.Result = new ViewResult()
                    {
                        ViewName = "~/Views/Shared/Error.cshtml",
                        ViewData = new ViewDataDictionary<string>(filterContext.Exception.Message)
                    };
                }
                filterContext.ExceptionHandled = true;
            }
        }
    }

    /******
     * 一般的过滤器执行顺序
IAuthorizationFilter->OnAuthorization(授权)
IActionFilter          ->OnActionExecuting(行为)
Action
IActionFilter          ->OnActionExecuted(行为) 
IResultFilter          ->OnResultExecuting(结果)
View
IResultFilter          ->OnResultExecuted(结果)
*IExceptionFilter    ->OnException(异常)，此方法并不在以上的顺序执行中，有异常发生时即会执行，有点类似于中断
当同时在Controller和Action中都设置了过滤器后，执行顺序一般是由外到里，即“全局”->“控制器”->“行为”
Controller->IAuthorizationFilter->OnAuthorization
Action     ->IAuthorizationFilter->OnAuthorization
Controller->IActionFilter          ->OnActionExecuting
Action     ->IActionFilter          ->OnActionExecuting
Action
Action     ->IActionFilter          ->OnActionExecuted
Controller->IActionFilter          ->OnActionExecuted
Controller->IResultFilter          ->OnResultExecuting
Action     ->IResultFilter          ->OnActionExecuting
Action     ->IResultFilter          ->OnActionExecuted
Controller->IResultFilter          ->OnActionExecuted
因为异常是从里往外抛，因次异常的处理顺序则刚好相反，一般是由里到外，即“行为”->“控制器”->“全局”
Action     ->IExceptionFilter->OnException
Controller->IExceptionFilter->OnException
     * ***********/

    public static class Process
    {
        /// <summary>
        /// 安全调用
        /// </summary>
        /// <param name="act"></param>
        /// <returns>true 正常完成  false 异常</returns>
        public static bool SafeInvoke(this Action act)
        {
            try
            {
                act.Invoke();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {

            }
        }
    }
}