/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.Quartz.Demo
*文件名： QuartzService
*创建人： Lxsh
*创建时间：2018/12/19 17:35:13
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/19 17:35:13
*修改人：Lxsh
*描述：
************************************************************************/
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.Quartz.Demo
{
    public class QuartzService
    {
        IScheduler scheduler = null;

        public QuartzService()
        {
            var properties = new NameValueCollection();

            properties["quartz.dataSource.sqlserver.provider"] = "SqlServer-20";
            properties["quartz.dataSource.sqlserver.connectionString"] = @"Data Source='127.0.0.1';Initial Catalog='quartz';User ID='sa';Password='123456'";

            properties["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz";
            properties["quartz.jobStore.dataSource"] = "sqlserver";
            properties["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.SqlServerDelegate, Quartz";

            //cluster 集群指定
            properties["quartz.jobStore.clustered"] = "true";
            properties["quartz.scheduler.instanceId"] = "AUTO";

            var factory = new StdSchedulerFactory(properties);

            //scheduler
            scheduler = factory.GetScheduler();

            //job
            var job = JobBuilder.Create<lxshJob>()
                                .StoreDurably(true)
                                .WithIdentity("lxshJob").Build();

            //trigger   1s执行一次
            var trigger = TriggerBuilder.Create().ForJob(job)
                                        .UsingJobData("key", "trigger")
                                        .WithSimpleSchedule(x => x.WithIntervalInSeconds(1)
                                                                  .RepeatForever()).Build();

            var isExists = scheduler.CheckExists(job.Key);

            if (!isExists)
            {
                //开始调度
                scheduler.ScheduleJob(job, trigger);
            }
        }

        public void Start()
        {
            scheduler.Start();
        }

        public void Stop()
        {
            scheduler.Shutdown(true);
        }
    }
}