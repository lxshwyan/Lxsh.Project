using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lxsh.Project.SocketDemo
{
    public partial class FrmServer : Form
    {
        public FrmServer()
        {
            InitializeComponent();
        }
        private Socket socketServer;
        private   Dictionary<string, Socket> dicSocket = new Dictionary<string, Socket>();
        private List<string> strSocket;
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.button1.Text == "启动服务")
            {
                socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Parse(textBox1.Text);
                IPEndPoint iPEndPoint = new IPEndPoint(ip, int.Parse(textBox2.Text));
                socketServer.Bind(iPEndPoint);

                //3、开启侦听(等待客户机发出的连接),并设置最大客户端连接数为10
                socketServer.Listen(10);
                txtMsg.Text = "启动服务成功\r\n";
                Text = "服务已启动";
                this.button1.Text = "停止服务";
                Task.Run(() => Accept(socketServer));
             
            }
            else
            {
                this.button1.Text = "启动服务";
                txtMsg.Text = "停止服务成功\r\n";
                socketServer?.Close();
                foreach (var item in listBox1.Items)
                {
                    dicSocket[item.ToString()].Shutdown(SocketShutdown.Both);
                 
                }
                dicSocket.Clear();
            }
          

        }
        
        private void Accept(Socket socket)
        {
            while (true)
            {
                Socket newSocket = socket.Accept();  
                InvokeText(txtMsg, $"{newSocket.RemoteEndPoint}上线了\r\n");
                InvokeControl(listBox1,()=>listBox1.Items.Add(newSocket.RemoteEndPoint));
                dicSocket.Add(newSocket.RemoteEndPoint.ToString(), newSocket);
                Task.Run(() => Revice(newSocket));
            }
        }
        private void Revice(Socket socket)
        {
            byte[] data = new byte[1024 * 1024];
            while (socket.Connected)
            {
                //读取客户端发送过来的数据
                int readLeng = socket.Receive(data, 0, data.Length, SocketFlags.None);
                if (readLeng == 0)//客户端断开连接
                {   
                    InvokeText(txtMsg, $"{socket.RemoteEndPoint}下线了\r\n");
                    InvokeControl(listBox1, () => listBox1.Items.Remove(socket.RemoteEndPoint));
                    dicSocket.Remove(socket.RemoteEndPoint.ToString());
                    //停止会话（禁用Socket上的发送和接收，该方法允许Socket对象一直等待，直到将内部缓冲区的数据发送完为止）
                    socket.Shutdown(SocketShutdown.Both);
                    //关闭连接
                    socket.Close();
                    return;
                }
                InvokeText(txtMsg, $"{socket.RemoteEndPoint}：{Encoding.UTF8.GetString(data, 0, readLeng)}\r\n");
             
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (var item in listBox1.SelectedItems)
            {     
                //把消息内容转成字节数组后发送
                dicSocket[item.ToString()].Send(Encoding.UTF8.GetBytes(txtContent.Text));
            }
        }
        private void InvokeText(Control control,string msg)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new Action(() =>control.Text+= msg));
            }
            else
            {
                control.Text += msg;
            }
        }
        private void InvokeControl(Control control,Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }

        private void FrmServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var item in listBox1.Items)
            {
                dicSocket[item.ToString()].Shutdown(SocketShutdown.Both);
            }
        }
    }
}
