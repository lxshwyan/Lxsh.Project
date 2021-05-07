
using System;

using Grpc.Core;
using Grpc.Net.Client;
using Lxsh.Project.GrpcServiceDemo;
using static Lxsh.Project.GrpcServiceDemo.Greeter;

namespace Lxsh.Project.GrpcClient
{
    class Program
    {
        static void Main(string[] args)
        {


            GrpcChannel grpcChannel = GrpcChannel.ForAddress("http://l27.0.0.1:6666");

            GreeterClient greeterClient = new Greeter.GreeterClient(grpcChannel);

            HelloReply helloReply = greeterClient.SayHello(new HelloRequest()

            {

                Name = "C#调用"

            });

            Console.WriteLine(helloReply.Message);

            grpcChannel.Dispose();


        }
    }
}
