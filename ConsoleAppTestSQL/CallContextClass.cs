using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleAppTestSQL
{
  public  class CallContextClass
    {
        public void TestGetSetData()
        {
            Console.WriteLine($"Current ThreadID={Thread.CurrentThread.ManagedThreadId}");
            var user = new User()
            {
                Id = DateTime.Now.ToString(),
                Name = "Enson"
            };
            CallContext.SetData("key", user);
            var value1 = CallContext.GetData("key");
            Console.WriteLine(user == value1);



            // 异步线程执行
            Task.Run(() =>
            {
                Console.WriteLine($"Current ThreadId={Thread.CurrentThread.ManagedThreadId}");
                var value2 = CallContext.GetData("key");
                Console.WriteLine(value2 == null ?
                    "NULL" : (value2 == value1).ToString());
            });
            // 主线程执行
            Console.WriteLine($"Current ThreadId={Thread.CurrentThread.ManagedThreadId}");
            value1 = CallContext.GetData("key");
            Console.WriteLine(value1 == user);

            // 清理数据槽
            CallContext.FreeNamedDataSlot("key");
            var value3 = CallContext.GetData("key");
            Console.WriteLine(value3 == null ?
                    "NULL" : (value3 == value1).ToString());
        }
    }
    public class User
    {
        public string  Id { get; set; }
        public string Name { get; set; }
    }
}
