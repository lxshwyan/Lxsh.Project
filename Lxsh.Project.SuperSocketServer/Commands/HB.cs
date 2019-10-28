/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lxsh.Project.SuperSocketServer.Commands
*文件名： HB
*创建人： Lxsh
*创建时间：2019/8/12 17:13:08
*描述
*=======================================================================
*修改标记
*修改时间：2019/8/12 17:13:08
*修改人：Lxsh
*描述：
************************************************************************/
using Lxsh.Project.SuperSocketServer.Session;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lxsh.Project.SuperSocketServer.Commands
{
    public class HB : CommandBase<ChatSession, StringRequestInfo>
    {
        public override void ExecuteCommand(ChatSession session, StringRequestInfo requestInfo)
        {
            if (requestInfo.Parameters != null && requestInfo.Parameters.Length == 1)
            {
                if ("$".Equals(requestInfo.Parameters[0]))
                {
                    session.LastHBTime = DateTime.Now;
                    session.Send("$");
                }
                else
                {
                    session.Send("Wrong Parameter");
                }
            }
            else
            {
                session.Send("Wrong Parameter");
            }
        }
    }
}