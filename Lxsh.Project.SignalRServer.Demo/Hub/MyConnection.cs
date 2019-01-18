using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;                
using Microsoft.AspNet.SignalR.Hubs;

namespace Lxsh.Project.SignalRServer.Demo.Hub
{
    public class MyConnection : Microsoft.AspNet.SignalR.Hub
    {

        public static ConcurrentDictionary<string, string> dictionary = new ConcurrentDictionary<string, string>();

        public void Hello(string msg)
        {
            this.Clients.All.Welcome("当前登录成功:"+ msg);
        }

        public override Task OnConnected()
        {
            dictionary.TryAdd(this.Context.ConnectionId, DateTime.Now.ToString());

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            dictionary.TryRemove(this.Context.ConnectionId,out string value);
            return base.OnDisconnected(stopCalled);
        }
    }
}