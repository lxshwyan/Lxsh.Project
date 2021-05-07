using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
//https://www.cnblogs.com/hhhnicvscs/p/14323522.html
namespace Lxsh.Project.MSMQDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // SendQueue();
            ActionTest actionTest = new ActionTest();
            Console.WriteLine(Environment.TickCount/1000/60); 
          //  actionTest.test(ConsoleWrite).test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test().test();
       
        }
       private static void ConsoleWrite(string str)
        {
            Console.WriteLine(str);
        }
        private static void SendQueue()
        {
#if DEBUG
            Console.WriteLine("DEBUG");
#endif
            MessageQueue messageQueue = null;
            string description = "This is a test queue.";
            string message = "This is a test message.";
            string path = @".\Private$\IDG";
            ReadQueue(path);
            try
            {
                if (MessageQueue.Exists(path))
                {
                    messageQueue = new MessageQueue(path);
                    messageQueue.Label = description;
                }
                else
                {
                    MessageQueue.Create(path);
                    messageQueue = new MessageQueue(path);
                    messageQueue.Label = description;
                }
                messageQueue.Send(message);
            }
            catch
            {
                throw;
            }
            finally
            {
                messageQueue.Dispose();
            }
            Console.ReadKey();
        }
        private static List<string> ReadQueue(string path)
        {
            List<string> lstMessages = new List<string>();
            using (MessageQueue messageQueue = new MessageQueue(path))
            {
                System.Messaging.Message[] messages = messageQueue.GetAllMessages();
                foreach (System.Messaging.Message message in messages)
                {
                    message.Formatter = new XmlMessageFormatter(
                    new String[] { "System.String, mscorlib" });
                    string msg = message.Body.ToString();
                    Console.WriteLine(msg);
                    lstMessages.Add(msg);
                }
            }
            return lstMessages;
        }

    }
}
