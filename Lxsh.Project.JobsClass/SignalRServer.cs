using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Host.HttpListener;
using Microsoft.AspNet.SignalR.Client;
using System.Threading;

namespace Lxsh.Project.JobsClass
{
    public class SignalRServer
    {
        public static HubConnection Connection { get; set; }

        //定义代理,广播服务连接相关
        private static IHubProxy HubProxy { get; set; }
        private static string ServerUrl = "http://192.168.137.110:6178/signalr";
        public static void Start(string SignalRURI = "http://localhost:6178")
        {
            ServerUrl = SignalRURI;
            try
            {
                try
                {
                    using (WebApp.Start(SignalRURI, builder =>
                    {
                        builder.Map("/signalr", map =>
                        {
                            map.UseCors(CorsOptions.AllowAll);
                            var hubConfiguration = new HubConfiguration
                            {
                                EnableJSONP = true
                            };
                            map.RunSignalR(hubConfiguration);
                        });
                        builder.MapSignalR();

                    }))
                    {
                        Console.WriteLine("服务开启成功,运行在{0}", SignalRURI + "/signalr");

                     
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("服务开启失败. 已经有一个服务运行在{0}", SignalRURI);
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("服务开启异常：{0}", ex.ToString());
                Console.ReadLine();
            }
        }

        #region 测试
        //异步连接服务器
        public static void ConnectAsync()
        {
            Connection = new HubConnection(ServerUrl);
            Connection.Closed += Connection_Closed;
            Connection.StateChanged += Connection_StateChanged;
            Connection.Reconnected += Connection_Reconnected;
            HubProxy = Connection.CreateHubProxy("MsgHub");
            try
            {
                Connection.Start();

                HubProxy.On<string>("system", RecvSystem);
                HubProxy.On<string>("door", RecvDoor);
                Thread.Sleep(1000);   //等待连接成功
                HubProxy.Invoke("Hello", "aaa");
                Console.WriteLine("服务监听成功。。。。。");
                Console.ReadKey();
            }
            catch (Exception EX)
            {
                Console.WriteLine(EX.Message);
                return;
            }
        }
        private static void Connection_StateChanged(StateChange obj)
        {

        }

        private static void Connection_Reconnected()
        {

        }

        private static void RecvSystem(string msg)
        {
            Console.WriteLine("系统消息：" + msg);
        }
        private static void RecvDoor(string msg)
        {
            Console.WriteLine("门禁消息：" + msg);
        }
        private static void Connection_Closed()
        {
            // Connection.Start();
            // throw new NotImplementedException();
        }
        #endregion

    }
}
