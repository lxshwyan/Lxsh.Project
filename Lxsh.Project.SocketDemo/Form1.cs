using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lxsh.Project.SocketDemo
{
    public partial class Form1 : Form
    {
        private Socket Socket;
        private Socket ClientSocket;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            //根据DNS获取域名绑定的IP
            foreach (var address in Dns.GetHostEntry("www.baidu.com").AddressList)
            {
                Console.WriteLine($"百度IP:{address}");
            }
            //字符串转IP地址
            IPAddress ipAddress = IPAddress.Parse("192.168.1.101");

            //通过IP和端口构造IPEndPoint对象，用于远程连接
            //通过IP可以确定一台电脑，通过端口可以确定电脑上的一个程序
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 80);
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            //  Console.WriteLine(System.Text.Encoding.Default.EncodingName);
            StartServer();
        }
        private void StartServer()
        {
            //1 创建socket对象
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //字符串转IP地址
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            //2 绑定IP和端口
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 6868);
            Socket.Bind(ipEndPoint);

            //3. 设置监听最大队列
            Socket.Listen(10);
            Task.Run(() => { Accept(Socket); });

        }
        private void Accept(Socket socket)
        {
            while (true)
            {
                //4、【阻塞】，等待客户端连接
                Socket newSocket = Socket.Accept();
                Task.Run(() => { Receive(newSocket); });
            }
          

        }
        private void Receive(Socket socket)
        {
            while (true)
            {
                //5、【阻塞】，等待读取客户端发送过来的数据
                byte[] data = new byte[1024 * 1024];
                int readLeng = socket.Receive(data, 0, data.Length, SocketFlags.None);

                if (readLeng == 0)//客户端断开连接
                {
                    //停止会话（禁用Socket上的发送和接收，该方法允许Socket对象一直等待，直到将内部缓冲区的数据发送完为止）
                    socket.Shutdown(SocketShutdown.Both);
                    //关闭连接
                    socket.Close();
                    //跳出循环
                    return;
                }

                //6、读取数据
                var msg = Encoding.Default.GetString(data, 0, readLeng);
                socket.Send(Encoding.Default.GetBytes(msg));
                Console.WriteLine("服务端接收消息：" + msg);
            }
        }

        private void ClientConnet()
        {
            ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //字符串转IP地址
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            //2 绑定IP和端口
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 6868);
            ClientSocket.Connect(ipEndPoint);
            Task.Run(() => ReviceInfo(ClientSocket));
        }
        private void ReviceInfo(Socket socket)
        {
            while (true)
            {
                //5、【阻塞】，等待读取客户端发送过来的数据
                byte[] data = new byte[1024 * 1024];
                int readLeng = socket.Receive(data, 0, data.Length, SocketFlags.None);

                if (readLeng == 0)//客户端断开连接
                {
                    //停止会话（禁用Socket上的发送和接收，该方法允许Socket对象一直等待，直到将内部缓冲区的数据发送完为止）
                    socket.Shutdown(SocketShutdown.Both);
                    //关闭连接
                    socket.Close();
                    //跳出循环
                    return;
                }

                //6、读取数据
                var msg = Encoding.Default.GetString(data, 0, readLeng);
                socket.Send(Encoding.Default.GetBytes(msg));
                Console.WriteLine("客户端接收消息："+msg);
            }
        }
      
        private void btnClient_Click(object sender, EventArgs e)
        {
            ClientConnet();
            ClientSocket.Send(Encoding.Default.GetBytes("开始/结束"));
        }
    }
}
