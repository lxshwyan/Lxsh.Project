using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Lxsh.Project.CoreAPI
{
    public class Startup
    {

        #region 仓储 --Log4Net、.Net Core Configuration
        /// <summary>
        /// log4net 仓储库
        /// </summary>
        //public static ILoggerRepository Repository { get; set; }
        public IConfiguration Configuration { get; }

        #endregion

        #region .Net Core 启动
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion

        #region .Net Core 配置服务注入  
        // This method gets called by the runtime. Use this method to add services to the container.
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        //}
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

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
                        Contact = new Contact { Name = "韩俊俊", Email = "1454055505@qq.com", Url = "http://gaobili.cn/" }
                    });
                });
                var xmlPath1 = Path.Combine(basePath, "Titan.Blog.WebAPP.xml");//这个就是刚刚配置的xml文件名
                c.IncludeXmlComments(xmlPath1, true);//默认的第二个参数是false，这个是controller的注释，记得修改
                var xmlPath2 = Path.Combine(basePath, "Titan.Blog.AppService.xml");//这个就是Model层的xml文件名
                c.IncludeXmlComments(xmlPath2);
                var xmlPath3 = Path.Combine(basePath, "Titan.Blog.Model.xml");//这个就是Model层的xml文件名
                c.IncludeXmlComments(xmlPath3);

                #region Token绑定到ConfigureServices
                //添加header验证信息
                //c.OperationFilter<SwaggerHeader>();
                // 发行人
                var issuerName = (Configuration.GetSection("Audience"))["Issuer"];
                var security = new Dictionary<string, IEnumerable<string>> { { issuerName, new string[] { } }, };
                c.AddSecurityRequirement(security);

                //方案名称“Blog.WebAPP”可自定义，上下一致即可
                c.AddSecurityDefinition(issuerName, new ApiKeyScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer token（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = "header",//jwt默认存放Authorization信息的位置(请求头中)
                    Type = "apiKey"
                });
                #endregion


                #region AutoFac
                ////实例化 AutoFac  容器   
                var builder = new ContainerBuilder();
            ////注册要通过反射创建的组件
            ////builder.RegisterType<AdvertisementServices>().As<IAdvertisementServices>();
            //builder.RegisterType<BlogCacheAOP>();//可以直接替换其他拦截器
            //                                     //builder.RegisterType<AuthorDomainSvc>();//可以直接替换其他拦截器
            //                                     //var assemblysServices1 = Assembly.Load("Blog.Core.Services");
            //                                     //将services填充到Autofac容器生成器中
            //builder.Populate(services);
            ////获取当前应用程序加载程序集（C/S应用中使用）
            ////var assembly = Assembly.GetExecutingAssembly();
            ////builder.RegisterAssemblyTypes(assembly); //注册所有程序集类定义的非静态类型
            //builder.RegisterType<Permission>();
            //builder.RegisterType<SigningCredentials>();
            //builder.RegisterType<TimeSpan>();
            ////builder.RegisterType<AuthorDomainSvc>();
            ////builder.RegisterType<AuthorSvc>();
            ////builder.RegisterType<ModelRespositoryFactory<Author, Guid>>();

            //// ※※★※※ 如果你是第一次下载项目，请先F6编译，然后再F5执行，※※★※※
            //// ※※★※※ 因为解耦，bin文件夹没有以下两个dll文件，会报错，所以先编译生成这两个dll ※※★※※
            //builder.RegisterType<ModelRespositoryFactory<SysRoleModuleButton, Guid>>();
            //builder.RegisterType<ModelRespositoryFactory<SysRole, Guid>>();
            //var repositoryDllFile = Path.Combine(basePath, "Titan.Blog.Repository.dll");
            ////var assemblysRepository = Assembly.LoadFile(repositoryDllFile);//Assembly.Load("Titan.Blog.Repository");
            //var assemblysRepository = Assembly.Load("Titan.Blog.Repository");
            //builder.RegisterAssemblyTypes(assemblysRepository);

            //var servicesDllFile = Path.Combine(basePath, "Titan.Blog.AppService.dll");//获取项目绝对路径
            //                                                                          //var assemblysServices = Assembly.LoadFile(servicesDllFile);// Assembly.Load("Titan.Blog.AppService");//直接采用加载文件的方法
            //var assemblysServices = Assembly.Load("Titan.Blog.AppService");//直接采用加载文件的方法
            //builder.RegisterAssemblyTypes(assemblysServices);//指定已扫描程序集中的类型注册为提供所有其实现的接口。.InstancePerRequest()

            //var assemblysModel = Assembly.Load("Titan.Blog.Model");//直接采用加载文件的方法
            //builder.RegisterAssemblyTypes(assemblysModel);//指定已扫描程序集中的类型注册为提供所有其实现的接口。.InstancePerRequest()

            //var assemblysInfrastru = Assembly.Load("Titan.Blog.Infrastructure");//直接采用加载文件的方法
            //builder.RegisterAssemblyTypes(assemblysInfrastru);//指定已扫描程序集中的类型注册为提供所有其实现的接口。.InstancePerRequest()

            //////builder.RegisterAssemblyTypes(assemblysServices)
            //////         .AsImplementedInterfaces()
            //////         .InstancePerLifetimeScope()
            //////         .EnableInterfaceInterceptors()//引用Autofac.Extras.DynamicProxy;
            //////         .InterceptedBy(typeof(BlogCacheAOP));//允许将拦截器服务的列表分配给注册。可以直接替换其他拦截器

            ////var infrastructureDllFile = Path.Combine(basePath, "Titan.Blog.Infrastructure.dll");//获取项目绝对路径
            ////var assemblysInfrastructure = Assembly.LoadFile(infrastructureDllFile);//直接采用加载文件的方法
            ////builder.RegisterAssemblyTypes(assemblysInfrastructure).AsSelf();//指定已扫描程序集中的类型注册为提供所有其实现的接口。

            ////var modelDllFile = Path.Combine(basePath, "Titan.Blog.Model.dll");//获取项目绝对路径
            ////var assemblysModel = Assembly.LoadFile(modelDllFile);//直接采用加载文件的方法
            ////builder.RegisterAssemblyTypes(assemblysModel).AsSelf();//指定已扫描程序集中的类型注册为提供所有其实现的接口。



            ////var aa = Path.Combine(basePath, "Titan.Blog.WebAPP.dll");
            ////var bb = Assembly.LoadFile(aa);
            ////builder.RegisterAssemblyTypes(bb);



            ////使用已进行的组件登记创建新容器
           var applicationContainer = builder.Build();

            ////获取容器内的对象
            ////var data = applicationContainer.ComponentRegistry.Registrations
            ////    .Where(x => x.Activator.LimitType.ToString().Contains("Titan.RepositoryCode")).ToList();
            ////var data1 = applicationContainer.ComponentRegistry.Registrations
            ////    .Where(x => x.Activator.LimitType.ToString().Contains("Titan.Blog.AppService")).ToList();

            ////var clas1 = applicationContainer.Resolve<AuthorDomainSvc>();
            ////Console.WriteLine(clas1.GetList());
            #endregion
            return new AutofacServiceProvider(applicationContainer);//第三方IOC接管 core内置DI容器
        }

            #endregion .Net Core 配置服务注入

            #region .Net Core 配置
            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
        #endregion
    }
}
