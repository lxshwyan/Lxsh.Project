using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Lxsh.Project.SignalR.Demo.Startup1))]

namespace Lxsh.Project.SignalR.Demo
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkID=316888


            app.MapSignalR("/myhub", new Microsoft.AspNet.SignalR.HubConfiguration());
        }
    }
}
