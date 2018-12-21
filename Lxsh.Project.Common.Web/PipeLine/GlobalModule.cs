using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Lxsh.Project.Common.Web.PipeLine
{
    /// <summary>
    /// 在每处理一个Http请求时，应用程序事件都会触发一遍，但是Application_Start和 Application_End 例外，它仅在第一个资源文件被访问时被触发。
    /// Http Module无法注册和响应Session事件，对于Session_Start 和 Session_End，只能通过Glabal.asax来处理。
    /// </summary>
    public class GlobalModule : IHttpModule
    {
        public event EventHandler GlobalModuleEvent;

        /// <summary>
        /// Init方法仅用于给期望的事件注册方法
        /// </summary>
        /// <param name="httpApplication"></param>
        public void Init(HttpApplication httpApplication)
        {
            httpApplication.BeginRequest += new EventHandler(context_BeginRequest);//Asp.net处理的第一个事件，表示处理的开始

            httpApplication.EndRequest += new EventHandler(context_EndRequest);//本次请求处理完成
        }

        // 处理BeginRequest 事件的实际代码
        void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpContext context = application.Context;
            string extension = Path.GetExtension(context.Request.Url.AbsoluteUri);
            if (string.IsNullOrWhiteSpace(extension) && !context.Request.Url.AbsolutePath.Contains("Verify"))
                context.Response.Write(string.Format("<h1 style='color:#00f'>来自GlobalModule 的处理，{0}请求到达</h1><hr>", DateTime.Now.ToString()));

            //处理地址重写
            if (context.Request.Url.AbsolutePath.Equals("/Pipe/Some", StringComparison.OrdinalIgnoreCase))
                context.RewritePath("/Pipe/Handler");

            if (GlobalModuleEvent != null)
                GlobalModuleEvent.Invoke(this, e);
        }

        // 处理EndRequest 事件的实际代码
        void context_EndRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpContext context = application.Context;
            string extension = Path.GetExtension(context.Request.Url.AbsoluteUri);
            if (string.IsNullOrWhiteSpace(extension) && !context.Request.Url.AbsolutePath.Contains("Verify"))
                context.Response.Write(string.Format("<hr><h1 style='color:#f00'>来自GlobalModule的处理，{0}请求结束</h1>", DateTime.Now.ToString()));
        }

        public void Dispose()
        {
        }
    }
}