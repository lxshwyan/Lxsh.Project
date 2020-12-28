using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Protocol;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lxsh.Project.MQTTDemo
{
    class Program
    {
        private static IMqttClient mqttClient = null; //客户端对象
        private static string ClientId = "sfbr-1";
        private static string IP = "127.0.0.1";
        private static int? Port= 1883;
        private static string UserName = "sfbr";
        private static string pwd = "sfbr123456";
        static void Main(string[] args)
        {
            CreateMqttClient();
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
        private static void CreateMqttClient()
        {
            //实例化 创建客户端对象
            var Factory = new MqttFactory();
            mqttClient = Factory.CreateMqttClient();
            mqttClient.UseApplicationMessageReceivedHandler(agrs =>
            {
                mqttClient.PublishAsync(new MqttApplicationMessage() { Topic = "system/ab", Payload = Encoding.UTF8.GetBytes("afsafs") });
                //Console.WriteLine(agrs.ApplicationMessage);
            });
            mqttClient.UseConnectedHandler(agrs =>
            {
                Console.WriteLine(agrs.AuthenticateResult.AssignedClientIdentifier);

                mqttClient.PublishAsync(new MqttApplicationMessage() { Topic = "system/ab", Payload = Encoding.UTF8.GetBytes("afsafs") });
            });
            mqttClient.ConnectAsync(option());
            mqttClient.SubscribeAsync(new MqttTopicFilter() { Topic = "system/ab", QualityOfServiceLevel=0 });

        
        }
        public static IMqttClientOptions option()
        {
            //连接到服务器前，获取所需要的MqttClientTcpOptions 对象的信息
            var options = new MqttClientOptionsBuilder()
            .WithClientId(ClientId)                    // clientid是设备id
            .WithTcpServer(IP, Port)              //onenet ip：183.230.40.39    port:6002
           // .WithCredentials(UserName, pwd)      //username为产品id       密码为鉴权信息或者APIkey
                                                 //.WithTls()//服务器端没有启用加密协议，这里用tls的会提示协议异常
            .WithCleanSession(false)
            .WithKeepAlivePeriod(TimeSpan.FromSeconds(2000))
            .Build();
            return options;
        }


    }
}
