using System;
using Microsoft.Extensions.Configuration;

namespace Lxsh.Project.NetCore.ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {    

           IConfiguration  config=new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
               .AddJsonFile($"appSetting.json", optional: true, reloadOnChange: true)
               //.AddXmlFile("appsettings.xml") 
               //.AddEnvironmentVariables()
               .Build();

            var rootobject = config.Get<Rootobject>();

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}

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
