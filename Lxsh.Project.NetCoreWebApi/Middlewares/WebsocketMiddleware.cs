using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lxsh.Project.NetCoreWebApi.Middlewares
{
    public class WebsocketMiddleware
    {
        private readonly RequestDelegate _next;
   
        private readonly ILogger<WebsocketMiddleware> _logger;


        public WebsocketMiddleware( RequestDelegate next, ILogger<WebsocketMiddleware> logger)
        {
            _next = next;
            this._logger = logger;


        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/ws")
            {

                //仅当网页执行new WebSocket("ws://localhost:5000/ws")时，后台会执行此逻辑

//                1、Error during WebSocket handshake: Unexpected response code: 404
//当VS设置使用IIS Express启动，但IIS没安装WebSocket时，会出现这个错误，解决方法有两个：①IIS安装WebSocket，②设置为项目自托管启动。

                if (context.WebSockets.IsWebSocketRequest)
                {
                    //后台成功接收到连接请求并建立连接后，前台的webSocket.onopen = function (event){}才执行
                    WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    string clientId = Guid.NewGuid().ToString();

                    var wsClient = new WebsocketClient
                    {
                        Id = clientId,
                        Client = webSocket
                    };
                    try
                    {
                        await Handle(context, wsClient);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Echo websocket client {0} err .", clientId);

                        await context.Response.WriteAsync("closed");
                    }
                }
                else
                {
                    context.Response.StatusCode = 404;
                }
            }
            else
            {
                await _next(context);
            }
        }
        private async Task Handle(HttpContext context, WebsocketClient websocketClient)
        {
            WebsocketClientCollection.Instance.AddClient(websocketClient);
            _logger.LogInformation($"Websocket client added.");

            WebSocketReceiveResult clientData = null;
            do
            {
                try
                {
                    var buffer = new byte[1024 * 1];
                    //客户端与服务器成功建立连接后，服务器会循环异步接收客户端发送的消息，收到消息后就会执行Handle(WebsocketClient websocketClient)中的do{}while;直到客户端断开连接
                    //不同的客户端向服务器发送消息后台执行do{}while;时，websocketClient实参是不同的，它与客户端一一对应
                    //同一个客户端向服务器多次发送消息后台执行do{}while;时，websocketClient实参是相同的
                    clientData = await websocketClient.Client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    if (clientData.MessageType == WebSocketMessageType.Text && !clientData.CloseStatus.HasValue)
                    {
                        var msgString = Encoding.UTF8.GetString(buffer);
                        _logger.LogInformation($"Websocket client ReceiveAsync message {msgString}.");
                        var message = JsonConvert.DeserializeObject<Message>(msgString);
                        message.SendClientId = websocketClient.Id;
                        HandleMessage(message);
                    }
                }
                catch (Exception ex)
                {

                    _logger.LogInformation(ex.StackTrace+Environment.NewLine +ex.Message);
                }
              
            } while (!clientData.CloseStatus.HasValue);
            //关掉使用WebSocket连接的网页/调用webSocket.close()后，与之对应的后台会跳出循环
           await  WebsocketClientCollection.Instance.RemoveClient(websocketClient, WebSocketCloseStatus.Empty);
            _logger.LogInformation($"Websocket client closed.");



        }
        private void HandleMessage(Message message)
        {
            var client = WebsocketClientCollection.Instance.Get(message.SendClientId);
            switch (message.Action)
            {
                case "join":
                    client.RoomNo = message.RoomNo;
                    WebsocketClientCollection.Instance.SendMessageToAll($"{message.Nick} join room {client.RoomNo} success .");
                    _logger.LogInformation($"Websocket client {message.SendClientId} join room {client.RoomNo}.");
                    break;
                case "send_to_room":
                    if (string.IsNullOrEmpty(client.RoomNo))
                    {
                        break;
                    }
                    var clients = WebsocketClientCollection.Instance.GetClientsByRoomNo(client.RoomNo);
                  
                      WebsocketClientCollection.Instance.SendMessageToAll(message.Nick + " : " + message.Msg);
                   
                    _logger.LogInformation($"Websocket client {message.SendClientId} send message {message.Msg} to room {client.RoomNo}");
                    break;
                case "leave":
                    #region 通过把连接的RoomNo置空模拟关闭连接
                    var roomNo = client.RoomNo;
                    client.RoomNo = "";
                    #endregion

                    #region 后台关闭连接
                    //client.WebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                    //WebsocketClientCollection.Remove(client); 
                    #endregion

                    WebsocketClientCollection.Instance.SendMessageToAll($"{message.Nick} leave room {roomNo} success .");
                    _logger.LogInformation($"Websocket client {message.SendClientId} leave room {roomNo}");
                    break;
                default:
                    break;
            }
        }
    }
}
