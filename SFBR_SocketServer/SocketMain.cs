/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：SFBR_SocketServer
*文件名： SocketMain
*创建人： Lxsh
*创建时间：2019/8/27 17:01:49
*描述
*=======================================================================
*修改标记
*修改时间：2019/8/27 17:01:49
*修改人：Lxsh
*描述：
************************************************************************/
using SFBR_Socket;
using SFBR_Socket.PublicClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace SFBR_SocketServer
{
    public class SocketMain
    {
      
        private ITxServer server = null;
        public void Start()
        {
            InitTcpServer();
        }
        public void InitTcpServer()
        {
            try
            {
                string serverPort = System.Configuration.ConfigurationManager.AppSettings["ServerPort"];
                server = TxStart.startServer(int.Parse(serverPort));
                server.AcceptString += new TxDelegate<IPEndPoint, string>(acceptString);
                server.AcceptByte += new TxDelegate<IPEndPoint, byte[]>(acceptBytes);
                server.Connect += new TxDelegate<IPEndPoint>(connect);
                server.dateSuccess += new TxDelegate<IPEndPoint>(dateSuccess);
                server.Disconnection += new TxDelegate<IPEndPoint, string>(disconnection);
                server.EngineClose += new TxDelegate(engineClose);
                server.EngineLost += new TxDelegate<string>(engineLost);  
                server.StartEngine();  
                Console.WriteLine($"服务器启动成功！！！！正在监听{serverPort}");
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        /// <summary>
        /// 当接收到来之客户端的文本信息的时候
        /// </summary>
        /// <param name="state"></param>
        /// <param name="str"></param>
        private void acceptString(IPEndPoint ipEndPoint, string str)
        {
           Console.WriteLine($" 接收时间：{DateTime.Now.ToString()},客户端节点：{ipEndPoint.ToString()},接收内容：{str}"  );  
        }
        /// <summary>
        /// 当接收到来之客户端的图片信息的时候
        /// </summary>
        /// <param name="ipEndPoint"></param>
        /// <param name="bytes"></param>
        private void acceptBytes(IPEndPoint ipEndPoint, byte[] bytes)
        {  
        }
        /// <summary>
        /// 当有客户端连接上来的时候
        /// </summary>
        /// <param name="state"></param>
        private void connect(IPEndPoint ipEndPoint)
        {
            Console.WriteLine($" 接收时间：{DateTime.Now.ToString()},客户端节点：{ipEndPoint.ToString()},上线");
            ClientNumber();
        }
        /// <summary>
        /// 当对方已收到我方发送数据的时候
        /// </summary>
        /// <param name="state"></param>
        private void dateSuccess(IPEndPoint ipEndPoint)
        {
            Console.WriteLine($"{DateTime.Now.ToString()}：已向" + ipEndPoint.ToString() + "发送成功");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipEndPoint"></param>
        /// <param name="str"></param>
        private void SendAllClient(string msg)
        {
            this.server.ClientAll?.ForEach(item =>
            {
                server.sendMessage(item, msg);
            });       

        }
        /// <summary>
        /// 当有客户端掉线的时候
        /// </summary>
        /// <param name="state"></param>
        /// <param name="str"></param>
        private void disconnection(IPEndPoint ipEndPoint, string str)
        {    
            Console.WriteLine($" 接收时间：{DateTime.Now.ToString()},客户端节点：{ipEndPoint.ToString()},下线");
            ClientNumber();
        }

        /// <summary>
        /// 当服务器完全关闭的时候
        /// </summary>
        private void engineClose()
        {

            Console.WriteLine($"{DateTime.Now.ToString()}： 服务器已关闭");       
        }
        /// <summary>
        /// 当服务器非正常原因断开的时候
        /// </summary>
        /// <param name="str"></param>
        private void engineLost(string str)
        { Console.WriteLine($"服务器非正常时间：{DateTime.Now.ToString()}： {str}"); }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipEndPoint"></param>
        /// <param name="str"></param>
        private void ClientNumber()
        {    
            this.server.ClientAll?.ForEach(item =>
            {
                server.sendMessage(item,Newtonsoft.Json.JsonConvert.SerializeObject(
                    new BodyMsg()  { MsgType = MsgType.OnLineCount, MsgContent = "当前在线人数:" + this.server.ClientNumber.ToString() }
                    ));
            });
            Console.WriteLine( "当前在线人数:" + this.server.ClientNumber.ToString());

        }
        public void Stop()
        {
          
        }
    }
}