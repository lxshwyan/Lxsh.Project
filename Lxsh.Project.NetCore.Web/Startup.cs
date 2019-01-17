using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lxsh.Project.NetCore.Web.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace Lxsh.Project.NetCore.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            env.ConfigureNLog("NLog.config");
            Configuration = configuration;
            var appconfig = configuration.Get<Appconfig>();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.Configure<Appconfig>("appconfig",Configuration);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env ,ILoggerFactory factory)
        {
            var logger = factory.CreateLogger("test");
            logger.LogError("B after");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.LuckyCoreFile();
            app.Map(new PathString("/filter"), builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.ContentType = "text/plain; charset=utf-8";
                    await context.Response.WriteAsync("测试Map");

                }); 
            });
            //app.Map(new PathString("/filter"), (builder) =>
            //{
            //    builder.Run(async (context) =>
            //    {
            //        logger.LogInformation("B before");

            //        await context.Response.WriteAsync("当前是filter的逻辑!");

            //        logger.LogInformation("B after");
            //    });
            //});

            ////更加灵活
            //app.MapWhen(context => context.Request.Path.Value.StartsWith("/filterwhen"), (builder) =>
            //{
            //    builder.Run(async (context) =>
            //    {
            //        logger.LogInformation("C before");

            //        await context.Response.WriteAsync("当前是 filterwhen 的逻辑!");

            //        logger.LogInformation("C after");
            //    });
            //});

            app.UseWhen(context => context.Request.Path.Value.StartsWith("/usewhen"), (builder) =>
            {
                //builder.Run(async (context) =>
                //{
                //    logger.LogInformation("D before");

                //    await context.Response.WriteAsync("当前是 usewhen 的逻辑!");

                //    logger.LogInformation("D after");
                //});

                //我不返回，我只往后面传
                builder.Use(async (context, next) =>
                {
                    logger.LogInformation("E before");
                    await next();
                    logger.LogInformation("E after");
                });
            });

            //app.MapWhen(context => context.Request.Path.Value.StartsWith("/usewhen"), (builder) =>
            //{
            //    //builder.Run(async (context) =>
            //    //{
            //    //    logger.LogInformation("D before");

            //    //    await context.Response.WriteAsync("当前是 usewhen 的逻辑!");

            //    //    logger.LogInformation("D after");
            //    //});

            //    //我不返回，我只往后面传
            //    builder.Use(async (context, next) =>
            //    {
            //        logger.LogInformation("E before");
            //        await next();
            //        logger.LogInformation("E after");
            //    });
            //});
            app.UseStaticFiles();


            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
