/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.Quartz.Demo
*文件名： Program
*创建人： Lxsh
*创建时间：2018/12/18 10:27:18
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/18 10:27:18
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Calendar;
using Quartz.Impl.Matchers;
using Quartzlog=Common.Logging;
using Topshelf;

namespace Lxsh.Project.Quartz.Demo
{
    class Program
    {
       static Common.LxshManualautorSet lxshManualautorSet = new Common.LxshManualautorSet();
        static void Main(string[] args)
        {


            HostFactory.Run(x =>                                 //1
            {
                x.Service<QuartzService>(s =>                        //2
                {
                    s.ConstructUsing(name => new QuartzService());     //3
                    s.WhenStarted(tc => tc.Start());              //4
                    s.WhenStopped(tc => tc.Stop());               //5
                });

                x.RunAsLocalSystem();                            //6

                x.SetDescription("我们的Quartz Cluster集群服务");        //7
                x.SetDisplayName("QuartzCluster");                       //8
                x.SetServiceName("QuartzCluster");                       //9
            });

        }
        static void TestQuarz()
        {
            #region 测试验证
            //  Common.Config.PropertiesParser.ReadFromFileResource("test.txt");
            // Run();
            //    Set();  
            //     Console.Read();
            #endregion


            //  Quartzlog.LogManager.Adapter=new  

            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();  //调度者


            WeeklyCalendar calendar = new WeeklyCalendar();
            calendar.SetDayExcluded(DayOfWeek.Thursday, true);      //设置每周五不能执行; 


            HolidayCalendar daycalendar = new HolidayCalendar();
            daycalendar.AddExcludedDate(DateTime.Now);             //只取日   DateTime.Now.Day 


            MonthlyCalendar monthcalendar = new MonthlyCalendar();
            monthcalendar.SetDayExcluded(16, true);                //只取月   DateTime.Now.Month 

            AnnualCalendar annualCalendar = new AnnualCalendar();
            annualCalendar.SetDayExcluded(DateTime.Now, true);      //只取年月日   DateTime.Now.Year 

            CronCalendar cronCalendar = new CronCalendar("* * * 17 6 ?"); //6月17 不执行

            scheduler.AddCalendar("mycalendar", calendar, true, true);//设置每周五不能执行; 
            scheduler.AddCalendar("mycalendar", daycalendar, true, true);     //某一天不执行 
            scheduler.AddCalendar("mycalendar", monthcalendar, true, true);     //某每月某一天不执行 
            scheduler.AddCalendar("mycalendar", annualCalendar, true, true);     //每年某一月某一日不执行

            scheduler.AddCalendar("mycalendar", cronCalendar, true, true);     //每年某一月某一日不执行



            scheduler.Start();
            var job = JobBuilder.Create<lxshJob>().WithDescription("Job")
                                                  .WithIdentity("lxshJob", "lxshGroup")
                                                  .UsingJobData("userName", "Joblxsh")
                                                  .Build(); //任务

            var job1 = JobBuilder.Create<lxshJob>().WithDescription("Job")
                                              .WithIdentity("lxshJob", "lxshGroup")
                                              .UsingJobData("userName", "Joblxsh1")
                                              .Build(); //任务

            var trigger = TriggerBuilder.Create().StartNow()
                                              .WithDescription("trigger")
                                                .WithSimpleSchedule(x => x.WithIntervalInSeconds(1).WithRepeatCount(10))
                                               // .WithCalendarIntervalSchedule(x=>x.WithIntervalInYears(1))
                                               //   .WithSimpleSchedule(x => x.WithIntervalInSeconds(1).RepeatForever())
                                               .WithDailyTimeIntervalSchedule(x =>
                                               {
                                                   x.OnDaysOfTheWeek(new DayOfWeek[2] { DayOfWeek.Tuesday, DayOfWeek.Friday });  //每周二或者周五执行
                                                   x.OnEveryDay()
                                                  .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(8, 00)) //   八点开始
                                                  .EndingDailyAt(TimeOfDay.HourAndMinuteOfDay(18, 00))  //    十八点开始
                                                  .WithIntervalInSeconds(1);     // 每1s执行一次 
                                               })
                                               // .ModifiedByCalendar("mycalendar")                        
                                               .Build();
            //秒分时日月
            var trigger1 = TriggerBuilder.Create().WithCronSchedule("* * * * * ?").Build();  //1秒执行一次
            var trigger2 = TriggerBuilder.Create().WithCronSchedule("0 * * * * ?").Build(); //1分钟执行一次
            var trigger3 = TriggerBuilder.Create().WithCronSchedule("0 0/30 8-20 * * ?").Build(); //30分钟检查一次 8-20点
            var trigger4 = TriggerBuilder.Create().WithCronSchedule("* * * * * ?").Build();

