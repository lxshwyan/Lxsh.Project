using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using AspectCore.Extensions.DependencyInjection;
using Lxsh.Project.NetCore.ConsoleDemo.Define;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;    

namespace Lxsh.Project.NetCore.ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // TestConfig();

            // TestService();

            //TestAOP();

            TestADO();
            TestWebServer();
            Console.ReadKey();
        }

        #region TestConfig

        static void TestConfig()
        {

            IConfiguration config = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
                // .AddPropertiesFile("1.properties")
                .AddJsonFile("appSetting.json", optional: true, reloadOnChange: true)
                //.AddXmlFile("appsettings.xml") 
                //.AddEnvironmentVariables()
                .Build();


            var rootobject = config.Get<Rootobject>();
            //while (true)
            //{

            //    var dbopion = config.GetSection("DbOpion").Get<Dbopion>(); ;
            //    Console.WriteLine(dbopion.DbType);
            //    Thread.Sleep(1000);
            //}

        }


        #endregion

        #region TestService

        static void TestService()
        {
            IConfiguration config = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
                // .AddPropertiesFile("1.properties")
                .AddJsonFile("appSetting.json", optional: true, reloadOnChange: true)
                //.AddXmlFile("appsettings.xml") 
                //.AddEnvironmentVariables()
                .Build();
            ServiceCollection service = new ServiceCollection();
            service.AddTransient<IFly, Pig>();
            service.AddLogging();
            service.Configure<Rootobject>("lxsh", config);
            service.Configure<Loglevel>(r => r.Default = "info");
            //   service.Configure<Loglevel>("test", r => r.Default = "info");
            //service.Configure<Loglevel>("test1", r => r.Default = "info1");

            var provider = service.BuildServiceProvider();
            provider.GetService<ILoggerFactory>().AddConsole(LogLevel.Information);
            var fly = provider.GetService<IFly>();
            fly.Fly();
        }


        #endregion

        #region TestAOP

        static void TestAOP()
        {
            ////IOC容器
            //ServiceCollection services = new ServiceCollection();
            //services.AddDynamicProxy();
            //services.AddTransient<IMySql, MySql>();
            //var provider = services.BuildAspectCoreServiceProvider();
            //var mysql = provider.GetService<IMySql>();


            ////走业务逻辑了
            //var msg = mysql.GetData(10);

            //Console.WriteLine(msg);

            ////应该是直接走缓存了。。。
            //msg = mysql.GetData(10);

            //Console.WriteLine(msg);

            //Console.Read();
        }

        #endregion

        #region WebSocket

        static void TestWebServer()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Any, 8081));
            socket.Listen(100);
            socket.BeginAccept(OnAccept, socket);
        }

        public static void OnAccept(IAsyncResult async)
        {
            var serverSocket = async.AsyncState as Socket;

            //获取到客户端的socket
            var clientSocket = serverSocket.EndAccept(async);
            //进行下一步监听
            var bytes = new byte[10000];
            serverSocket.BeginAccept(OnAccept, serverSocket);
            //获取socket的内容
            var len = clientSocket.Receive(bytes);

            //将 bytes[] 转换 string
            var request = Encoding.UTF8.GetString(bytes, 0, len);

            var response = string.Empty;

            if (!string.IsNullOrEmpty(request) && !request.Contains("favicon.ico"))
            {
                // /1.html
                var filePath = request.Split("\r\n")[0].Split(" ")[1].TrimStart('/');

                //获取文件内容
                response = System.IO.File.ReadAllText(filePath, Encoding.UTF8);
            }


            //按照http的响应报文返回
            var responseHeader = string.Format(@"HTTP/1.1 200 OK
                Date: Sun, 26 Aug 2018 03:33:36 GMT
                Server: nginx
                Content-Type: text/html; charset=utf-8
                Cache-Control: no-cache
                Pragma: no-cache
                Via: hngd_ax63.139
                X-Via: 1.1 tjhtapp63.147:3800, 1.1 cbsshdf-A4-2-D-14.32:8101
                Connection: keep-alive
                Content-Length: {0}

", Encoding.UTF8.GetByteCount(response));

            //返回给客户端了
            clientSocket.Send(Encoding.UTF8.GetBytes(responseHeader));
            clientSocket.Send(Encoding.UTF8.GetBytes(response));

            clientSocket.Close();
        }


        #endregion

        #region Ado

        static void TestADO()
        {

         SqlConnection connection =
            new SqlConnection(
                "Data Source='192.168.137.111';Initial Catalog='Lxsh.Project.DB';User ID='sa';Password='123456'");
            connection.Open();
          SqlCommand  sqlCommand=new SqlCommand("select * from Base_User", connection);

            SqlDataAdapter myDataAdapter = new SqlDataAdapter();
            myDataAdapter.SelectCommand = sqlCommand;
            DataSet ds=new DataSet();
            
            myDataAdapter.Fill(ds,"test");
            connection.Close();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Console.WriteLine(ds.Tables[0].Rows[i]["UserID"].ToString());
            }

        }

    #endregion
    }
}

#region Rootobject

public class Rootobject
{
    public Logging Logging { get; set; }
    public Dbopion DbOpion { get; set; }
    public string AllowedHosts { get; set; }
}

public class Logging
{
    public Loglevel LogLevel { get; set; }
}

public class Loglevel
{
    public string Default { get; set; }
}

public class Dbopion
{
    public string ConnectionString { get; set; }
    public string DbType { get; set; }
}

#endregion

#region TestService

public interface IFly
{
    void Fly();
}
public class Pig : IFly
{
     ILogger<Pig> logger = null;          
    protected Rootobject _Rootobjects;

    public Pig(ILoggerFactory loggerFactory, IOptionsSnapshot<Rootobject> Rootobjects, IOptionsSnapshot<Loglevel> Loglevel)
    {
        this.logger = loggerFactory.CreateLogger<Pig>();
         _Rootobjects = Rootobjects.Get("lxsh");
         Loglevel LogLevel=Loglevel.Value;
    }

    public void Fly()
    {
       // logger.LogError("测试log");  
        Console.WriteLine( "猪要飞了");
    }
}
#endregion

#region AOP



/// <summary>
/// 日志切面
/// </summary>
public class MyLogInterceptorAttribute : AbstractInterceptorAttribute
{
    public override Task Invoke(AspectContext context, AspectDelegate next)
    {
        Console.WriteLine("开始记录日志。。。。。");

        var task = next(context);

        Console.WriteLine("结束记录日志。。。。。");

        return task;
    }
}

/// <summary>
/// 缓存切面
/// </summary>
public class MyCacheIntercptorAttribute : AbstractInterceptorAttribute
{
    private Dictionary<string, string> cacheDict = new Dictionary<string, string>();
    public override Task Invoke(AspectContext context, AspectDelegate next)
    {
        var cacheKey = string.Join(",", context.Parameters);
        if (cacheDict.ContainsKey(cacheKey))
        {
            context.ReturnValue = cacheDict[cacheKey].ToString();

            return Task.CompletedTask;
        }
        var task = next(context);

        //ReturnValue 其实就是一个传递值的媒介
        var cacheValue = context.ReturnValue;

        cacheDict.Add(cacheKey, string.Format(" from Cache : {0}", cacheValue.ToString()));

        return task;
    }
}

public interface IMySql
{
    string GetData(int id);
}

public class MySql : IMySql
{
    [MyCacheIntercptor]
    public string GetData(int id)
    {
        Thread.Sleep(2000);  
        string msg = $"当前是id=｛id｝的数据，时间：｛ DateTime.Now.ToString()｝";
        return msg;
    }
}

#endregion

