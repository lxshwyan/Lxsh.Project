using System.IO.Compression;
using System.Web;
using System.Web.Mvc;

namespace Lxsh.Project.Common.Web.Filter
{
    /// <summary>
    /// http://weblogs.asp.net/rashid/asp-net-mvc-action-filter-caching-and-compression
    /// http://www.cnblogs.com/QLeelulu/archive/2008/03/28/1127119.html
    /// </summary>
    public class CompressFilter : ActionFilterAttribute
    {
        //OnActionExecuting（）-> Action execute & return View() ->OnActionExecuted（） ->OnResultExecuting（） -> Render View() ->OnResultExecuted()

        /// <summary>
        /// action执行前
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpRequestBase request = filterContext.HttpContext.Request;

            string acceptEncoding = request.Headers["Accept-Encoding"];

            if (string.IsNullOrEmpty(acceptEncoding)) return;

            acceptEncoding = acceptEncoding.ToUpperInvariant();

            HttpResponseBase response = filterContext.HttpContext.Response;

            if (acceptEncoding.Contains("DEFLATE"))
            {
                response.AppendHeader("Content-encoding", "deflate");
                response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
            }
            else if (acceptEncoding.Contains("GZIP"))
            {
                response.AppendHeader("Content-encoding", "gzip");
                response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
            }

        }
    }
}