/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lxsh.Project.SignalRServer.Demo.Client
*文件名： HubClient
*创建人： Lxsh
*创建时间：2019/1/18 16:01:46
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/18 16:01:46
*修改人：Lxsh
*描述：
************************************************************************/
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lxsh.Project.SignalRServer.Demo.Client
{
  public  class HubClient
    {
        public static void ClientTest()
        {
            var conn = new HubConnection("http://localhost:7178/signalr");

            var proxy = conn.CreateHubProxy("MyConnection");

            proxy.On("Welcome", (msg) =>
            {
                Console.WriteLine(msg);
            });
            proxy.On("notice", (msg) =>
            {
                Console.WriteLine(msg);
            });
            proxy.On("ALLInfo", (msg) =>
          {
              Console.WriteLine(msg);
          });
            conn.Start().Wait();
            while (true)
            {
                Thread.Sleep(1000);
                var info = proxy.Invoke<string>("Hello", 1000000).Result;
            }
         
          //  Console.ReadKey();
        }
    }
}