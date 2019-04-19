using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lxsh.Project.SignalR.Demo.Controllers
{
    /// <summary>
    /// 这样d
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseController<T> : Controller where T : Hub
    {
        public IHubContext  Clients { get;private set; }

        public IGroupManager Groups { get; private set; }

        public BaseController()
        {
            var hub = GlobalHost.ConnectionManager.GetHubContext<MyConnection>();

            Clients = hub.Clients;
            Groups = hub.Groups;
        }
    }
}