            scheduler.ListenerManager.AddJobListener(new lxshJobLinstener(), GroupMatcher<JobKey>.AnyGroup());

            scheduler.ScheduleJob(job, trigger);  //开始调度任务
                                                  //    scheduler.GetTriggersOfJob(new JobKey("lxshJob")).Select(x =>x.Key.ToString() );
            Console.Read();

            #region WithCronSchedule
            //           --------------------------------------
            //   0 0 12 * * ? 每天12点触发

            //   0 15 10 ? **每天10点15分触发

            //   0 15 10 * * ? 每天10点15分触发

            //   0 15 10 * * ? *每天10点15分触发

            //   0 15 10 * * ? 2005 2005年每天10点15分触发

            // 0 * 14 * * ? 每天下午的 2点到2点59分每分触发

            // 0 0 / 5 14 * * ? 每天下午的 2点到2点59分(整点开始，每隔5分触发)

            // 0 0 / 5 14,18 * * ? 每天下午的 2点到2点59分(整点开始，每隔5分触发) 每天下午的 18点到18点59分(整点开始，每隔5分触发)

            // 0 0 - 5 14 * * ? 每天下午的 2点到2点05分每分触发

            // 0 10,44 14 ? 3 WED 3月分每周三下午的 2点10分和2点44分触发

            // 0 15 10 ? *MON - FRI 从周一到周五每天上午的10点15分触发

            // 0 15 10 15 * ? 每月15号上午10点15分触发

            // 0 15 10 L * ? 每月最后一天的10点15分触发

            // 0 15 10 ? *6L 每月最后一周的星期五的10点15分触发

            // 0 15 10 ? *6L 2002 - 2005 从2002年到2005年每月最后一周的星期五的10点15分触发

            // 0 15 10 ? *6#3         每月的第三周的星期五开始触发
            //    0 0 12 1 / 5 * ? 每月的第一个中午开始每隔5天触发一次

            // 0 11 11 11 11 ? 每年的11月11号 11点11分触发(光棍节)
            //--------------------------------------                    


            #endregion
        }
        static void Run()
        {
            for (int i = 0; i < 5; i++)
            {
                int j = i;
                Task.Factory.StartNew((obj) =>
                {
                    Console.WriteLine($"{j}号准备已完成");
                    lxshManualautorSet.WaitOne();
                    Console.WriteLine($"{j}号执行已完成");
                }, j);
            }  
        }
        static void Set()
        {
            Task.Factory.StartNew(() =>
            {
                System.Threading.Thread.Sleep(5000);
                Console.WriteLine("全部准备已完成");
                lxshManualautorSet.Set();
            });
        }
        static void SetAll()
        {
            Task.Factory.StartNew(() =>
            {
                System.Threading.Thread.Sleep(5000);
                Console.WriteLine("全部准备已完成");
                lxshManualautorSet.SetAll(); 
                Console.WriteLine("全部执行已完成");
            });
           
        }
    }
      public class lxshJobLinstener : IJobListener
    {
        public string Name =>"lxsh";

        public void JobExecutionVetoed(IJobExecutionContext context)
        {
        
            Console.WriteLine("JobExecutionVetoed");
          // throw new NotImplementedException();
        }

        public void JobToBeExecuted(IJobExecutionContext context)
        {
         //   Console.WriteLine("JobToBeExecuted");
            //throw new NotImplementedException();
        }

        public void JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException)
        {
           // Console.WriteLine("JobWasExecuted");
           // throw new NotImplementedException();
        }
    }
}
