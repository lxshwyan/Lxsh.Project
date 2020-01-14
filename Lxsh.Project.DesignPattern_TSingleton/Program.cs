using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.DesignPattern_TSingleton
{
    class Program
    {
        static void Main(string[] args)
        {
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            //Singleton._instance.Test();

            //Console.WriteLine($"第一次一共耗时{sw.ElapsedMilliseconds}毫秒");


            //sw.Restart();
            //Singleton._instance.Test();
            //sw.Stop();
            //Console.WriteLine($"第二次一共耗时{sw.ElapsedMilliseconds}毫秒");

            TestSQLServerConnectionCount();
            Console.Read();
        }


        public static void TestSQLServerConnectionCount()
        {
            int i = 1;
            try
            {
                int maxCount = 40000;
                string connectionString =
                    "Data Source=192.168.137.252;Initial Catalog=SFBR_Criminal_Count;User Id=sa;Password=123456;connect timeout = 5; ";
                for (i = 1; i < maxCount; i++)
                {
                    var db = new SqlConnection(connectionString);
                    db.Open();
                    Console.WriteLine("已创建连接对象" + i);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(i+"=="+ ex.Message);
            }
        }
    }
}
