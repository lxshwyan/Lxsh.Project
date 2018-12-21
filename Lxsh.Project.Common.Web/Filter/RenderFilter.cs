using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Lxsh.Project.Common.Web.Filter
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RenderFilter : ActionFilterAttribute
    {
        /// <summary>
        /// render之前
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            HttpContextBase context = filterContext.HttpContext;
            string extension = Path.GetExtension(context.Request.Url.AbsoluteUri);
            if (string.IsNullOrWhiteSpace(extension) && !context.Request.Url.AbsolutePath.Contains("Verify"))
                context.Response.Write(string.Format("<h1 style='color:#00f'>来自OnResultExecuting 的处理，{0}请求到达</h1><hr>", DateTime.Now.ToString()));
            base.OnResultExecuting(filterContext);
        }

        /// <summary>
        /// render之后
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {

            HttpContextBase context = filterContext.HttpContext;
            string extension = Path.GetExtension(context.Request.Url.AbsoluteUri);
            if (string.IsNullOrWhiteSpace(extension) && !context.Request.Url.AbsolutePath.Contains("Verify"))
                context.Response.Write(string.Format("<h1 style='color:#00f'>来自OnResultExecuted 的处理，{0}请求结束</h1><hr>", DateTime.Now.ToString()));
            base.OnResultExecuted(filterContext);
        }
    }
}
