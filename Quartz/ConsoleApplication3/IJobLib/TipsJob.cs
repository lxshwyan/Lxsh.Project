using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJobLib
{
    public class TipsJob : IJob
    {
        static int index = 1;

        public void Execute(IJobExecutionContext context)
        {
            Debug.WriteLine(" TipsJob index={0},current={1}, scheuler={2},nexttime={3}",
                                       index++, DateTime.Now,
                                       context.ScheduledFireTimeUtc?.LocalDateTime,
                                       context.NextFireTimeUtc?.LocalDateTime);
        }
    }
}
