using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using static Lxsh.Project.NetCoreWebApi.Middlewares.MiddlewareExtensions;

namespace Lxsh.Project.NetCoreWebApi.Middlewares
{
    [MiddlewareRegister(Sort =1)]
    public class ExceptionHandlerMidd
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMidd> _logger;

        public ExceptionHandlerMidd(RequestDelegate next, ILogger<ExceptionHandlerMidd> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            if (e == null) return;

            _logger.LogError(e.GetBaseException().ToString());

            await WriteExceptionAsync(context, e).ConfigureAwait(false);
        }

        private static async Task WriteExceptionAsync(HttpContext context, Exception e)
        {


            context.Response.ContentType = "application/json";
            var result = "{\"status\":0,\"data\":\"" + e.Message + "\",\"msg\":\"" + e.Message + "\"}";
            await context.Response.WriteAsync(result).ConfigureAwait(false);
            //await context.Response.WriteAsync(new JResult(106, null, e.Message.ToString()).ToString()).ConfigureAwait(false);
        }
    }
}
