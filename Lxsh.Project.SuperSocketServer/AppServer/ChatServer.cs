/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lxsh.Project.SuperSocketServer.AppServer
*文件名： ChatServer
*创建人： Lxsh
*创建时间：2019/8/12 16:51:09
*描述
*=======================================================================
*修改标记
*修改时间：2019/8/12 16:51:09
*修改人：Lxsh
*描述：
************************************************************************/
using Lxsh.Project.SuperSocketServer.Session;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lxsh.Project.SuperSocketServer.AppServer
{
   public class ChatServer : AppServer<ChatSession>
    {     
        protected override bool Setup(IRootConfig rootConfig, IServerConfig config)
        {
            Console.WriteLine("准备读取配置文件。。。。");
            return base.Setup(rootConfig, config);
        }

        protected override void OnStarted()
        {
            Console.WriteLine("Chat服务启动。。。");
            base.OnStarted();
        }

        protected override void OnStopped()
        {
            Console.WriteLine("Chat服务停止。。。");
            base.OnStopped();
        }

        /// <summary>
        /// 新的连接
        /// </summary>
        /// <param name="session"></param>
        protected override void OnNewSessionConnected(ChatSession session)
        {
            Console.WriteLine("Chat服务新加入的连接:" + session.LocalEndPoint.Address.ToString());
            base.OnNewSessionConnected(session);
        }
    }
}