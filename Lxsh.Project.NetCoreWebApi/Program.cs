using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Lxsh.Project.NetCoreWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
              .ConfigureAppConfiguration((builderContext, config) =>
              {
                  config.AddJsonFile("IllegalKeywords.json", optional: false, reloadOnChange: true);// ���ÿ�������
                  config.AddJsonFile("IllegalUrls.json", optional: false, reloadOnChange: true);// ���ÿ�������
              })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
