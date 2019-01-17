using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.NetCore.Web
{
    public class LuckyCoreFileMiddleware
    {
        private readonly RequestDelegate _next;
        public LuckyCoreFileMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.Value;
            if (path.EndsWith("lxsh"))
            {
                context.Response.ContentType = "text/plain; charset=utf-8";
                return context.Response.WriteAsync("您好，北京欢迎你");
                // var mypath = path.TrimStart('/');
                //  return context.Response.SendFileAsync(mypath);
            }

            var task = this._next(context);

            return task;
        }
    }
}
