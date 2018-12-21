/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.Quartz.Demo
*文件名： lxshJob
*创建人： Lxsh
*创建时间：2018/12/18 10:27:18
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/18 10:27:18
*修改人：Lxsh
*描述：
************************************************************************/
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.Quartz.Demo
{

    [DisallowConcurrentExecution]  //一个一个执行  （每次任务执行完成才能执行下一个任务）
    [PersistJobDataAfterExecution]
    public class lxshJob : IJob ,IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("当前任务执行成功已释放");
        }
        static int index = 1;
        public void Execute(IJobExecutionContext context)
        {
            // [PersistJobDataAfterExecution]   加了这个特性， 才能对  JobDetail里面键值队赋值 （因为默认是无状态的，每次执行的是重新初始化JobDetail对象）
          
            //string str = context.JobDetail.JobDataMap["userName"].ToString();
            //Console.WriteLine(DateTime.Now.ToString()+ str);
            //context.JobDetail.JobDataMap["userName"] = str + 1;
            //System.Threading.Thread.Sleep(5000);  //测试验证 DisallowConcurrentExecution


            var info = string.Format("{4} index={0},current={1}, scheuler={2},nexttime={3}",
                                           index++, DateTime.Now,
                                           context.ScheduledFireTimeUtc?.LocalDateTime,
                                           context.NextFireTimeUtc?.LocalDateTime,
                                           context.JobDetail.JobDataMap["key"]);
            Console.WriteLine(info);
        }

    }
}
