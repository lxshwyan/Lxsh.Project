using System;
using System.Collections.Generic;
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
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Singleton._instance.Test();
         
            Console.WriteLine($"第一次一共耗时{sw.ElapsedMilliseconds}毫秒");


            sw.Restart();
            Singleton._instance.Test();
            sw.Stop();
            Console.WriteLine($"第二次一共耗时{sw.ElapsedMilliseconds}毫秒");
        }
    }
}
