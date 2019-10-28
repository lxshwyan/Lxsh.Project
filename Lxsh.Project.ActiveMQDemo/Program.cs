using Apache.NMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lxsh.Project.ActiveMQDemo
{
    class Program
    {
        static void Main(string[] args)
        {
         // ActiveMQHelper mymq = new ActiveMQHelper(isLocalMachine: false, remoteAddress: "192.168.80.110");
            ActiveMQHelper mymq = new ActiveMQHelper(isLocalMachine: true, remoteAddress: "192.168.80.110");

            mymq.InitQueueOrTopic(topic: true, name: "openapi.fas.topic", selector: false);
            mymq.consumer.Listener += Consumer_Listener;
            while (true)
            {
                mymq.SendMessage("tttttttttttttttt");
            }

        }

        private static void Consumer_Listener(Apache.NMS.IMessage message)
        {

            try
            {
                ITextMessage msg = (ITextMessage)message;
                Console.WriteLine("Receive: " + msg.Text);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
