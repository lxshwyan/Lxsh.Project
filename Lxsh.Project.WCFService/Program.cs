using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using Lxsh.Project.WcfServiceLib;

namespace Lxsh.Project.WcfServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //////删除不读默认配置文件
            //ServiceHost host = new ServiceHost(typeof(EventAlarmInfo), new Uri("http://localhost:8733/Lxsh"));

            ////2. 添加endpoint
            //host.AddServiceEndpoint(typeof(IEventAlarm), new WSDualHttpBinding(), "");

            ////3.添加behaviors
            //var serviceMeta = new ServiceMetadataBehavior()
            //{
            //    HttpGetEnabled = true,
            //    HttpsGetEnabled = true
            //};

            //var serviceDebug = new ServiceDebugBehavior()
            //{
            //    IncludeExceptionDetailInFaults = false
            //};

            //host.Description.Behaviors.Add(serviceMeta);
            ////host.Description.Behaviors.Add(serviceDebug);

            ////4. 添加mex端点
            //host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(), "mex");

            ServiceHost host = new ServiceHost(typeof(EventAlarmInfo));
            host.Closed += Host_Closed;
           
            host.Open();

            Console.WriteLine("wcf启动成功！");

            Task.Run(() => EventAlarmInfo.Modify());

            System.Threading.Thread.Sleep(int.MaxValue);

          
        }

        private static void Host_Closed(object sender, EventArgs e)
        {
           
        }
    }
}
