/*
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 *                                                                                 *
 * Copyright (c) 2019 Company Name                                                 *
 *                                                                                 *
 * Author Lxsh                                                                     *
 *                                                                                 *
 * Time 2020-08-20 18:00:30                                                                     *
 *                                                                                 *
 * Describe                                                                        *
 *                                                                                 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 */
using Microsoft.AspNetCore.Builder;

namespace Lxsh.Project.NetCoreWebApi.Middlewares
{
    public static class MiddlewareHelpers
    {
       

        /// <summary>
        /// IP请求中间件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseIPLogMildd(this IApplicationBuilder app)
        {
            return app.UseMiddleware<IPLogMildd>();
        }
        /// <summary>
        /// 用户访问中间件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseRecordAccessLogsMildd(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RecordAccessLogsMildd>();
        }
        /// <summary>
        /// 请求响应中间件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseReuestResponseLog(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequRespLogMildd>();
        }
        /// <summary>
        /// 异常处理中间件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseExceptionHandlerMidd(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlerMidd>();
        }

        /// <summary>
        /// api过期验证中间件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseExpirationTimeMidd(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExpirationTimeMildd>();
        }
        /// <summary>
        /// websocket
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseWebsocketMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<WebsocketMiddleware>();
        }
    }
}
