using Lxsh.Project.Common.RabbitMQ;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Lxsh.Project.SignalR.Demo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RabbitMsgTest();
        }
         static void RabbitMsgTest()
        {
           
            MQHelper tMQHelper = MQHelperFactory.CreateBus("host=127.0.0.1:5672;virtualHost=/;username=lxsh;password=123456");   
            tMQHelper.TopicSubscribe(Guid.NewGuid().ToString(), s =>
                {
                   Console.WriteLine("当前收到信息：" + s.Body.FromJson<SystemMessage>().Content);
                 
                    var hub = GlobalHost.ConnectionManager.GetHubContext<MyConnection>();
                    foreach (var connectionID in MyConnection.list)
                    {

                        if (hub != null && connectionID != null)
                        {
                            hub.Clients.Client(connectionID)
                                .notice("当前收到信息：" + s.Body.FromJson<SystemMessage>().Content);
                        }
                    }

                }, true, CategoryMessage.System.ToString() + ".*", CategoryMessage.Alarm.ToString() + ".*");

            Console.WriteLine("Please enter a message. 'Quit' to quit.");
           
        }
    }
}
