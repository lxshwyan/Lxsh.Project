using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using Newtonsoft;

namespace Lxsh.Project.WebSocket.ClientDemo
{
    class Program
    {
        static WebSocketSharp.WebSocket ws;
        static void Main(string[] args)
        {
            ws = new WebSocketSharp.WebSocket("ws://localhost:9278/ws");
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
                ws.Send(Newtonsoft.Json.JsonConvert.SerializeObject(new SfWebPlusCallModel() { JsonString = "12", Name = "tetes", Success =true }));
            }
            catch (Exception ex)
            {
            }
        }
        private static void Ws_OnError(object sender, ErrorEventArgs e)
        {

        }

        private static void Ws_OnMessage(object sender, MessageEventArgs e)
        {

        }

        private static void Ws_OnClose(object sender, CloseEventArgs e)
        {

        }

        private static void Ws_OnOpen(object sender, EventArgs e)
        {

        }
    }
    public class SfWebPlusCallModel
    {
        public string Name { get; set; }

        public bool Success { get; set; }

        public string JsonString { get; set; }
    }
}
