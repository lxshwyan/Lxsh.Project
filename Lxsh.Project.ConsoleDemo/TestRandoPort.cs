/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lxsh.Project.ConsoleDemo
*文件名： TestRandoPort
*创建人： Lxsh
*创建时间：2019/10/26 17:12:34
*描述
*=======================================================================
*修改标记
*修改时间：2019/10/26 17:12:34
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace Lxsh.Project.ConsoleDemo
{
   public class TestRandoPort
    {
        public static TestRandoPort _instance;
        private static object objLock = new object();

        static TestRandoPort()
        {
            if (_instance == null)
            {
                lock (objLock)
                {
                    if (_instance == null)
                        _instance = new TestRandoPort();
                }
            }
        }


        /// <summary>        

        /// 获取操作系统已用的端口号        

        /// </summary>        

        /// <returns></returns>        

        public List<int> PortIsUsed()   
        {
            Stopwatch _sw = new Stopwatch();
            _sw.Start();   
            //获取本地计算机的网络连接和通信统计数据的信息    
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();  
            //返回本地计算机上的所有Tcp监听程序       
            IPEndPoint[] ipsTCP = ipGlobalProperties.GetActiveTcpListeners();
            //返回本地计算机上的所有UDP监听程序        
            IPEndPoint[] ipsUDP = ipGlobalProperties.GetActiveUdpListeners();    
            //返回本地计算机上的Internet协议版本4(IPV4 传输控制协议(TCP)连接的信息。
            TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();
            List<int> allPorts = new List<int>();
            List<int> ranndPorts = new List<int>();
            for (int i = 9000; i < 9999; i++)
            {
                ranndPorts.Add(i);
            }
          
            foreach (IPEndPoint ep in ipsTCP)  
            {           
                allPorts.Add(ep.Port);  
            }

            foreach (IPEndPoint ep in ipsUDP)     
            {   
                allPorts.Add(ep.Port); 
            }

            foreach (TcpConnectionInformation conn in tcpConnInfoArray)     
            {

                allPorts.Add(conn.LocalEndPoint.Port);

            }
            ranndPorts= ranndPorts.Where(r => !allPorts.Contains(r)).ToList();
            Console.WriteLine(_sw.ElapsedMilliseconds);

            return ranndPorts;

        }

    }
}