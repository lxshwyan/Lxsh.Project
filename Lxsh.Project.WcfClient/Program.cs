

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Lxsh.Project.WcfClient.Lxsh.WcfService;      

namespace Lxsh.Project.WcfClient
{
    class Program
    {
        static void Main(string[] args)
        {
          

            // 删除不读默认配置文件及不要服务引用
          //  ChannelFactory<IEventAlarm> channelFactory = new ChannelFactory<IEventAlarm>(new WSDualHttpBinding(),
                                                          new EndpointAddress("http://localhost:8733/lxsh");
            //  var chanel = channelFactory.CreateChannel();
            //  chanel.DoWork();
            var instance = new InstanceContext(new MyLoginCallback());
            EventAlarmClient eventAlarmClient = new EventAlarmClient(instance);
            eventAlarmClient.Login("mary" + Guid.NewGuid().ToString());

            Console.ReadKey();

        }
        public class MyLoginCallback : Lxsh.WcfService.IEventAlarmCallback
        {  
            public void Notify1(string msg)
            {
                Console.WriteLine(msg);
            }

            public void Notify2(string msg)
            {
                Console.WriteLine(msg);
            }
        }
    }
}
