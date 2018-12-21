using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace Ruanmou.MVC5.Utility.Pipeline
{
    /// <summary>
    /// 扩展IRouteHandler，
    /// 扩展IHttpHandler
    /// </summary>
    public class ElevenRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new ElevenHttpHandler(requestContext);
        }
    }

    /// <summary>
    /// 还是我们熟悉的handler
    /// </summary>
    public class ElevenHttpHandler : IHttpHandler
    {
        public ElevenHttpHandler(RequestContext requestContext)
        {
            Console.WriteLine("构造ElevenHandler");
        }

        public void ProcessRequest(HttpContext context)
        {
            string url = context.Request.Url.AbsoluteUri;
            context.Response.Write(string.Format("这里是Eleven定制：{0}", this.GetType().Name));
            context.Response.Write((string.Format("当前地址为：{0}", url)));

            context.Response.End();
        }

        public virtual bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}