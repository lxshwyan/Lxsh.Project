using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;
using System;
using System.Collections.Generic;
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
        static string name = "";
        static async Task Main()
        {
            //int Caps = 0;
            int Caps = (1 << (int)VideoWallCaps.CellSplit1) | (1 << (int)VideoWallCaps.CellSplit9) | (1 << (int)VideoWallCaps.CellSplit4) | (1 << (int)VideoWallCaps.PollingCinfog);
            //Caps = Caps | (1 << (int)VideoWallCaps.CellBack) | (1 << (int)VideoWallCaps.CellTop) | (1 << (int)VideoWallCaps.CellOpen);
            Caps = Caps | (1 << (int)VideoWallCaps.PlanCinfig);
            Caps = Caps | (1 << (int)VideoWallCaps.PlanCall) | (1 << (int)VideoWallCaps.PollingStart) | (1 << (int)VideoWallCaps.PollingStop);
            int a = 1 << (int)VideoWallCaps.CellSplit1;



            var objectPool = new ServiceCollection().AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>()
            .BuildServiceProvider()
            .GetRequiredService<ObjectPoolProvider>()
            .Create<FoobarService>();
            while (true)
            {
                Console.Write("Used services: ");
                await Task.WhenAll(Enumerable.Range(1, 18).Select(_ => ExecuteAsync()));
                Console.Write("\n");
            }
            async Task ExecuteAsync()
            {
                var service = objectPool.Get();
                
                try
                {
                    Console.Write($"{service.Id}; ");
                    await Task.Delay(1000);
                }
                finally
                {
                    objectPool.Return(service);
                }
            }

            //Console.WriteLine("Hello World!");
            //System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            //for (int i = 0; i < System.Text.Encoding.GetEncodings().Length; i++)
            //{
            //    Console.WriteLine(System.Text.Encoding.GetEncodings()[i].DisplayName);
            //}
            //int j = 0;
            //Task.Run(() =>
            //{
            //    while(true)
            //    {
            //        j++;
            //        Console.WriteLine(RequestData($"https://tcc.taobao.com/cc/json/mobile_tel_segment.htm?tel=13667178037"));
            //        Thread.Sleep(100);
            //        Console.WriteLine(RequestData($"https://tcc.taobao.com/cc/json/mobile_tel_segment.htm?tel=13826920293"));
            //        Thread.Sleep(100);
            //        Console.WriteLine(RequestData($"https://tcc.taobao.com/cc/json/mobile_tel_segment.htm?tel=13826920193"));
            //        Thread.Sleep(100);
            //        Console.WriteLine($"第{j.ToString()}次调用"); 
            //    }
            //});
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

    public enum VideoWallCaps
    {
        /// <summary>
        /// 开窗
        /// </summary>
        CellOpen = 1,
        /// <summary>
        /// 置顶
        /// </summary>
        CellTop,
        /// <summary>
        /// 置后
        /// </summary>
        CellBack,
        /// <summary>
        /// 拼接
        /// </summary>
        CellSplicing,
        /// <summary>
        /// 解拼
        /// </summary>
        CellDefuse,
        /// <summary>
        /// 单画面
        /// </summary>
        CellSplit1,
        /// <summary>
        /// 四分屏
        /// </summary>
        CellSplit4,
        /// <summary>
        /// 六分屏
        /// </summary>
        CellSplit6,
        /// <summary>
        /// 九分屏
        /// </summary>
        CellSplit9,
        /// <summary>
        /// 十六分屏
        /// </summary>
        CellSplit16,
        /// <summary>
        /// 预案调用
        /// </summary>
        PlanCall,
        /// <summary>
        /// 预案配置
        /// </summary>
        PlanCinfig,
        /// <summary>
        /// 轮询开始
        /// </summary>
        PollingStart,
        /// <summary>
        /// 轮询停止
        /// </summary>
        PollingStop,
        /// <summary>
        /// 轮询配置
        /// </summary>
        PollingCinfog
    }
}
