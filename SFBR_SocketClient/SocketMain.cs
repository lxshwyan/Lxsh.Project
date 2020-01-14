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
using System.Threading;

namespace SFBR_SocketClient
{
    public class SocketMain
    {
        private ITxClient TxClient = null;
        public void Start()
        {
            InitTcpClient();
        }
        public void InitTcpClient()
        {
            try
            {
                string serverPort = System.Configuration.ConfigurationManager.AppSettings["ServerPort"];
                string serverIP = System.Configuration.ConfigurationManager.AppSettings["ServerIP"];
                TxClient = TxStart.startClient(serverIP, int.Parse(serverPort));
                TxClient.AcceptString += new TxDelegate<IPEndPoint, string>(accptString);//当收到文本数据的时候
                TxClient.dateSuccess += new TxDelegate<IPEndPoint>(sendSuccess);//当对方已经成功收到我数据的时候
                TxClient.EngineClose += new TxDelegate(engineClose);//当客户端引擎完全关闭释放资源的时候
                TxClient.EngineLost += new TxDelegate<string>(engineLost);//当客户端非正常原因断开的时候
                TxClient.ReconnectionStart += new TxDelegate(reconnectionStart);//当自动重连开始的时候
                TxClient.StartResult += new TxDelegate<bool, string>(startResult);//当登录完成的时候  
                //TxClient.BufferSize = 12048;//这里大小自己设置，默认为1KB，也就是1024个字节
                TxClient.StartEngine();
                TxClient.ReconnectMax = 100;
                SendInfo();
            }
            catch (Exception)
            {
                throw;
            }

        }
        private void SendInfo()
        {
            int i = 0;
            while (true)
            {
                Thread.Sleep(500);
                TxClient.sendMessage($"第{i++}条消息");   
            }
        }
        /// <summary>
        /// 接收到文本数据的时候
        /// </summary>
        /// <param name="str"></param>
        /// 
        private void accptString(IPEndPoint end, string str)
        {

            Console.WriteLine(str);

        }
        /// <summary>
        /// 当自动重连开始的时候
        /// </summary>
        private void reconnectionStart()
        {
            Console.WriteLine("正在重新连接");
            // "10秒后自动重连开始";
        }
        /// <summary>
        /// 当登录有结果的时候
        /// </summary>
        /// <param name="b">是否成功</param>
        /// <param name="str">失败或成功原因</param>
        private void startResult(bool b, string str)
        {
        }
        /// <summary>
        /// 当客户端引擎完全关闭的时候
        /// </summary>
        private void engineClose()
        {
            //  "客户端已经关闭";
        }
        /// <summary>
        /// 当客户端突然断开的时候
        /// </summary>
        /// <param name="str">断开原因</param>
        private void engineLost(string str)
        { }
        /// <summary>
        /// 当数据发送成功的时候
        /// </summary>
        private void sendSuccess(IPEndPoint end)
        {
            //"数据发送成功";
        }
        public void Stop()
        {

        }
    }
}