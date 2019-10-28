using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Lxsh.Project.CsRedisDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                #region 队列
                PubMsg pubMsg = new PubMsg();
              
            //   pubMsg.Subscribe("qd", msg => { Console.WriteLine("qd1" + ":" + msg.Body); });
              
                //for (int i = 0; i < 1000000; i++)
                //{
                //    pubMsg.Publish("qd", Newtonsoft.Json.JsonConvert.SerializeObject(new CriInfo()
                //    {
                //        ID = i,
                //        BulkNo = i.ToString().PadLeft(10, '0'),
                //        Name = "李小双-" + i

                //    }));
                //}
                #endregion

                #region  Lock
                // string strKey = "name";
                // pubMsg.csRedisClient.Set(strKey, "123");
                //var unLock= pubMsg.Lock(strKey);
                // unLock.Unlock();
                // pubMsg.csRedisClient.Set(strKey, "234");
                // Console.WriteLine(pubMsg.csRedisClient.Get(strKey));

                #endregion

                #region  分布式锁
                //    var lockTimeout = 50000;//单位是毫秒
                //   var currentTime = DateTime.Now.ToUniversalTime().Millisecond;
                //    if (pubMsg.SetNx("lockkey", currentTime + lockTimeout, lockTimeout))
                //    {
                //        //TODO:一些业务逻辑代码
                //        //.....
                //        //.....
                //        //最后释放锁
                //        Console.WriteLine("正在执行业务逻辑");

                //      //  pubMsg.Remove("lockkey");
                //}
                //    else
                //    {
                //        Console.WriteLine("没有获得分布式锁");
                //    }
                //    Console.ReadKey();
                #endregion

                #region dynamic
                //dynamic obj = new System.Dynamic.ExpandoObject();
                //obj.name = "lxsh";
                //obj.passWord = "lxsh";
                //obj.Call = new Action<string>(str=> { Console.WriteLine($"{obj.name} Call {str}"); });
                ////obj.Call("Wy");
                //obj.Call="lxsh";
                #endregion

                #region csRedisClient List
                //DateTime dt = DateTime.Now;
                //for (int i = 0; i < 1000000; i++)
                //{
                //    pubMsg.csRedisClient.SAdd("sSQ", Newtonsoft.Json.JsonConvert.SerializeObject(new CriInfo()
                //    {
                //        ID = i,
                //        BulkNo = i.ToString().PadLeft(10, '0'),
                //        Name = "李小双-" + i

                //    }));
                //}
                //double d = DateTime.Now.Subtract(dt).TotalMilliseconds;  
                //方法一
                DateTime dt1 = DateTime.Now; 
                //var list = pubMsg.csRedisClient.SMembers("sSQ");
                var list = pubMsg.csRedisClient.SCard("sSQ");
                double d1 = DateTime.Now.Subtract(dt1).TotalMilliseconds;
                Console.WriteLine("=====");
                #endregion   

                #region   Environment.StackTrace
                Console.WriteLine($"我的错误日志：{Environment.StackTrace}");
                #endregion

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public class CriInfo
        {
            public int ID { get; set; }
            public string Name { get; set; }   
            public string BulkNo { get; set; }
        }

    }
   
}
