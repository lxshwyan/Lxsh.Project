using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            var info = Assembly.Load("IJobLib").CreateInstance("IJobLib.HelloJob");

            //Quartz.ScheduleBuilder

            //scheduler
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

            scheduler.Start();

            //job
            var job = JobBuilder.Create<HelloJob>().Build();

            //trigger   1s执行一次
            var trigger = TriggerBuilder.Create().WithSimpleSchedule(x => x.WithIntervalInSeconds(1)
                                                                           .RepeatForever()).Build();

            //开始调度
            scheduler.ScheduleJob(job, trigger);

            Console.Read();
        }
    }
}
