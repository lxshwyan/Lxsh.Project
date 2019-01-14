using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Lxsh.Project.CoreWebApi.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Lxsh.Project.CoreWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #region Swagger UI Service API文档服务
            services.AddScoped<SwaggerGenerator>();//GetSwagger获取swagger.json的核心代码在这里面，这里我们用ioc容器存储对象，后面直接调里面的获取json的方法。
            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            services.AddSwaggerGen(c =>
            {
                //遍历出全部的版本，做文档信息展示
                typeof(CustomApiVersion.ApiVersions).GetEnumNames().ToList().ForEach(version =>
                {
                    c.SwaggerDoc(version, new Info
                    {
                        Version = version,
                        Title = $"{(Configuration.GetSection("Swagger"))["ProjectName"]} WebAPI",
                        Description = $"{(Configuration.GetSection("Swagger"))["ProjectName"]} HTTP WebAPI " + version + "，博客系统前后端分离，后端框架。",
                        TermsOfService = "None",
                        Contact = new Contact { Name = "Lxsh", Email = "327251174@qq.com", Url = "http://327251174.cn/" }
                    });
                });
                var xmlPath1 = Path.Combine(basePath, "Lxsh.Project.CoreWebApi.xml");//这个就是刚刚配置的xml文件名
                c.IncludeXmlComments(xmlPath1, true);//默认的第二个参数是false，这个是controller的注释，记得修改
             //   var xmlPath2 = Path.Combine(basePath, "Lxsh.Project.CoreWebApi.xml");//这个就是Model层的xml文件名
             //   c.IncludeXmlComments(xmlPath2);
            //    var xmlPath3 = Path.Combine(basePath, "Lxsh.Project.CoreWebApi.xml");//这个就是Model层的xml文件名
               // c.IncludeXmlComments(xmlPath3);

                #region Token绑定到ConfigureServices
                //添加header验证信息
                //c.OperationFilter<SwaggerHeader>();
                // 发行人
              //  var issuerName = (Configuration.GetSection("Audience"))["Issuer"];
              //  var security = new Dictionary<string, IEnumerable<string>> { { issuerName, new string[] { } }, };
            //    c.AddSecurityRequirement(security);

                //方案名称“Blog.WebAPP”可自定义，上下一致即可
                //c.AddSecurityDefinition(issuerName, new ApiKeyScheme
                //{
                //    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer token（注意两者之间是一个空格）\"",
                //    Name = "Authorization",//jwt默认的参数名称
                //    In = "header",//jwt默认存放Authorization信息的位置(请求头中)
                //    Type = "apiKey"
                //});
                #endregion

                #region Swagger参数自定义
              //  c.OperationFilter<SwaggerUploadFileFilter>();//文件上传参数
                #endregion

                #region Swagger文档过滤
             //   c.DocumentFilter<RemoveBogusDefinitionsDocumentFilter>();//过滤model
                #endregion

            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            #region Swagger配置
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //之前是写死的
                //c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
                //c.RoutePrefix = "";//路径配置，设置为空，表示直接在根域名（localhost:8001）访问该文件,注意localhost:8001/swagger是访问不到的，去launchSettings.json把launchUrl去掉

                //根据版本名称倒序 遍历展示
                typeof(CustomApiVersion.ApiVersions).GetEnumNames().OrderByDescending(e => e).ToList().ForEach(version =>
                {
                    c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{(Configuration.GetSection("Swagger"))["" + "ProjectName" + ""]} {version}");
                });
                // Display
                c.DefaultModelExpandDepth(2);
                c.DefaultModelRendering(ModelRendering.Model);
                c.DefaultModelsExpandDepth(-1);//不显示model
                c.DisplayOperationId();
                c.DisplayRequestDuration();
                c.DocExpansion(DocExpansion.None);
                //c.EnableDeepLinking();
                //c.EnableFilter();
                c.ShowExtensions();

                // Network
                //c.EnableValidator(null);//会导致页面右下角Error
                //c.SupportedSubmitMethods(SubmitMethod.Get);

                // Other
                c.DocumentTitle = "Lxsh.Project.CoreWebApi 在线文档调试";
                //css注入
                c.InjectStylesheet("/swagger-common.css");//自定义样式
                c.InjectStylesheet("/buzyload/app.min.css");//等待load遮罩层样式
                //js注入
                c.InjectJavascript("/jquery/jquery.js");//jquery 插件
                c.InjectJavascript("/buzyload/app.min.js");//loading 遮罩层js
                c.InjectJavascript("/swagger-lang.js");//我们自定义的js


            });
            #endregion

            // 使用静态文件
            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
