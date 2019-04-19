/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lxsh.Project.SignalRServer.Demo.AppStart
*文件名： Startup
*创建人： Lxsh
*创建时间：2019/1/18 14:57:51
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/18 14:57:51
*修改人：Lxsh
*描述：
************************************************************************/
using Microsoft.AspNet.SignalR;
using Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using System;
using System.Reflection;
using System.Threading;
using Lxsh.Project.SignalRServer.Demo.Hub;

namespace Lxsh.Project.SignalRServer.Demo.AppStart
{

    public class Startup
    {

        SocketMessageMng socket;
        private  void InitUdp(int port = 8893)
        {
            try
            {
                SocketMessageMng socket = new SocketMessageMng(port);
                socket.UdpStartListen();
                socket.SetTextEvent += Socket_SetTextEvent;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Udp异常：{0}", ex.ToString());

            }


        }
        private  void InitSinalR(string SignalRURI = "http://localhost:6178")
        {
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

                        Console.ReadLine();
                    }
                }
                catch (TargetInvocationException)
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
        private  void Socket_SetTextEvent(string msg)
        {
            msg = string.Format("{0}:收到信息：{1}", DateTime.Now.ToString(), msg);
            Console.WriteLine(msg);
            MsgTest(msg);

        }

        public  void MsgTest(string msg)
        {
            var hub = GlobalHost.ConnectionManager.GetHubContext<MyConnection>();

            foreach (var item in MyConnection.dictionary)
            {

                if (hub != null && item.Key != null)
                {
                    hub.Clients.Client(item.Key)
                        .notice(msg);
                }   
            }

        }
        /// <summary>
        ///  开启服务
        /// </summary>
        public void Start()
        {
            InitUdp();
            InitSinalR();
        }
        /// <summary>
        /// 停止服务
        /// </summary>
        public void Stop()
        {
            socket.Dis();
        }
    }
}