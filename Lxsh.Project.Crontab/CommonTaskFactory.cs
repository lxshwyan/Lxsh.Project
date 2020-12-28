using Lxsh.Project.JobsClass;
using NLog;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lxsh.Project.Crontab
{
	public static class CommonTaskFactory
	{
		public static Logger logger = LogManager.GetLogger(nameof(CommonTaskFactory));
		public static async Task CreateTask(IScheduler scheduler, TaskConfigModel taskConfig, Type obj)
		{
			try
			{
				IJobDetail job = JobBuilder.Create(obj)
					.WithIdentity(taskConfig.JobName, taskConfig.GroupName)
					.Build();

				TriggerBuilder triggerBuilder = TriggerBuilder.Create()
					.WithIdentity(taskConfig.TriggerName, taskConfig.GroupName)
					.StartNow();

				//设置时间间隔
				switch (taskConfig.TimeIntervalType)
				{
					case (int)TimeIntervalTypeEnum.Seconds:
						triggerBuilder.WithSimpleSchedule(y => y.WithIntervalInSeconds(taskConfig.TimeInterval).RepeatForever());
						break;
					case (int)TimeIntervalTypeEnum.Minutes:
						triggerBuilder.WithSimpleSchedule(y => y.WithIntervalInMinutes(taskConfig.TimeInterval).RepeatForever());
						break;
					case (int)TimeIntervalTypeEnum.Hours:
						triggerBuilder.WithSimpleSchedule(y => y.WithIntervalInHours(taskConfig.TimeInterval).RepeatForever());
						break;
					case (int)TimeIntervalTypeEnum.DailyTime:
						triggerBuilder.WithDailyTimeIntervalSchedule(y => y.OnEveryDay().StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(taskConfig.Hours, taskConfig.Minutes)));
						break;
				}

				ITrigger trigger = triggerBuilder.Build();
				await scheduler.ScheduleJob(job, trigger);
			}
			catch (SchedulerException se)
			{
				logger.Error(se.InnerException.ToString());
				Console.WriteLine(se);
			}
		}
	}
	/// <summary>
	/// 时间间隔类型
	/// </summary>
	public enum TimeIntervalTypeEnum
	{
		/// <summary>
		/// 秒
		/// </summary>
		Seconds = 1,
		/// <summary>
		/// 分
		/// </summary>
		Minutes = 2,
		/// <summary>
		/// 小时
		/// </summary>
		Hours = 3,
		/// <summary>
		/// 每天指定的时间
		/// </summary>
		DailyTime = 4
	}
}
