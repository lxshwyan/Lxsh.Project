using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTestSQL
{
   public class DisposTest : IDisposable
    {
        public void Call()
        {
            Console.WriteLine("调用Call方法");
        }
        public void Dispose()
        {
            Console.WriteLine("释放完成!");
        }
    }
}
