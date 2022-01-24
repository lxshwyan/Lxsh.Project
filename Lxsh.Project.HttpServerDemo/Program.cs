using System;
using System.Collections.Generic;
using NewLife.Http;
using NewLife.Log;
using NewLife.Remoting;
namespace Lxsh.Project.HttpServerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
      //  public IDictionary<String, IHttpHandler> Routes { get; set; } = new Dictionary<String, IHttpHandler>(StringComparer.OrdinalIgnoreCase);
            //XTrace.UseConsole();
            //var server = new HttpServer
            //{
            //    Port=8080,
            //    Log=XTrace.Log,
            //    SessionLog=XTrace.Log
            //};
            //server.Map("/", () => "<h1>Hello NewLife!</h1></br> " + DateTime.Now.ToFullString() + "</br><img src=\"logos/leaf.png\" />");
            //server.Map("/user", (String act, Int32 uid) => new { code = 0, data = $"User.{act}({uid}) success!" });
            //server.MapStaticFiles("/logos", "images/");
            //server.MapController<ApiController>("/api");
          

            //server.Map("/my", new MyHttpHandler());
            //server.Start();

            //Console.ReadLine();
        }
    }
    class MyHttpHandler : IHttpHandler
    {
        public void ProcessRequest(IHttpContext context)
        {
            var name = context.Parameters["name"];
            //var html = $"<h2>你好，<span color=\"red\">{name}</span></h2>";
            var html = "你好";
          context.Response.SetResult(html);
        }
    }/// <summary>Http处理器</summary>
    public interface IHttpHandler
    {
        /// <summary>处理请求</summary>
        /// <param name="context"></param>
        void ProcessRequest(IHttpContext context);
    }
    /// <summary>Http请求处理委托</summary>
    /// <param name="context"></param>
    public delegate void HttpProcessDelegate(IHttpContext context);
    /// <summary>委托Http处理器</summary>
    public class DelegateHandler : IHttpHandler
    {
        /// <summary>委托</summary>
        public Delegate Callback { get; set; }
        public void ProcessRequest(IHttpContext context)
        {
            var handler = Callback;
            if (handler is HttpProcessDelegate httpHandler)
            {
                httpHandler(context);
            }
        }
    }
   }
