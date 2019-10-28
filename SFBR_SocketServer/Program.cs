using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Topshelf;

namespace SFBR_SocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                Console.WriteLine(e);
            };
           HostFactory.Run(host =>
            {
                host.Service<SocketMain>(ss=>
                {
                    ss.ConstructUsing(() => new SocketMain());
                    ss.WhenStarted(s => s.Start());
                    ss.WhenStopped(s => s.Stop());
                });   
                host.SetDescription("Sfbr_SocketMain");
                host.SetDisplayName("Topshelf Sfbr_SocketMain service");
                host.SetServiceName("Sfbr_SocketMainService");
                host.StartAutomatically();
                host.RunAsLocalSystem();
                host.EnableServiceRecovery(reStart =>
                {
                    reStart.RestartService(1);
                    reStart.OnCrashOnly();
                    reStart.SetResetPeriod(1);
                });
            });
         
        }
    }
}
