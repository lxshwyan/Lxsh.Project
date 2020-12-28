using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.HttpWebServerDemo
{
    class Program
    {
        public static HttpWebServer httpServer = new HttpWebServer();
        static void Main(string[] args)
        {
            httpServer.OnExceptionEvent += HttpServer_OnExceptionEvent;
            httpServer.InitSocket(59899);
            httpServer.OnMessageEvent += HttpServer_OnMessageEvent;
            Console.ReadKey();
        }

        private static SocketReqModel HttpServer_OnMessageEvent(HttpWebServer.XHRequest request)
        {
            if (request.IsSuccess && request.Method == "POST")//暂只处理POST请求
            {
                if (request.Url == "/query/ReadIDCard")//读身份证操作
                {
                }
                else if (request.Url == "/query/ReadDoorCard")//读门禁卡操作
                {
                    return null;
                }
            }
            return null;
        }

        private static void HttpServer_OnExceptionEvent(Exception obj)
        {
            Console.WriteLine(obj.Message);
            Console.WriteLine(obj.StackTrace);
        }
    }
}
