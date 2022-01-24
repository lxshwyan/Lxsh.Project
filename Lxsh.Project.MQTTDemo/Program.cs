using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Protocol;
using Newtonsoft.Json;
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
        private static string IP = "192.168.0.253";
        private static int? Port= 1888;
        private static string UserName = "sfbr";
        private static string pwd = "123456";
        static void Main(string[] args)
        {
            CreateMqttClient();
            Console.WriteLine("Hello World!");
            Thread.Sleep(3000);
            mqttClient.SubscribeAsync(new TopicFilterBuilder().WithTopic("e135951f-2e76-4bf3-913f-27cc7b10c235/f1d02f8a-7e95-41a0-8064-e15afa26e70b/UploadData").Build());
           // mqttClient.SubscribeAsync(new TopicFilterBuilder().WithTopic("b1e70aea-a7d9-471f-8d6e-ebde0110cb0a/HostReal/UploadData").Build());

            Console.ReadKey();
        }
        private static void CreateMqttClient()
        {
            //实例化 创建客户端对象
            var Factory = new MqttFactory();
            mqttClient = Factory.CreateMqttClient();
            mqttClient.UseApplicationMessageReceivedHandler(e =>
            {

                string Msg = "{\"Topic\":\"" + e.ApplicationMessage.Topic + "\"," + Environment.NewLine
                + "\"PayLoad\":" + Encoding.UTF8.GetString(e.ApplicationMessage.Payload) + "," + Environment.NewLine
                + "\"QoS\":" + "\"" + e.ApplicationMessage.QualityOfServiceLevel + "\"," + Environment.NewLine
                + "\"Time\":" + "\"" + DateTime.Now.ToString() + "\"}" + Environment.NewLine;
                Console.WriteLine(e.ApplicationMessage);

                DistributionBoxEntity distributionBoxEntity = JsonConvert.DeserializeObject<DistributionBoxEntity>(Msg);
                Console.WriteLine(distributionBoxEntity.Topic);
                Console.WriteLine(distributionBoxEntity.PayLoad.DeviceName);
            });
            mqttClient.UseConnectedHandler(agrs =>
            {
                mqttClient.ConnectAsync(option());
            });
            mqttClient.ConnectAsync(option());
            
           // mqttClient.SubscribeAsync(new MqttTopicFilter() { Topic = "b1e70aea-a7d9-471f-8d6e-ebde0110cb0a/HostReal/UploadData", QualityOfServiceLevel=0 });
          //  mqttClient.SubscribeAsync(new MqttTopicFilter() { Topic = "e135951f-2e76-4bf3-913f-27cc7b10c235/f1d02f8a-7e95-41a0-8064-e15afa26e70b/UploadData", QualityOfServiceLevel = 0 });

       

        }
        public static IMqttClientOptions option()
        {
            //连接到服务器前，获取所需要的MqttClientTcpOptions 对象的信息
            var options = new MqttClientOptionsBuilder()
            .WithClientId(ClientId)                    // clientid是设备id
            .WithTcpServer(IP, Port)              //onenet ip：183.230.40.39    port:6002
            .WithCredentials(UserName, pwd)      //username为产品id       密码为鉴权信息或者APIkey
                                                 //.WithTls()//服务器端没有启用加密协议，这里用tls的会提示协议异常
            .WithCleanSession(false)
            .WithKeepAlivePeriod(TimeSpan.FromSeconds(2000))
            .Build();
            return options;
        }
    }
    public class ListBoxChannelRealData
    {
        public int AirConditioningStatus { get; set; }

        public string AirDoorTemp { get; set; }

        public int AlarmLevel { get; set; }

        public int Channel { get; set; }

        public string ChannelName { get; set; }

        public int ChannelStatus { get; set; }

        public string ChannelTypeName { get; set; }

        public string Channeltype { get; set; }

        public int ConnecTion { get; set; }

        public bool ContactStatus { get; set; }

        public DateTime CreateTime { get; set; }

        public string CreateUser { get; set; }

        public string CurrentA { get; set; }

        public string CurrentB { get; set; }

        public string CurrentC { get; set; }

        public string Describe { get; set; }

        public string DeviceCode { get; set; }

        public string DeviceId { get; set; }

        public string DeviceName { get; set; }
        public string DeviceNum { get; set; }
        public bool Enabled { get; set; }
        public string Humdity { get; set; }
        public string Id { get; set; }
        public bool IsControl { get; set; }
        public bool IsLeakageProtection { get; set; }
        public List<object> ListSubChannelRealData { get; set; }
        public string MainVoltage_A { get; set; }
        public string MainVoltage_B { get; set; }
        public string MainVoltage_C { get; set; }
        public DateTime NewTime { get; set; }
        public string ParentId { get; set; }
        public string PhaseA { get; set; }

        public string PhaseB { get; set; }

        public string PhaseC { get; set; }

        public int PhaseType { get; set; }

        //序号
        public int Sort { get; set; }

        public string Temp0 { get; set; }

        //温度A
        public string TempA { get; set; }
        //温度B
        public string TempB { get; set; }
        //温度C
        public string TempC { get; set; }
        //温度N
        public string TempN { get; set; }

        public object UploadTime { get; set; }

        //是否警报
        public bool isAlarm { get; set; }

        //漏电
        public string La { get; set; }
    }


    //public class DistributionBoxEntity
    //{
    //    public string DeviceId { get; set; }

    //    public string DeviceName { get; set; }

    //    public string DeviceType { get; set; }
    //}
    public class DistributionBoxEntity
    {
        public string Topic { get; set; }

        public dynamic PayLoad { get; set; }
    }
}
