using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace System
{
    public class HttpWebServer : IDisposable
    {
        public Socket socket;
        public static object objectLock = new object();
        System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
        public event Func<XHRequest, SocketReqModel> OnMessageEvent;
        public event Action<Exception> OnExceptionEvent;
        
        public void Dispose()
        {
            if (socket != null) socket.Close();
        }

        public void InitSocket(int port)
        {
            try
            {
                if (socket == null || !socket.Connected)
                {
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.Bind(new IPEndPoint(IPAddress.Any, port));
                    socket.Listen(10);
                    socket.BeginAccept(new AsyncCallback(OnAccept), socket);
                }
            }
            catch (Exception ex)
            {
                OnExceptionEvent?.Invoke(ex);
                new Task(() => {
                    Thread.Sleep(1000);
                    InitSocket(port);
                }).Start();
            }
        }

        public void OnAccept(IAsyncResult ar)
        {
            Socket socket = ar.AsyncState as Socket;
            Socket acceptSocket = socket.EndAccept(ar);
            socket.BeginAccept(new AsyncCallback(OnAccept), socket);

            byte[] recvBuffer = new byte[655350];
            acceptSocket.BeginReceive(recvBuffer, 0, recvBuffer.Length, SocketFlags.None, OnMessage, new SocketModel(acceptSocket, recvBuffer));
        }

        public void OnMessage(IAsyncResult ar)
        {
            lock (objectLock)
            {
                var model = ar.AsyncState as SocketModel;
                var buffer = model.buffer;
                var acceptSocket = model.socket;
                var count = acceptSocket.EndReceive(ar);


                var recvReq = Encoding.UTF8.GetString(buffer, 0, count);
                var req = GetRequest(recvReq);
                if (req.IsSuccess)
                {
                    var resp = OnMessageEvent?.Invoke(req);
                    if (resp != null)
                    {
                        SendCmdResp(new SocketRespModel() { status = 1, data = resp.response, message = resp.message }, acceptSocket);
                        return;
                    }
                }
                SendCmdResp(new SocketRespModel() { status = 0, message = "非法请求." + req.Exception?.Message }, acceptSocket);
            }
        }
       
        private XHRequest GetRequest(string request)
        {
            XHRequest r = new XHRequest();
            using (StringReader sr = new StringReader(request))
            {
                try
                {
                    var httpHeader = sr.ReadLine().Split(' ');
                    r.Method = httpHeader[0];
                    r.Url = httpHeader[1];
                    //解析请求头
                    while (true)
                    {
                        var headers = sr.ReadLine().Trim();
                        if (string.IsNullOrEmpty(headers))
                        {
                            break;
                        }
                        var reg = new Regex("([^\\:]+):([\\s\\S]+)");
                        var group = reg.Match(headers).Groups;
                        if (group.Count > 0)
                        {
                            var key = group[1].Value.Trim();
                            var value = group[2].Value.Trim();
                            if (!r.Headers.ContainsKey(key))
                            {
                                r.Headers.Add(key, value);
                            }
                            else
                            {
                                r.Headers[key] = value;
                            }
                        }                        
                    }
                    //解析参数
                    if (sr.Peek() != -1)
                    {
                        var ps = sr.ReadLine();
                        ps = Web.HttpUtility.UrlDecode(ps);
                        var tks = ps.Split('&');
                        foreach (var token in tks)
                        {
                            if (string.IsNullOrEmpty(token))
                                break;
                            var t = token.Split('=');
                            if (t.Length == 2)
                            {
                                if (!r.Params.ContainsKey(t[0]))
                                {
                                    r.Params.Add(t[0], t[1]);
                                }
                                else
                                {
                                    r.Params[t[0]] = t[1];
                                }
                            }
                        }
                    }
                    r.IsSuccess = true;                    
                }
                catch(Exception ex)
                {
                    r.Exception = ex;
                }
                return r;
            }
        }

        public void SendCmdResp(SocketRespModel resp, Socket socket)
        {
            byte[] contentResp = Encoding.UTF8.GetBytes(js.Serialize(resp));
            StringBuilder sb = new StringBuilder();
            sb.Append("HTTP/1.1 200 OK");
            sb.Append(Environment.NewLine);
            sb.Append("Access-Control-Allow-Credentials: true");
            sb.Append(Environment.NewLine);
            sb.Append("Access-Control-Allow-Origin: *");
            sb.Append(Environment.NewLine);
            sb.Append("Content-Type: application/json; charset=utf-8");
            sb.Append(Environment.NewLine);
            sb.Append("Content-Length: " + contentResp.Length);
            sb.Append(Environment.NewLine);
            sb.Append("Date: " + DateTime.Now.ToString());
            sb.Append(Environment.NewLine);
            sb.Append("Server: XiaoBaWang");
            sb.Append(Environment.NewLine);
            sb.Append("X-Powered-By: BaWangXiao");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append(js.Serialize(resp));
            var buffer = Encoding.UTF8.GetBytes(sb.ToString());
            //var headResp = Encoding.UTF8.GetBytes(sb.ToString());
            //var buffer = new byte[headResp.Length + contentResp.Length];
            //headResp.CopyTo(buffer, 0);
            //contentResp.CopyTo(buffer, headResp.Length);

            if (socket.Connected)
            {
                socket.Send(buffer);
                socket.Close();
                socket.Dispose();
            }
        }

        public class XHRequest
        {
            public string Method { get; set; }
            public Dictionary<string, string> Params { get; set; } = new Dictionary<string, string>();
            public string Url { get; set; }
            public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
            public bool IsSuccess { get; set; }
            public Exception Exception { get; set; }
            public string GetParams(string key, string defaultValue = "")
            {
                if (Params.ContainsKey(key))
                    return Params[key];
                return defaultValue;
            }
        }
    }
}