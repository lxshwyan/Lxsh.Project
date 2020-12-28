using NLog;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.JobsClass
{
    public class TestJob: IJob
    {
        public static Logger logger = LogManager.GetLogger(nameof(TestJob));
        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("执行成功");
            logger.Info("执行成功");
        }

    }
}
