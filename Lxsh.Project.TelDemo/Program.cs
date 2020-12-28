using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lxsh.Project.TelDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            for (int i = 0; i < System.Text.Encoding.GetEncodings().Length; i++)
            {
                Console.WriteLine(System.Text.Encoding.GetEncodings()[i].DisplayName);
            }
            int j = 0;
            Task.Run(() =>
            {
                while(true)
                {
                    j++;
                    Console.WriteLine(RequestData($"https://tcc.taobao.com/cc/json/mobile_tel_segment.htm?tel=13667178037"));
                    Thread.Sleep(100);
                    Console.WriteLine(RequestData($"https://tcc.taobao.com/cc/json/mobile_tel_segment.htm?tel=13826920293"));
                    Thread.Sleep(100);
                    Console.WriteLine(RequestData($"https://tcc.taobao.com/cc/json/mobile_tel_segment.htm?tel=13826920193"));
                    Thread.Sleep(100);
                    Console.WriteLine($"第{j.ToString()}次调用"); 
                }


            });
            Console.ReadLine();
        }


        /// <summary>
        /// 请求数据
        /// 注：若使用证书,推荐使用X509Certificate2的pkcs12证书
        /// </summary>
        /// <param name="method">请求方法</param>
        /// <param name="url">请求地址</param>
        /// <param name="body">请求的body内容</param>
        /// <param name="contentType">请求数据类型</param>
        /// <param name="headers">请求头</param>
        /// <param name="cerFile">证书</param>
        /// <returns></returns>
        public static string RequestData(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new Exception("请求地址不能为NULL或空！");
            string newUrl = url;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(newUrl);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
           // request.
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                int httpStatusCode = (int)response.StatusCode;
                using (Stream responseStream = response.GetResponseStream())
                {
                    Encoding encoding = Encoding.GetEncoding("GBK");
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("GBK"));
                    string resData = reader.ReadToEnd();
                    return resData;
                }
            }
        }

    }
}
