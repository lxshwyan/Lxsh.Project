using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;                         
using System.Text;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;

namespace Lxsh.Project.NetMQDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Console.ReadLine().ToLower() == "s")
            {
                RunServer();
            }
            else
            {
                RunClient();
            }

            Console.ReadLine();
        }
        public static void RunServer()
        {
            using (NetMQSocket serverSocket = new ResponseSocket())
            {
                serverSocket.Bind("tcp://127.0.0.1:5555");
                while (true)
                {
                    string message1 = serverSocket.ReceiveFrameString();

                    Console.WriteLine("Receive message :\r\n{0}\r\n", message1);

                    string[] msg = message1.Split(':');
                    string message = msg[1];


                    #region 根据接收到的消息，返回不同的信息
                    if (message == "Hello")
                    {
                        serverSocket.SendFrame("World");
                    }
                    else
                    {
                        serverSocket.SendFrame(message);
                    }
                    #endregion

                    if (message == "exit")
                    {
                        break;
                    }
                }
            }
        }
        public static void RunClient()
        {
            using (NetMQSocket clientSocket = new RequestSocket())
            {
                Random rd = new Random();
                int num = rd.Next(0, 100);
                clientSocket.Connect("tcp://127.0.0.1:5555");
                while (true)
                {
                    Console.WriteLine(num + ",Please enter your message:");
                    string message = Console.ReadLine();
                    clientSocket.SendFrame(num + ":" + message);

                    string answer = clientSocket.ReceiveFrameString();

                    Console.WriteLine("Answer from server:{0}", answer);

                    if (message == "exit")
                    {
                        break;
                    }
                }
            }
        }

    }
}
