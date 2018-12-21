using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJobLib
{
    public class HelloJob : IJob
    {
        static int index = 1;

        public void Execute(IJobExecutionContext context)
        {
            Debug.WriteLine(" helloJob index={0},current={1}, scheuler={2},nexttime={3}",
                                            index++, DateTime.Now,
                                            context.ScheduledFireTimeUtc?.LocalDateTime,
                                            context.NextFireTimeUtc?.LocalDateTime);
        }
    }
}
