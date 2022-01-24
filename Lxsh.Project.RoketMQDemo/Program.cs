using NewLife.RocketMQ;
using NewLife.RocketMQ.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lxsh.Project.RoketMQDemo
{
    class Program
    {

        static void Main(string[] args)
        {
            string identityCard = "421381198809034011";//随便拼的，如有雷同，纯属搞怪哈
            BirthdayAgeSex entity = new BirthdayAgeSex();
            entity = GetBirthdayAgeSex(identityCard);
            if (entity != null)
            {
                Console.WriteLine(entity.Birthday + "-----" + entity.Sex + "-----" + entity.Age);
            }
            Console.ReadLine();
            string strstrTopic = "SFBR-ABM";
            RoketMQHelper _RoketMQHelper = new RoketMQHelper("192.168.137.252:9876");
            _RoketMQHelper.DelConsumerMsgEvent += _RoketMQHelper_DelConsumerMsgEvent;
            _RoketMQHelper.ConsumerMsg(strstrTopic, "test");
            // Console.ReadLine();
            SendMsg(strstrTopic, _RoketMQHelper);
        }
        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="strstrTopic"></param>
        /// <param name="roketMQHelper"></param>
        private static void SendMsg(string strstrTopic, RoketMQHelper roketMQHelper)
        {
            int i = 0;
            while (true)
            {
                i++;
                try
                {
                    roketMQHelper.ProducerMsg(strstrTopic, Guid.NewGuid().ToString(), "3309", $"我是第{i}条信息");
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message + "\n\r" + ex.StackTrace);
                }

                Thread.Sleep(100);
            }

        }
        /// <summary>
        /// 订阅信息
        /// </summary>
        /// <param name="MsgId"></param>
        /// <param name="Keys"></param>
        /// <param name="Tags"></param>
        /// <param name="BornTimestamp"></param>
        /// <param name="MsgBody"></param>
        private static void _RoketMQHelper_DelConsumerMsgEvent(string MsgId, string Keys, string Tags, DateTime BornTimestamp, string MsgBody)
        {
            Console.WriteLine(MsgBody);
        }

        public static BirthdayAgeSex GetBirthdayAgeSex(string identityCard)
        {
            if (string.IsNullOrEmpty(identityCard))
            {
                return null;
            }
            else
            {
                if (identityCard.Length != 15 && identityCard.Length != 18)//身份证号码只能为15位或18位其它不合法
                {
                    return null;
                }
            }

            BirthdayAgeSex entity = new BirthdayAgeSex();
            string strSex = string.Empty;
            if (identityCard.Length == 18)//处理18位的身份证号码从号码中得到生日和性别代码
            {
                entity.Birthday = identityCard.Substring(6, 4) + "-" + identityCard.Substring(10, 2) + "-" + identityCard.Substring(12, 2);
                strSex = identityCard.Substring(14, 3);
            }
            if (identityCard.Length == 15)
            {
                entity.Birthday = "19" + identityCard.Substring(6, 2) + "-" + identityCard.Substring(8, 2) + "-" + identityCard.Substring(10, 2);
                strSex = identityCard.Substring(12, 3);
            }

            entity.Age = CalculateAge(entity.Birthday);//根据生日计算年龄
            if (int.Parse(strSex) % 2 == 0)//性别代码为偶数是女性奇数为男性
            {
                entity.Sex = "女";
            }
            else
            {
                entity.Sex = "男";
            }
            return entity;
        }

        /// <summary>
        /// 根据出生日期，计算精确的年龄
        /// </summary>
        /// <param name="birthDate">生日</param>
        /// <returns></returns>
        public static int CalculateAge(string birthDay)
        {
            DateTime birthDate = DateTime.Parse(birthDay);
            DateTime nowDateTime = DateTime.Now;
            int age = nowDateTime.Year - birthDate.Year;
            //再考虑月、天的因素
            if (nowDateTime.Month < birthDate.Month || (nowDateTime.Month == birthDate.Month && nowDateTime.Day < birthDate.Day))
            {
                age--;
            }
            return age;
        }
    }

    /// <summary>
    /// 定义 生日年龄性别 实体
    /// </summary>
    public class BirthdayAgeSex
    {
        public string Birthday { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
    }
}
