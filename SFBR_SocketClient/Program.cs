using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace SFBR_SocketClient
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
                host.Service<SocketMain>(ss =>
                {
                    ss.ConstructUsing(() => new SocketMain());
                    ss.WhenStarted(s => s.Start());
                    ss.WhenStopped(s => s.Stop());
                });
                host.SetDescription("SFBR_SocketClient");
                host.SetDisplayName("Topshelf SFBR_SocketClient service");
                host.SetServiceName("SFBR_SocketClient");
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
