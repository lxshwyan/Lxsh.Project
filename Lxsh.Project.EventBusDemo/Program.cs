using System;

namespace Lxsh.Project.EventBusDemo
{
   public class Program
    {
        static void Main(string[] args)
        {
            var test = new TestEventBus();
            test.TestEventBusRegister();
        }
      
    }
}
