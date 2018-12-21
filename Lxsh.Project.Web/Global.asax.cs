using Lxsh.Project.Common.Web.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Lxsh.Project.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {  
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ControllerBuilder.Current.SetControllerFactory(new UnityControllerFactory());//替换默认的控制器工厂
        }
    }
}
