using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Protocol;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.GetIPDemo
{
   public class MqttCommon
    {
        private string IP = string.Empty;
        private int Port = 0;
        private string UserName = string.Empty;
        private string PassWord = string.Empty;
        string ClientId = string.Empty;
        private string Topic = string.Empty;
        private static object _objectInstance = new object();
        public static MqttCommon _instance = null;
        public event Action<string> msgEvent;
        public static MqttCommon CreateInstance()
        {
            if (_instance == null)        //双层判断优化性能+线程安全
            {
                lock (_objectInstance)
                {
                    if (_instance == null)
                    {
                        _instance = new MqttCommon();
                    }
                }
            }
            return _instance;
        }
        public MqttCommon()
        {

            IP = ConstHelper.MQTTIP;
            Port = ConstHelper.MQTTPORT;
            UserName = ConstHelper.MQTTUSERNAME;
            PassWord = ConstHelper.MQTTPASSWORD;
            Topic = ConstHelper.MQTTTOPIC;
            {
                var client = new MqttFactory().CreateMqttClient();
                client.UseDisconnectedHandler(async e =>
                {
                    if (client.IsConnected)
                    {
                        return;
                    }
                    await Task.Delay(1000);
                    await connectAsync(client, IP, Port, UserName, PassWord, ClientId);
                });

                try
                {
                    connectAsync(client, IP, Port, UserName, PassWord, ClientId).Wait();

                }
                catch (Exception ex)
                {
                    Logger.Default.Error("mqtt连接失败，请检查配置;");
                    m_client = null;
                }
                m_client = client;
            };
        }
        IMqttClient m_client = null;
        public async Task connectAsync(IMqttClient mqttClient, string ip, int port, string username, string pwd, string clientid)
        {
            var builder = new MqttClientOptionsBuilder().WithTcpServer(ip, port);
            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(pwd))
            {
                builder = builder.WithCredentials(username, pwd);
            }
            if (!string.IsNullOrWhiteSpace(clientid))
            {
                builder = builder.WithClientId(clientid);
            }
            await mqttClient.ConnectAsync(builder.WithCommunicationTimeout(TimeSpan.FromSeconds(5)).Build());
            await mqttClient.SubscribeAsync(Topic, MqttQualityOfServiceLevel.AtMostOnce);
            mqttClient.UseApplicationMessageReceivedHandler(eventArgs=> {
                var msg = Encoding.UTF8.GetString(eventArgs.ApplicationMessage.Payload);
                Logger.Default.Info(DateTime.Now.ToString("HH:mm:ss") + ">" + msg);
                if (msgEvent!=null)
                {
                    msgEvent.Invoke(msg);
                }
            });
        }
        public string SendIP(string msg)
        {
         
            try
            {
                if (m_client != null && m_client.IsConnected)
                {
                    Task.Run(async () => await m_client.PublishAsync("/sfbr/send/testInfo", msg, MqttQualityOfServiceLevel.AtMostOnce));
                }
                else
                {
                    Logger.Default.Error("未连接,mqtt连接失败，请检查配置;");
                   // logger.Error("未连接", "mqtt连接失败，请检查配置;");
                }
            }
            catch (Exception ex)
            {
                Logger.Default.Error(ex.Message+Environment.NewLine+ex.StackTrace);
            }
            return string.Empty;
        }
    }
}
