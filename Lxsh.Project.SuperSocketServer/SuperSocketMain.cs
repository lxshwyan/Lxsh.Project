/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lxsh.Project.SuperSocketServer
*文件名： SuperSocketMain
*创建人： Lxsh
*创建时间：2019/8/12 17:04:57
*描述
*=======================================================================
*修改标记
*修改时间：2019/8/12 17:04:57
*修改人：Lxsh
*描述：
************************************************************************/
using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lxsh.Project.SuperSocketServer
{
   public class SuperSocketMain
    {    
       public static void Init()
            {
                try
                {
                    Console.WriteLine("Welcome to SuperSocket SocketService!");
                    IBootstrap bootstrap = BootstrapFactory.CreateBootstrap();
                    if (!bootstrap.Initialize())
                    {
                        Console.WriteLine("初始化失败");
                        Console.ReadKey();
                        return;
                    }
                    Console.WriteLine("启动中...");
                    var result = bootstrap.Start();
                    foreach (var server in bootstrap.AppServers)
                    {
                        if (server.State == ServerState.Running)
                        {
                            Console.WriteLine("- {0} 运行中", server.Name);
                        }
                        else
                        {
                            Console.WriteLine("- {0} 启动失败", server.Name);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.Read();
            }
    }
}