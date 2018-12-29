

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Lxsh.Project.WcfServiceLib;

namespace Lxsh.Project.WcfClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //UserInfoClient user = new UserInfoClient();  
            //user.DoWork();
            //var list = user.GetString().ToList<Student>();
           // EventAlarmClient eventAlarmClient = new EventAlarmClient();
           // eventAlarmClient.DoWork();

           //删除不读默认配置文件及不要服务引用
            ChannelFactory<IEventAlarm> channelFactory = new ChannelFactory<IEventAlarm>(new BasicHttpBinding(),
                                                          new EndpointAddress("http://localhost:8733/lxsh"));
            var chanel = channelFactory.CreateChannel();
            chanel.DoWork();

            Console.ReadKey();

        }
    }
}
