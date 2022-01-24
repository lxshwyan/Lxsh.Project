using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Lxsh.Project.NetCoreWebApi.Middlewares
{
    public static class MiddlewareExtensions
    {
        private static readonly IEnumerable<MiddlewareRegisterInfo> _middlewareRegisterInfos = GetMiddlewareRegisterInfos();
        public static IServiceCollection AddMiddlewares(this IServiceCollection services)
        {
            foreach (var middlewareRegisterInfo in _middlewareRegisterInfos)
            {
                switch (middlewareRegisterInfo.Lifetime)
                {
                    case ServiceLifetime.Singleton:
                        services.AddSingleton(middlewareRegisterInfo.MiddlewareRegisterType);
                        break;
                    case ServiceLifetime.Transient:
                        services.AddTransient(middlewareRegisterInfo.MiddlewareRegisterType);
                        break;
                    default:
                        services.AddScoped(middlewareRegisterInfo.MiddlewareRegisterType);
                        break;
                }
            }

            return services;
        }
        public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder applicationBuilder)
        {
            foreach (var middlewareRegisterInfo in _middlewareRegisterInfos)
            {
                applicationBuilder.UseMiddleware(middlewareRegisterInfo.MiddlewareRegisterType);
            }

            return applicationBuilder;
        }
        public static List<MiddlewareRegisterInfo> GetMiddlewareRegisterInfos()
        {
            var middlewareRegisterInfos = new List<MiddlewareRegisterInfo>();
            var assemblies = new Assembly[] { typeof(Startup).Assembly };
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes().Where(x => !x.IsAbstract))
                {
                    var attribute = type.GetCustomAttribute<MiddlewareRegisterAttribute>();

                    if (attribute != null)
                    {
                        middlewareRegisterInfos.Add(new MiddlewareRegisterInfo(type, attribute));
                    }
                }
            }
            return middlewareRegisterInfos.OrderBy(p => p.Sort).ToList();

        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class MiddlewareRegisterAttribute:Attribute
    {
        [Description("排序")]
        public int Sort { get; set; } = int.MaxValue;
        [Description("生命周期")]
        public ServiceLifetime Lifetime { get; set; } = ServiceLifetime.Scoped;
    }
    public class MiddlewareRegisterInfo 
    {
        public MiddlewareRegisterInfo(Type type, MiddlewareRegisterAttribute attribute)
        {
            this.MiddlewareRegisterType = type;
            this.Sort = attribute.Sort;
            this.Lifetime = attribute.Lifetime;
        }
        [Description("中间件注册类型")]
        public Type MiddlewareRegisterType { get; set; }
        [Description("排序")]
        public int Sort { get; set; } = int.MaxValue;
        [Description("生命周期")]
        public ServiceLifetime Lifetime { get; set; } = ServiceLifetime.Scoped;
    }

}
