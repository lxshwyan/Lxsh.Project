/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lxsh.Project.SuperSocketServer.Commands
*文件名： Login
*创建人： Lxsh
*创建时间：2019/8/12 17:18:51
*描述
*=======================================================================
*修改标记
*修改时间：2019/8/12 17:18:51
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
   public  class Login : CommandBase<ChatSession, StringRequestInfo>
    {   
        public override void ExecuteCommand(ChatSession session, StringRequestInfo requestInfo)
        {
            if (requestInfo.Parameters != null && requestInfo.Parameters.Length == 2)
            {
              
                var sesssionList = session.AppServer.GetAllSessions();
                if (sesssionList != null)
                {   
                    //ChatSession oldSession = sesssionList.FirstOrDefault(s => requestInfo.Parameters[0].Equals(s.Id));
                    //if (oldSession != null)
                    //{
                    //    oldSession.Send("login other computer，you kick off！");
                    //    oldSession.Close();
                    //}
                }
                //不去数据库查询了
                session.Id = requestInfo.Parameters[0];
                session.Password = requestInfo.Parameters[1];
                session.IsLogin = true;
                session.LoginTime = DateTime.Now;
                session.Send("Login Success");   
            }
            else//能进入这个方法，说明已经是Check
            {
                session.Send("Wrong Parameter");
            }
        }
    }
}