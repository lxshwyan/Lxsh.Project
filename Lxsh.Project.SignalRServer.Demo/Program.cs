using Lxsh.Project.SignalRServer.Demo.AppStart;
using Lxsh.Project.SignalRServer.Demo.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.SignalRServer.Demo
{
    class Program
    {
        static void Main(string[] args)
        {

            string input = "";
            string strErrorMsg = "";
            Console.WriteLine("请输入你要选择的服务端和客户端==1服务端，2为客户端");
            string type = Console.ReadLine();
            if (type == "1")
            {
                Startup.Start();
            }
            else
            {
                HubClient.ClientTest();
            }
        }
    }
}
