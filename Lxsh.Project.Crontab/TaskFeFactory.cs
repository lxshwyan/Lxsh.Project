using Lxsh.Project.JobsClass;
using NLog;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Lxsh.Project.JobsClass.XmlHelper;

namespace Lxsh.Project.Crontab
{
  public  class TaskFeFactory
    {
        private IScheduler scheduler;
		public Logger logger = LogManager.GetLogger(nameof(TaskFeFactory));
		public  TaskFeFactory()
        {
            InitScheduler();
        }

        private async void InitScheduler()
        {
            //创建计划工厂
            StdSchedulerFactory factory = new StdSchedulerFactory();
            this.scheduler = await factory.GetScheduler();
            await scheduler.Start();
        }
		public async void CreateFactoryObj()
		{
			//加载定时任务配置
			TaskList taskList = XmlHelper.ReadXMLFile();
			logger.Info("定时任务总数：" + taskList.taskConfigModel.Count.ToString());
			foreach (var items in taskList.taskConfigModel)
			{
				string jobsDllName = ConfigurationManager.AppSettings["jobsDllName"].ToString();
				Assembly assembly = Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + jobsDllName);
				var types = assembly.GetTypes();
				foreach (var typItem in types)
				{
					if (typItem.Name == items.JobClass && items.Enable)
					{
						//Type obj = assembly.GetType(typItem.FullName);
						Task _task = CommonTaskFactory.CreateTask(scheduler, items, typItem);
						_task.GetAwaiter().GetResult();
					}
				}
			}
		}

	}
}
