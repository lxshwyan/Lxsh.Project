using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks; 
using System.Configuration;
using SFBR.RabbitMQ;

namespace Lxsh.Project.Common.RabbbitMQ.Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {    
            string input = "";
            string strErrorMsg = "";
            Console.WriteLine("请输入你要选择的发送端和接收端==1为发，2为收");
            string type = Console.ReadLine();
            string connStr = System.Configuration.ConfigurationManager.AppSettings["connStr"] ?? "host=192.168.137.110:5672;virtualHost=/;username=guest;password=guest";
            Console.WriteLine(connStr);
            MQHelper tMQHelper = MQHelperFactory.CreateBus(connStr);
            if (type == "1")
            {        
                Console.WriteLine("================================开始发送消息================================");
                Console.WriteLine("Please enter a message. 'Quit' to quit.");
                int i = 0;

                //while ((input = Console.ReadLine()) != "Quit")
                while(true)
                {
                    Thread.Sleep(1000);
                    i++;
                    var bodyMsg = new SystemMessage()
                    {
                       // Content = $"第{i}条信息:{ input}",
                        Content= $"定时第{i}条信息",
                        DateTime = DateTime.Now.ToString(),
                        Title = "四方博瑞安防平台信息",
                        Type = "log".ToJson()
                     };                         
                    var message = new RabbitMQMsg { Body=bodyMsg.ToJson(), EventCategory = CategoryMessage.System, EventDst = "预留字段",  EventSendTime = DateTime.Now.ToString(),  SourceName = "消息来源" };
                     
                    tMQHelper.TopicPublish(message.EventCategory.ToString() + ".xA", message, ref strErrorMsg);
                  
                    Console.WriteLine(message.Body);
                    Console.WriteLine("================================发送消息完成================================");
                }
            }
            else
            {
               

                tMQHelper.TopicSubscribe(Guid.NewGuid().ToString(), s => Console.WriteLine("当前收到信息："+s.Body.FromJson<SystemMessage>().Content),true, CategoryMessage.System.ToString() + ".*", CategoryMessage.Alarm.ToString() + ".*");
                Console.WriteLine("Please enter a message. 'Quit' to quit.");
            }
        }   
       
    }
}
