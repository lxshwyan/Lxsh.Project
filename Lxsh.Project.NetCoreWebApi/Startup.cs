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

            //���Swagger
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo  //�汾Ҫһ��
                {
                    Version = "v1",    //�汾Ҫһ��
                    Title = "Lanyp.App Swagger",
                    Description = "����.NET Core 3.1 ��Api Swagger",
                    Contact = new OpenApiContact { Name = "lxsh", Email = "", Url = new Uri("http://www.baidu.com") },
                    License = new OpenApiLicense { Name = "lxsh���֤", Url = new Uri("http://www.baidu.com") }

                }); ; ;
                // ���س��򼯵�xml�����ĵ�
                var baseDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
                //���ļ����Ǵ���Ŀ ������-->����-->���-->XML�ĵ��ļ��� �еõ���  ����Ϊ��System.AppDomain.CurrentDomain.FriendlyName + ".xml";��
                var xmlFile = "Lxsh.Project.NetCoreWebApi.xml";
                var xmlPath = Path.Combine(baseDirectory, xmlFile);
                option.IncludeXmlComments(xmlPath,true);  //��ȡ���Ƶ�ע�ͣ����Ϊfalse����ʾ

                #region ����
                var openApiSecurity = new OpenApiSecurityScheme
                {
                    Description = "JWT��֤��Ȩ��ʹ��ֱ�����¿�������Bearer {token}��ע������֮����һ���ո�\"",
                    Name = "Authorization",  //jwt Ĭ�ϲ�������
                    In = ParameterLocation.Header,  //jwtĬ�ϴ��Authorization��Ϣ��λ�ã�����ͷ��
                    Type = SecuritySchemeType.ApiKey
                };

               option.AddSecurityDefinition("oauth2", openApiSecurity);
               option.OperationFilter<AddResponseHeadersFilter>();
               option.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                //��header�����token,���ݵ���̨
                option.OperationFilter<SecurityRequirementsOperationFilter>();
                #endregion
            });
            services.AddControllers();
            #region ������֤����

            var Issurer = "lxsh.Auth";  //������
            var Audience = "api.auth";       //������
            var secretCredentials = "sifangboruiyanfayibu";   //��Կ

            //������֤����
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o => {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    //�Ƿ���֤������
                    ValidateIssuer = true,
                    ValidIssuer = Issurer,//������
                    //�Ƿ���֤������
                    ValidateAudience = true,
                    ValidAudience = Audience,//������
                    //�Ƿ���֤��Կ
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretCredentials)),

                    ValidateLifetime = true, //��֤��������
                    RequireExpirationTime = true, //����ʱ��
                };
            });
            #endregion
            services.AddScoped<IAuthorizationHandler, PermissionRequirementHandler>();
            //�����Զ��������Ȩ
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

            //api������֤
            app.UseExpirationTimeMidd();
            // ��̬�ļ�·��
            app.UseStaticFilesMidd();
            // ��¼�����뷵������ 
            app.UseReuestResponseLog();
            // ��¼ip����
            app.UseIPLogMildd();
            // �û����ʼ�¼
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
            // �����쳣�м����Ҫ�ŵ����
            app.UseExceptionHandlerMidd();
            //�����м����������Swagger��ΪJSON�ս��
            app.UseSwagger();
            //�����м�������swagger-ui��ָ��Swagger JSON�ս��
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint("/swagger/v1/swagger.json", "Lanyp.App version 1.0");   //�汾Ҫһ��
            });
         
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
