/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lxsh.Project.SuperSocketServer.Session
*文件名： ChatSession
*创建人： Lxsh
*创建时间：2019/8/12 16:56:26
*描述
*=======================================================================
*修改标记
*修改时间：2019/8/12 16:56:26
*修改人：Lxsh
*描述：
************************************************************************/
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lxsh.Project.SuperSocketServer.Session
{
   public class ChatSession: AppSession<ChatSession>
    {
        /// <summary>
        /// 用户的唯一标识
        /// </summary>
        public string Id { get; set; }
        public string Password { get; set; }
        public bool IsLogin { get; set; }
        public DateTime LoginTime { get; set; }

        public DateTime LastHBTime { get; set; }

        public bool IsOnLine
        {
            get
            {
                return this.LastHBTime.AddSeconds(10) > DateTime.Now;
            }
        }

        public override void Send(string message)
        {
            Console.WriteLine($"准备发送给{this.Id}：{message}");
            base.Send(message.Format());
        }

        protected override void OnSessionStarted()
        {
            var sesssionList = this.AppServer.GetAllSessions();
            if (sesssionList != null)
            {
                sesssionList.ToList().ForEach(s => s.Send("ConnetCount:" + (sesssionList.ToList().Count +1).ToString()));
            }
            this.Send("ConnetCount:"+(sesssionList.ToList().Count + 1).ToString());
            //this.Send("Welcome to SuperSocket Chat Server");
        }

        protected override void OnInit()
        {
            this.Charset = Encoding.GetEncoding("gb2312");
            base.OnInit();
        }

        protected override void HandleUnknownRequest(StringRequestInfo requestInfo)
        {
            Console.WriteLine("收到命令:" + requestInfo.Key.ToString()); 

            this.Send("不知道如何处理 " + requestInfo.Key.ToString() + " 命令");
        }


        /// <summary>
        /// 异常捕捉
        /// </summary>
        /// <param name="e"></param>
        protected override void HandleException(Exception e)
        {
            this.Send($"\n\r异常信息：{ e.Message}");
            //base.HandleException(e);
        }

        /// <summary>
        /// 连接关闭
        /// </summary>
        /// <param name="reason"></param>
        protected override void OnSessionClosed(CloseReason reason)
        {
            Console.WriteLine("链接已关闭。。。");
            var sesssionList = this.AppServer.GetAllSessions();
            if (sesssionList != null)
            {
                sesssionList.ToList().ForEach(s => s.Send("ConnetCount:" + (sesssionList.ToList().Count-1).ToString()));
            }
            base.OnSessionClosed(reason);
           
        }
    }
}