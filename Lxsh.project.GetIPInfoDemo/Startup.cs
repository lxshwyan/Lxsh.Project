using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Protocol;
using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lxsh.project.GetIPInfoDemo
{

    public class Startup
    {
        public void Start()
        {
            MqttCommon.CreateInstance();
            MqttCommon._instance.msgEvent += _instance_msgEvent;
            Logger.Default.Info("启动成功!");
;           Console.ReadLine();
        }
        public void Stop()
        {
            Logger.Default.Info("停止成功!");
        }
       public  void _instance_msgEvent(string obj)
        {
            Logger.Default.Info(obj);
            if (obj == "13826920293")
            {
                string strIP = Validator.GetIP(GetBodyByURL());
                Logger.Default.Info(strIP);
                MqttCommon._instance.SendIP($"杭州：{strIP}");
            }
            else
            {
                MqttCommon._instance.SendIP("指令错误");
                Logger.Default.Error("指令错误");
            }
        }

        public  string GetBodyByURL()
        {
            string strURL = "https://tool.lu/ip/";
            HttpWebRequest myrq = (HttpWebRequest)WebRequest.Create(strURL);
            myrq.Timeout = 30 * 1000; //超时时间
            myrq.Method = "Get";  //请求方式 
            myrq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            //  myrq.Host = "baike.baidu.com"; //来源
            //定义请求请求Referer 
            //  myrq.Referer = "https://www.baidu.com/link?url=krnoB2YHt94yzV5ewGRncTo8ayAJETxd_Yv2VXwmkO6wN9K401boggwFVgiPulgwix76akOoMOt72D6UBXb1WtxZoXFok4wW_BADpdDbcQk8U114CohHj0j-JPr0epo1&wd=&eqid=c0dedaf300022d3f000000025d4a87cd";
            //定义浏览器代理
            myrq.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 UBrowser/6.2.4098.3 Safari/537.36";

            //请求网页
            HttpWebResponse myrp = (HttpWebResponse)myrq.GetResponse();

            //判断请求状态
            if (myrp.StatusCode != HttpStatusCode.OK)
            {
                return "";
            }

            //打印网页源码
            using (StreamReader sr = new StreamReader(myrp.GetResponseStream()))
            {

                return (sr.ReadToEnd());
            }
        }
    }
}
