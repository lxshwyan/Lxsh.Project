using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using WebSocketSharp;

namespace Lxsh.Project.WebSocketDemo
{
    public class WebSocketHepler
    {
        private WebSocket ws = null;
        /// <summary>
        /// 静态变量(用来存放类的实例)
        /// </summary>
        static WebSocketHepler _instance;
        /// <summary>
        /// 用来锁定的对象
        /// </summary>
        static readonly object locker = new object();
        /// <summary>
        /// 静态属性(提供给外部的全局访问点)
        /// </summary>
        public static WebSocketHepler Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (locker)
                    {
                        if (_instance == null)
                        {
                            _instance = new WebSocketHepler();
                        }
                    }
                }
                return _instance;
            }
        }
        public static string url { get; set; }
        public event Action<string> OnMessage;
        public event Action OnReconnected;
        public bool Isopen { get; set; } = false;
        private bool first = false;
        public WebSocketHepler()
        {
            Uri uri = new Uri(url);
            ws = new WebSocket($"{uri}");
            {
                ws.OnOpen += Ws_OnOpen;
                ws.OnClose += Ws_OnClose;
                ws.OnMessage += Ws_OnMessage;
                ws.OnError += Ws_OnError;
                ws.Log.Level = LogLevel.Trace;
            }
            try
            {
                ws.Connect();
            }
            catch (Exception ex)
            {
            }
            Thread thread = new Thread(new ThreadStart(Run))
            {
                IsBackground = false
            };
            thread.Start();
        }

        public void Send(string msg)
        {
            ws.Send(msg);
        }

        private void Run()
        {
            while (first)
            {
                var isOpen = true;
                lock (this)
                    isOpen = Isopen;
                if (!isOpen)
                {
                    ws.Connect();
                    Thread.Sleep(60000);
                }
            }
        }

        private void Ws_OnOpen(object sender, EventArgs e)
        {
            lock (this)
            {
                Isopen = true;
            }
            if (!first)
            {
                first = true;
            }
            else
            {
                OnReconnected?.Invoke();
            }
        }

        private void Ws_OnError(object sender, ErrorEventArgs e)
        {
            lock (this)
                Isopen = false;
        }

        private void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            try
            {
                OnMessage?.Invoke(e.Data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Ws_OnClose(object sender, CloseEventArgs e)
        {
            if (!first)
            {
                first = true;
            }
            lock (this)
                Isopen = false;
        }

    }
   
}
