using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lxsh.Project.NetCoreWebApi.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Microsoft.OpenApi.Models;
using System.IO;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Lxsh.Project.NetCoreWebApi.Policy;
using Microsoft.AspNetCore.Authorization;

namespace Lxsh.Project.NetCoreWebApi
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
          
           #region MiniProfiler
            services.AddMiniProfiler(options =>
            {
                options.RouteBasePath = "/profiler";
            });
            #endregion

            //添加Swagger
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo  //版本要一致
                {
                    Version = "v1",    //版本要一致
                    Title = "Lanyp.App Swagger",
                    Description = "基于.NET Core 3.1 的Api Swagger",
                    Contact = new OpenApiContact { Name = "lxsh", Email = "", Url = new Uri("http://www.baidu.com") },
                    License = new OpenApiLicense { Name = "lxsh许可证", Url = new Uri("http://www.baidu.com") }

                }); ; ;
                // 加载程序集的xml描述文档
                var baseDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
                //改文件名是从项目 “属性-->生成-->输出-->XML文档文件” 中得到的  或者为（System.AppDomain.CurrentDomain.FriendlyName + ".xml";）
                var xmlFile = "Lxsh.Project.NetCoreWebApi.xml";
                var xmlPath = Path.Combine(baseDirectory, xmlFile);
                option.IncludeXmlComments(xmlPath,true);  //获取控制的注释，如果为false不显示

                #region 加锁
                var openApiSecurity = new OpenApiSecurityScheme
                {
                    Description = "JWT认证授权，使用直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",  //jwt 默认参数名称
                    In = ParameterLocation.Header,  //jwt默认存放Authorization信息的位置（请求头）
                    Type = SecuritySchemeType.ApiKey
                };

               option.AddSecurityDefinition("oauth2", openApiSecurity);
               option.OperationFilter<AddResponseHeadersFilter>();
               option.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                //在header中添加token,传递到后台
                option.OperationFilter<SecurityRequirementsOperationFilter>();
                #endregion
            });
            services.AddControllers();
            #region 配置认证服务

            var Issurer = "lxsh.Auth";  //发行人
            var Audience = "api.auth";       //受众人
            var secretCredentials = "sifangboruiyanfayibu";   //密钥

            //配置认证服务
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o => {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    //是否验证发行人
                    ValidateIssuer = true,
                    ValidIssuer = Issurer,//发行人
                    //是否验证受众人
                    ValidateAudience = true,
                    ValidAudience = Audience,//受众人
                    //是否验证密钥
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretCredentials)),

                    ValidateLifetime = true, //验证生命周期
                    RequireExpirationTime = true, //过期时间
                };
            });
            #endregion
            services.AddScoped<IAuthorizationHandler, PermissionRequirementHandler>();
            //基于自定义策略授权
            services.AddAuthorization(options =>
            {
                options.AddPolicy("customizePermisson",
                  policy => policy
                    .Requirements
                    .Add(new PermissionRequirement("admin")));
            });
            services.AddSingleton(new Appsettings(Configuration));
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(op => {
                op.AllowAnyOrigin();
                op.AllowAnyMethod();
                op.AllowAnyHeader();
            });

            //api过期验证
            app.UseExpirationTimeMidd();
            // 静态文件路径
            app.UseStaticFilesMidd();
            // 记录请求与返回数据 
            app.UseReuestResponseLog();
            // 记录ip请求
            app.UseIPLogMildd();
            // 用户访问记录
            app.UseRecordAccessLogsMildd();
        
            app.UseMiniProfiler();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
         
            app.UseWebSockets(new WebSocketOptions()
            {
                KeepAliveInterval = TimeSpan.FromSeconds(60),
                ReceiveBufferSize = 2 * 1024
            });
            app.UseWebsocketMiddleware();
            // 开启异常中间件，要放到最后
            app.UseExceptionHandlerMidd();
            //启用中间件服务生成Swagger作为JSON终结点
            app.UseSwagger();
            //启用中间件服务对swagger-ui，指定Swagger JSON终结点
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint("/swagger/v1/swagger.json", "Lanyp.App version 1.0");   //版本要一致
            });
         
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
