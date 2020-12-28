using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace System
{
    public class SocketModel
    {
        public Socket socket;
        public byte[] buffer;

        public SocketModel(Socket socket, byte[] buffer)
        {
            this.socket = socket;
            this.buffer = buffer;
        }
    }

    public class SocketReqModel
    {
        public object response { get; set; }
        public string message { get; set; }
        public SocketReqModel() { }

        public SocketReqModel(object response)
        {
            this.response = response;
        }
        public SocketReqModel(object response, string message)
        {
            this.response = response;
            this.message = message;
        }
    }

    public class SocketRespModel
    {
        public int status { get; set; }
        public object data { get; set; }
        public string message { get; set; }
    }
}
