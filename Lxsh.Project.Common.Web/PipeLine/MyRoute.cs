using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Lxsh.Project.Common.Web.PipeLine
{
    /// <summary>
    /// 直接扩展route，拒绝浏览器
    /// </summary>
    public class MyRoute : RouteBase
    {
        /// <summary>
        /// 解析路由信息
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            if (httpContext.Request.UserAgent.IndexOf("Chrome/62.0.3202.94") >= 0)
            {
                RouteData rd = new RouteData(this, new MvcRouteHandler());
                rd.Values.Add("controller", "Pipe");
                rd.Values.Add("action", "Refuse");
                return rd;
            }
            return null;
        }

        /// <summary>
        /// 指定处理的虚拟路径
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }
    }
}
