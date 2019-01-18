﻿/************************************************************************
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
        /// <summary>
        ///  开启服务
        /// </summary>
        public static void Start()
        {
            string SignalRURI = "http://localhost:6178";
            try
            {
                try
                {
                    using (WebApp.Start(SignalRURI, builder =>
                    {
                        builder.Map("/signalr", map =>
                        {
                            // Setup the cors middleware to run before SignalR.
                            // By default this will allow all origins. You can 
                            // configure the set of origins and/or http verbs by
                            // providing a cors options with a different policy.
                            map.UseCors(CorsOptions.AllowAll);
                            var hubConfiguration = new HubConfiguration
                            {
                                // You can enable JSONP by uncommenting line below.
                                // JSONP requests are insecure but some older browsers (and some
                                // versions of IE) require JSONP to work cross domain
                                EnableJSONP = true
                            };
                            // Run the SignalR pipeline. We're not using MapSignalR
                            // since this branch is already runs under the "/signalr"
                            // path.
                            map.RunSignalR(hubConfiguration);
                        });
                        builder.MapSignalR();
                      
                    }))
                    {
                        Console.WriteLine("服务开启成功,运行在{0}", SignalRURI+"/signalr");
                        MsgTest();
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

        public static void MsgTest()
        {       
            var hub = GlobalHost.ConnectionManager.GetHubContext<MyConnection>();
            while (true)
            {     
                foreach (var item in MyConnection.dictionary)
                {

                    if (hub != null && item.Key != null)
                    {
                        hub.Clients.Client(item.Key)
                            .notice("当前收到信息:"+DateTime.Now.ToString());
                    }
                    Thread.Sleep(1000);
                }

            }

        }
    }
}