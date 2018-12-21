using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lxsh.Project.SSOLoginCenter
{
    public class AllowCrosAttribute : ActionFilterAttribute
    {
        private string[] _domains;
        public AllowCrosAttribute()
        {
            //_domains = new string[] { "", "" };
        }
        public AllowCrosAttribute(params string[] domain)
        {
            _domains = domain;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string domain = filterContext.HttpContext.Request.Headers.Get("Origin");
            var urlReferrer = filterContext.HttpContext.Request.UrlReferrer;
            if (urlReferrer != null)
            {
                var absolutePath = urlReferrer.OriginalString;
                var absolutePathFormat = absolutePath.Substring(0, absolutePath.Length - 1);
                //允许所有的
                if (_domains == null || _domains.Length == 0)
                {
                    filterContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", domain);
                }
                else if (_domains.Contains(domain))
                {
                    filterContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", domain);
                }
            }
            else
            {
                //如果urlReferrer为空，我理解为自己本地访问(亲自测试，本地访问urlReferrer为null)
                filterContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
            }
            filterContext.HttpContext.Response.AddHeader("Access-Control-Allow-Credentials", "true");
            filterContext.HttpContext.Response.AddHeader("Access-Control-Allow-Methods", "GET, HEAD, OPTIONS, POST, PUT");
            filterContext.HttpContext.Response.AddHeader("Access-Control-Allow-Headers", "Access-Control-Allow-Headers, Origin,Accept, X-Requested-With, Content-Type, Access-Control-Request-Method, Access-Control-Request-Headers");
            base.OnActionExecuting(filterContext);
        }
    }
}