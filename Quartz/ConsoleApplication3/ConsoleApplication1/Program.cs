using Common.Logging;
using ConsoleApplication3;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using Quartz.Listener;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Trace.WriteLine("hello world!");

            LogManager.Adapter = new Common.Logging.Simple.TraceLoggerFactoryAdapter()
            {
                Level = LogLevel.All
            };

            var scheduler = StdSchedulerFactory.GetDefaultScheduler();

            scheduler.Start();

            var job = JobBuilder.Create<HelloJob>()
                                .WithIdentity("test", "datamip")
                                .Build();

            var trigger = TriggerBuilder.Create()
                                        .WithCronSchedule("0 0 2 ? * MON-FRI")
                                        .Build();

            scheduler.ScheduleJob(job, trigger);
            Console.Read();
        }
    }
}
