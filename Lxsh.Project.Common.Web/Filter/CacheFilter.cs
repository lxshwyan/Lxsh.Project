using System;
using System.Web;
using System.Web.Mvc;

namespace Lxsh.Project.Common.Web.Filter
{
    /// <summary>
    /// http://weblogs.asp.net/rashid/asp-net-mvc-action-filter-caching-and-compression
    /// </summary>
    public class CacheFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 缓存时长 单位s
        /// </summary>
        private int _MaxSecond = 0;
        public CacheFilterAttribute(int duration)
        {
            this._MaxSecond = duration;
        }
        public CacheFilterAttribute()
        {
           this._MaxSecond = 60;
        }
        /// <summary>
        /// action执行后
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (this._MaxSecond <= 0) return;

            HttpCachePolicyBase cache = filterContext.HttpContext.Response.Cache;
            TimeSpan cacheDuration = TimeSpan.FromSeconds(this._MaxSecond);

            cache.SetCacheability(HttpCacheability.Public);
            //cache.SetLastModified(DateTime.Now.AddHours(8).Add(cacheDuration));
            //cache.SetExpires(DateTime.Now.AddHours(8).Add(cacheDuration));//GMT时间 格林威治时间 
            cache.SetExpires(DateTime.Now.Add(cacheDuration));
            cache.SetMaxAge(cacheDuration);
            cache.AppendCacheExtension("must-revalidate, proxy-revalidate");
        }
    }
}