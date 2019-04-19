using Lxsh.Project.SignalRServer.Demo.AppStart;
using Lxsh.Project.SignalRServer.Demo.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace Lxsh.Project.SignalRServer.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请输入你要选择的发送端和接收端==1为发，2为收");
            string type = Console.ReadLine();
            if (type == "1")
            {

                HostFactory.Run(x =>
                {
                    x.Service<Startup>(s =>
                    {
                        s.ConstructUsing(name => new Startup());
                        s.WhenStarted(tc => tc.Start());
                        s.WhenStopped(tc => tc.Stop());
                    });
                    x.RunAsLocalSystem();

                    x.SetDescription("SFBR_SignalRServer");
                    x.SetDisplayName("SFBR_SignalRServer");
                    x.SetServiceName("SFBR_SignalRServer");
                });
            }
            else
            {
                HubClient.ClientTest();

            }

        }
    }
}
