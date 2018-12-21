using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Lxsh.Project.Common.Web.PipeLine
{
    /// <summary>
    /// 注册handlerfactory
    /// 可以为不同的后缀指定不同的handler
    /// aspx的ioc就可以从这个地方注入
    /// </summary>
    public class ImageHandlerFactory : IHttpHandlerFactory
    {
        public IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
        {
            string path = context.Request.PhysicalPath;
            if (Path.GetExtension(path).Equals(".gif"))
            {
                return new ImageHandler();
            }
            else if (Path.GetExtension(path) == ".png")
            {
                return new ImageHandler();
            }
            else
            {
                return new ImageHandler();
            }
        }

        public void ReleaseHandler(IHttpHandler handler)
        {
        }
    }
}
