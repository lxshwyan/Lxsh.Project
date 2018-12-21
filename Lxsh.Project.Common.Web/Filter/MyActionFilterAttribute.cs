using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lxsh.Project.Common.Web.Filter
{

    public class MyActionFilterAttribute : ActionFilterAttribute
    {
        #region Identity
        private Stopwatch timerAction = new Stopwatch();
        private Stopwatch timerResult = new Stopwatch();
        private bool _IsShow = false;
        public MyActionFilterAttribute(bool isShow = true)
        {
            this._IsShow = isShow;
        }
        #endregion
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (this._IsShow)
            {
                timerAction.Start();
                filterContext.HttpContext.Response.Write($"<h1 style='color:#00f'>这里是OnActionExecuting :{DateTime.Now.ToString("yyyyMMdd - HHmmss.fff")}</h1><hr>");
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (this._IsShow)
            {
                timerAction.Stop();
                string message = $"<h1 style='color:#00f'>这里是OnActionExecuted:{DateTime.Now.ToString("yyyyMMdd-HHmmss.fff")},耗时{timerAction.ElapsedMilliseconds}</h1><hr>";
                filterContext.HttpContext.Response.Write(message);
            }
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (this._IsShow)
            {
                timerResult.Start();
                filterContext.HttpContext.Response.Write("<h1 style='color:#00f'>这里是OnResultExecuting</h1><hr>");
            }
        }
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (this._IsShow)
            {
                timerResult.Stop();
                string message = $"<h1 style='color:#00f'>这里是OnResultExecuted:{DateTime.Now.ToString("yyyyMMdd - HHmmss.fff")},耗时 { timerResult.ElapsedMilliseconds}</h1><hr>";
                filterContext.HttpContext.Response.Write(message);
            }
        }
    }

}