using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Lxsh.Project.SignalR.Demo
{
    public class MyConnection : Hub
    {
        public static List<string> list = new List<string>();

        public void Hello(string msg)
        {
            this.Clients.All.Welcome("当前登录成功:"+ msg);
        }

        public override Task OnConnected()
        {
            list.Add(this.Context.ConnectionId);

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            list.Remove(this.Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }
    }
}