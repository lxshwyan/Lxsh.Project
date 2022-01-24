using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Lxsh.Project.NetCoreWebApi.Middlewares.MiddlewareExtensions;

namespace Lxsh.Project.NetCoreWebApi.Middlewares
{
    [MiddlewareRegister(Sort = 2)]
    /// <summary>
    /// 中间件
    /// 验证api过期时间
    /// </summary>
    public class ExpirationTimeMildd
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly RequestDelegate _next;

        private readonly ILogger<ExpirationTimeMildd> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public ExpirationTimeMildd(RequestDelegate next, ILogger<ExpirationTimeMildd> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var expirationTime = Appsettings.app("Middleware", "ExpirationTime", "Time");
            if (!string.IsNullOrWhiteSpace(expirationTime))
            {
                var time = Convert.ToDateTime(expirationTime);
                var nowTime = DateTime.Now;
                if (time < nowTime)
                {
                    context.Response.ContentType = "application/json";
                    var result = "{\"status\":160,\"data\":\"Api已过期\",\"msg\":\"Api已过期\"}";
                    _logger.LogError($"Api已过期！到期时间：{expirationTime}");
                    await context.Response.WriteAsync(result).ConfigureAwait(false);
                }
                await _next(context);
            }
            else
            {
                await _next(context);
            }
        }
    }
}
