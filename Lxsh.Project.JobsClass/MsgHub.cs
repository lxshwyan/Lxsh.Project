using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.JobsClass
{
    public class MsgHub : Microsoft.AspNet.SignalR.Hub
    {

        public static ConcurrentDictionary<string, string> dictionary = new ConcurrentDictionary<string, string>();

        public void Hello(string msg)
        {

            this.Clients.All.Welcome($"当前在线一共{dictionary.Count}个连接");
        }
        /// <summary>
        /// 编写发送信息的方法
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public void Send(string name, string message)
        {
            //调用所有客户注册的本地的JS方法(addMessage)
            Clients.All.addMessage(name, message);

        }
        public void SendAll(string name, string message)
        {
            //调用所有客户注册的本地的JS方法(addMessage)
            Clients.All.allInfo(message);
        }
        public override Task OnConnected()
        {
            dictionary.TryAdd(this.Context.ConnectionId, DateTime.Now.ToString());

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            dictionary.TryRemove(this.Context.ConnectionId, out string value);
            return base.OnDisconnected(stopCalled);
        }
    }
}
