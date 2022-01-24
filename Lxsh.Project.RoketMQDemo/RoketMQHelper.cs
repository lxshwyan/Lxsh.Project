using NewLife.RocketMQ;
using NewLife.RocketMQ.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.RoketMQDemo
{
    public delegate void DelConsumerMsg(string MsgId,string Keys, string Tags, DateTime BornTimestamp, string MsgBody);
    public class RoketMQHelper
    {
        public event DelConsumerMsg DelConsumerMsgEvent;
        /// <summary>
        /// MQ地址
        /// </summary>
        private string _NameServerAddress { get; set; }
        public RoketMQHelper(string NameServerAddress)
        {
            _NameServerAddress = NameServerAddress;
        }
        /// <summary>
        ///  0：成功, 1：刷盘超时,2: 刷从机超时,3:从机不可用
        /// </summary>
        /// <param name="strTopic"></param>
        /// <param name="Keys"></param>
        /// <param name="Tags"></param>
        /// <param name="bodyString"></param>
        /// <returns></returns>
        public int ProducerMsg(string strTopic,string Keys,string Tags, string bodyString)
        {
            try
            {
                Message message = new Message()
                {
                    BodyString = bodyString,
                    Keys = Keys,
                    Tags = Tags,
                    Flag = 0,
                    WaitStoreMsgOK = true
                };
                var mq = new Producer
                {
                    Topic = strTopic,
                    NameServerAddress = _NameServerAddress
                };
                mq.Start();
                var sr = mq.Publish(message);
                mq.Dispose();
                return (int)sr.Status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsumerMsg(string strTopic,string strGroup)
        {
            Console.WriteLine("消息接收测试");
            //测试消费消息
            var consumer = new NewLife.RocketMQ.Consumer
            {
                Topic = strTopic,
                Group = strGroup,
                NameServerAddress = _NameServerAddress,
                //设置每次接收消息只拉取一条信息
                BatchSize = 1
            };
            consumer.OnConsume = (q, ms) =>
            {
                string mInfo = $"BrokerName={q.BrokerName},QueueId={q.QueueId},Length={ms.Length}";
                Console.WriteLine(mInfo);
                foreach (var item in ms.ToList())
                {
                    if (DelConsumerMsgEvent != null)
                        DelConsumerMsgEvent(item.MsgId, item.Keys, item.Tags,item.BornTimestamp.ToDateTime(), item.Body.ToStr());
                    string msg = $"消息：msgId={item.MsgId},key={item.Keys}，产生时间【{item.BornTimestamp.ToDateTime()}】，内容>{item.Body.ToStr()}";
                    Console.WriteLine(msg);
                }
                //   return false;//通知消息队：不消费消息
                return true;		//通知消息队：消费了消息
            };
            consumer.Start();

        }
    }
}
