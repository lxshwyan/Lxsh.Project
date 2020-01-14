using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lxsh.Project.CsRedisNetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 队列     
            string input = "";
            string strErrorMsg = "";
            Console.WriteLine("请输入你要选择的发送端和接收端==1为发，2为收");
            string type = Console.ReadLine();
            PubMsg pubMsg = new PubMsg();
            if (type == "2")
            {
                   int count =0;
                   pubMsg.Subscribe(CategoryMessage.System.ToString(),
                    msg => 
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine($"一共收到{count++}条消息");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(msg.Body);
                    });
                Console.WriteLine("正在监听。。。。");
            }
            else
            {
                for (int i = 0; i < 1000000; i++)
                {
                    Thread.Sleep(500);
                    var bodyMsg = new SystemMessage()
                    {
                        // Content = $"第{i}条信息:{ input}",
                        Content = $"定时第{i}条信息",
                        DateTime = DateTime.Now.ToString(),
                        Title = "四方博瑞安防平台信息",
                        Type = "log".ToJson()
                    };
                    var message = new RabbitMQMsg { Body = bodyMsg.ToJson(), EventCategory = CategoryMessage.System, EventDst = "预留字段", EventSendTime = DateTime.Now.ToString(), SourceName = "消息来源" };


                    pubMsg.Publish(CategoryMessage.ABDoor.ToString(), message.ToJson());
                    Console.WriteLine("发送成功："+ bodyMsg.Content);
                }
            }
            #endregion
        }
       
    }
}
