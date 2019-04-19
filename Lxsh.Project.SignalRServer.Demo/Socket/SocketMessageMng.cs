using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace Lxsh.Project.SignalRServer.Demo
{
    /// <summary>
    /// 发送数据
    /// </summary>
    /// <param name="msg"></param>
    public delegate void SetText(string msg);

    public class SocketMessageMng
    {
        /// <summary>
        /// 将监听端口接收到的信息发送出去
        /// </summary>
        public event SetText SetTextEvent;
        private string m_SocketType = "2";

        public string SocketType
        {
            get { return m_SocketType; }
            set { m_SocketType = value; }
        }
        private string m_SocketHomeIP = "255.255.255.255";
        /// <summary>
        /// 监听IP
        /// </summary>
        public string SocketHomeIP
        {
            get { return m_SocketHomeIP; }
            set { m_SocketHomeIP = value; }
        }

        private int m_SocketPort = 8888;
        /// <summary>
        /// 监听端口
        /// </summary>
        public int SocketPort
        {
            get { return m_SocketPort; }
            set { m_SocketPort = value; }
        }

        SetText Sendtxt;

        /// <summary>
        /// 类的初始化，当仅需要发送数据 而不接收时使用
        /// </summary>
        public SocketMessageMng()
        {
            Sendtxt = new SetText(SendRecData);
        }

        private void SendRecData(string msg)
        {
            if (SetTextEvent != null)
            {
                SetTextEvent(msg);
            }
        }

        /// <summary>
        /// 数据初始化
        /// 启动本机指定IP端口的消息监听
        /// </summary>
        /// <param name="HostName">IP</param>
        /// <param name="Port">端口</param>
        public SocketMessageMng(string HostName, int Port)
            : this()
        {
            this.SocketHomeIP = HostName;
            this.SocketPort = Port;
        }

        /// <summary>
        /// 数据初始化
        /// 启动本机指定IP端口的消息监听。
        /// </summary>
        /// <param name="Port">端口</param>
        public SocketMessageMng(int Port)
            : this()
        {
            this.SocketPort = Port;
        }

        #region TCP

        #region TCP 数据定义
        private TcpListener tcpLister;
        System.Threading.ThreadStart listenPort;
        System.Threading.Thread lister;
        TcpClient client;
        private bool ReceiveData = true;
        #endregion

        /// <summary>
        /// 初始化Tcp监听
        /// </summary>
        public void TcpInit()
        {
            listenPort += new ThreadStart(TcpStartListen);
            lister = new Thread(listenPort);

            tcpLister = new TcpListener(Dns.GetHostAddresses(Dns.GetHostName())[0], SocketPort);

            client = new TcpClient(SocketHomeIP, SocketPort);
        }


        /// <summary>
        /// 开始监听
        /// </summary>
        public void StartListening()
        {
            lister.Start();
        }

        /// <summary>
        /// 开始监听 
        /// </summary>
        public void TcpStartListen()
        {
            try
            {
                tcpLister.Start();

                while (ReceiveData)
                {
                    Socket s = tcpLister.AcceptSocket();
                    Byte[] stream = new Byte[80];

                    int i = s.Receive(stream);
                    string message = System.Text.Encoding.UTF8.GetString(stream);
                }
            }
            catch (Exception ef)
            {
                throw new Exception(ef.Message);
            }
        }

        /// <summary>
        /// 发送消息 
        /// </summary>
        /// <param name="msg">发送消息内容信息</param>
        public void TcpSendMsg(string msg)
        {
            lock (this)
            {
                NetworkStream sendStream = client.GetStream();
                StreamWriter writer = new StreamWriter(sendStream);

                writer.Write(msg);
                writer.Flush();
                sendStream.Close();
                client.Close();
            }
        }
        #endregion

        #region UDP
        private UdpClient udpCli;
        private IPEndPoint ipEnd;
        private Thread td;
        private bool IsStart = true;

        /// <summary>
        /// 初始化Udp监听
        /// </summary>
        private void UdpInit()
        {
            try
            {
                ipEnd = new IPEndPoint(IPAddress.Any, SocketPort);
                udpCli = new UdpClient(ipEnd);
                td = new Thread(new ThreadStart(UdpListener));
            }
            catch (Exception ef)
            {
              
            }
        }

        /// <summary>
        /// 开启Udp监听
        /// </summary>
        public void UdpStartListen()
        {
            UdpInit();
            if (td != null)
                td.Start();
        }

        /// <summary>
        /// 发送数据到指定 IP 端口
        /// </summary>
        /// <param name="ToIP">发送到的IP</param>
        /// <param name="msg">发送的端口</param>
        /// <returns>返回处理消息</returns>
        public string UdpSendMsg(string ToIP, string msg)
        {
            try
            {
                if (udpCli == null)
                {
                    udpCli = new UdpClient();
                }

                Byte[] StrBytes = Encoding.UTF8.GetBytes(msg);

                udpCli.Send(StrBytes, StrBytes.Length, ToIP, SocketPort);

                return "\n报警信息发送成功！";//"  "+ToIP+":"+SocketPort+ 
            }
            catch (Exception ef)
            {
                return "   \n报警信息发送失败！\n" + "原因可能是: " + ef.Message;//"  " + ToIP + ":" + SocketPort + 
            }
        }

        /// <summary>
        /// 发送数据到指定 IP端口
        /// </summary>
        /// <param name="ToIP">发送到的IP</param>
        /// <param name="msg">发送的端口</param>
        /// <returns>返回处理消息</returns>
        public string UdpSendMsg(string ToIP, int UdpPort, string msg)
        {
            try
            {
                if (udpCli == null)
                {
                    udpCli = new UdpClient();
                }

                Byte[] StrBytes = null;
                if (m_SocketType == "1")
                {
                    StrBytes = Encoding.Default.GetBytes(msg);
                }
                if (m_SocketType == "2")
                {
                    StrBytes = Encoding.UTF8.GetBytes(msg);
                }
                else
                {
                    StrBytes = strToToHexByte(msg);
                }

                udpCli.Send(StrBytes, StrBytes.Length, ToIP, UdpPort);

                return "\n报警信息发送成功！";//"  "+ToIP+":"+SocketPort+ 
            }
            catch (Exception ef)
            {
                return "   \n报警信息发送失败！\n" + "原因可能是: " + ef.Message;//"  " + ToIP + ":" + SocketPort + 
            }
        }

        /// <summary>
        /// 监听，接收数据
        /// </summary>
        private void UdpListener()
        {
            try
            {
                string Msg = "";

                while (IsStart)
                {
                    Byte[] receiveBytes = udpCli.Receive(ref ipEnd);
                    if (m_SocketType == "1")
                    {
                        Msg = Encoding.Default.GetString(receiveBytes);
                    }
                    if (m_SocketType == "2")
                    {
                        Msg = Encoding.UTF8.GetString(receiveBytes);
                    }
                    else
                    {
                        Msg = byteToHexStr(receiveBytes);
                    }

                    this.Sendtxt.Invoke(Msg);
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>   
        /// 16进制字符串转字节数组   
        /// </summary>   
        /// <param name="hexString"></param>   
        /// <returns></returns>   
        byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2).Trim(), 16);
            return returnBytes;
        }
        /// <summary>   
        /// 字节数组转16进制字符串   
        /// </summary>   
        /// <param name="bytes"></param>   
        /// <returns></returns>   
        string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                    returnStr += " ";
                }
            }
            return returnStr.Trim();
        }
        #endregion

        /// <summary>
        /// 释放所有资源
        /// </summary>
        public void Dis()
        {
            try
            {
                if (td.ThreadState == ThreadState.Running)
                {
                    IsStart = false;
                    udpCli.Close();
                    ReceiveData = false;
                    td.Abort();
                }

                //if (td.ThreadState == ThreadState.Running)
                //{
                //    IsStart = false;
                //    //tcp.Close();
                //    td.Abort();
                //}
            }
            catch (Exception ef)
            {
                //Logs.LogWrite("SocketMessage: " + ef.Message);
            }
        }

        ~SocketMessageMng()
        {

        }
    }
}
