using Lxsh.Project.ConsoleDemo.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Lxsh.Project.ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //string strDateTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo) ;
            //string strDateTime1 = DateTime.Now.ToString();
            //Console.WriteLine(strDateTime);
            //Console.WriteLine(strDateTime1);

            string strMd5 = GetMD5HashFromFile(@"C:\Users\lxsh_wyan\Desktop\人员清点\人数清点试运行用户说明.ZIP");
          //  TestValue();

           //TestType();
           //TestRegex._instance.RegexWrite();
           //TestSQLite._instance.Insert();
           //TestTextReader._instance.Reader(); 
           //Console.WriteLine(TestRandoPort._instance.PortIsUsed().Count); ;
          // TestYiled._instance.TestYieldMethod();
          // TestYiled._instance.TestMethod();  
          // TestMemoryStream._instance.TestStream();
        }
        private static void TestType()
        {
            string TypenName = System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName;
            TypenName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            Type type = typeof(Program);
            Console.WriteLine(TypenName);

            TestRandoPort obj = Activator.CreateInstance(typeof(TestRandoPort)) as TestRandoPort;
            Console.WriteLine(obj.PortIsUsed().Count);
        }
        private static void TestValue()
        {
            Point p1 = new Point() { X = 30, Y = 20 };//值类型型 struct
            Point p2 = new Point() { X = p1.X, Y = 20 };
            p2.X = 50;
            Console.WriteLine($"P1.X={p1.X} P2.X={p2.X}");
            Pen pen1 = new Pen(Color.Red);//引用类型
                 Pen pen2 = pen1;
            pen2.Color = Color.DarkRed;
            Console.WriteLine($"pen1={pen1.Color} pen2={pen2.Color}" );

        }
        public static string GetMD5HashFromFile(string fileName)
        {
            try
            {
                FileStream file = new FileStream(fileName, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }

        public static void TestIQuery()
        {
            List<Entity> list = new List<Entity>()
            {
                new Entity { Id = Guid.NewGuid(), Name = "2" },
                new Entity { Id = Guid.NewGuid(), Name = "233333" },
                new Entity { Id = Guid.NewGuid(), Name = "233333", Num = 233333 },
                new Entity { Id = Guid.NewGuid(), Name = "233333", Num = 3 },
                new Entity { Id = Guid.NewGuid(), Name = "23" },
                new Entity { Id = Guid.NewGuid(), Name = "23", Num = 2333 },
            };
            Entity input = new Entity()
            {
                Id = null,
                Name = "233",
                Num = 233
            };
            var result = list.AsQueryable().WhereIf(input.Id.HasValue, item => item.Id == input.Id)
.WhereIf(input.Name, item => item.Name.Contains(input.Name))
.WhereIf(input.Num > 0, item => item.Num > input.Num * 20).ToList();

        }
    }

    public class Entity
    {
        // 可为空
        public Guid? Id { get; set; }
        // 字符串
        public string Name { get; set; }
        // 值类型
        public int Num { get; set; }
    }
}
