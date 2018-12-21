using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class HelloJob : IJob
    {
        static int index = 1;
        public void Execute(IJobExecutionContext context)
        {
            //现在时间， schduler调度时间， 下次触发时间 都打出来

            Console.WriteLine("index={0},current={1}, scheuler={2},nexttime={3}",
                                            index++, DateTime.Now,
                                            context.ScheduledFireTimeUtc?.LocalDateTime,
                                            context.NextFireTimeUtc?.LocalDateTime);
        }
    }
}
