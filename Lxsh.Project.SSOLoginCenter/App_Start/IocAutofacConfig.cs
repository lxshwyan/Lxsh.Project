using Autofac;
using Autofac.Integration.Mvc;
using Lxsh.Project.Bussiness.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Lxsh.Project.SSOLoginCenter
{
    public  class IocAutofacConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            //自动注册控制器
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();//把当前程序集中的Controller 都注册 

            // Assembly asmService = Assembly.Load("Lxsh.Project.Bussiness.Service");
            Assembly[] asmService = new Assembly[] { Assembly.Load("Lxsh.Project.Bussiness.Service") };
            builder.RegisterAssemblyTypes(asmService).Where(type => !type.IsAbstract
                    && typeof(IBaseService).IsAssignableFrom(type))
                    .AsImplementedInterfaces().PropertiesAutowired();
            //Assign：赋值
            //type1.IsAssignableFrom(type2);type1类型的变量是否可以指向type2类型的对象
            //换一种说法：type2是否实现了type1接口/type2是否继承自type1
            //typeof(IBaseService).IsAssignableFrom(type)IBaseService
            //避免其他无关的类注册到AutoFac中    
            var container = builder.Build();
            //注册系统级别的DependencyResolver，这样当MVC框架创建Controller等对象的时候都是管Autofac要对象。
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));  
        }
    }
}