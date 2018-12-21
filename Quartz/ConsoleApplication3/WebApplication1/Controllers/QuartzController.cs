
using Newtonsoft.Json;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Calendar;
using Quartz.Impl.Matchers;
using Quartz.Impl.Triggers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Entitys;

namespace WebApplication1.Controllers
{
    [RoutePrefix("quartz")]
    public class QuartzController : Controller
    {
        static IScheduler scheduler = null;

        static QuartzController()
        {
            scheduler = StdSchedulerFactory.GetDefaultScheduler();

            scheduler.Start();
        }

        [Route("index")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("joblist")]
        public JsonResult JobList()
        {
            List<JobDetailImpl> jobList = new List<JobDetailImpl>();

            //第一步：获取所有的job信息
            var jobKeySet = scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup());

            foreach (var jobKey in jobKeySet)
            {
                try
                {
                    var info = scheduler.GetJobDetail(jobKey);
                }
                catch (Exception ex)
                {
                    throw;
                }

                var jobDetail = (JobDetailImpl)scheduler.GetJobDetail(jobKey);

                jobList.Add(jobDetail);
            }

            var json = JsonConvert.SerializeObject(jobList.Select(i => new
            {
                i.Name,
                i.Group,
                i.Durable,
                i.JobDataMap,
                i.Description,
                TriggerList = string.Join(",", scheduler.GetTriggersOfJob(new JobKey(i.Name, i.Group))
                                    .Select(m => m.Key.ToString()))
            }));

            return Json(json);
        }

        [Route("addjob")]
        public JsonResult AddJob(JobRequestEntity jobRequest)
        {
            try
            {
                var exists = scheduler.CheckExists(new JobKey(jobRequest.JobName,
                                                              jobRequest.JobGroupName));

                if (exists && !jobRequest.IsEdit)
                {
                    return Json("已经存在同名的Job，请更换！");
                }

                var assemblyName = jobRequest.JobFullClass.Split('.')[0];
                var fullName = jobRequest.JobFullClass;
                var dllpath = Request.MapPath(string.Format("~/bin/{0}.dll", assemblyName));

                //需要执行的job名称
                var jobClassName = Assembly.LoadFile(dllpath)
                                           .CreateInstance(jobRequest.JobFullClass);

                //第一种方式：
                var job = JobBuilder.Create(jobClassName.GetType()).StoreDurably(true)
                                          .WithIdentity(jobRequest.JobName, jobRequest.JobGroupName)
                                          .WithDescription(jobRequest.Description)
                                          .Build();

                scheduler.AddJob(job, true);

                //第二种方式：获取job信息，再更新实体。。。
                var jobdetail = scheduler.GetJobDetail(new JobKey(jobRequest.JobName, jobRequest.JobGroupName));

                return Json("1");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("pausejob")]
        public JsonResult PauseJob(string jobName, string groupName)
        {
            try
            {
                //job暂停，所有关联的trigger也必须暂停
                scheduler.PauseJob(new JobKey(jobName, groupName));

                return Json(1);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("resumejob")]
        public JsonResult ResumeJob(string jobName, string groupName)
        {
            try
            {
                scheduler.ResumeJob(new JobKey(jobName, groupName));

                return Json(1);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("removejob")]
        public JsonResult RemoveJob(string jobName, string groupName)
        {
            try
            {
                var isSuccess = scheduler.DeleteJob(new JobKey(jobName, groupName));

                return Json(isSuccess);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("triggerlist")]
        public JsonResult TriggerList()
        {
            List<CronTriggerImpl> triggerList = new List<CronTriggerImpl>();

            //第一步：获取所有的trigger信息
            var triggerKeys = scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.AnyGroup());

            foreach (var triggerkey in triggerKeys)
            {
                var triggerDetail = (CronTriggerImpl)scheduler.GetTrigger(triggerkey);

                triggerList.Add(triggerDetail);
            }

            var json = JsonConvert.SerializeObject(triggerList.Select(i => new
            {
                i.FullName,
                i.FullJobName,
                i.CronExpressionString,
                JobClassName = scheduler.GetJobDetail(i.JobKey).JobType.FullName,
                StartFireTime = i.StartTimeUtc.LocalDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                PrevFireTime = i.GetPreviousFireTimeUtc()?.LocalDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                NextFireTime = i.GetNextFireTimeUtc()?.LocalDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                TriggerStatus = scheduler.GetTriggerState(new TriggerKey(i.Name, i.Group)).ToString(),
                i.Priority,
                i.Description,
                i.Name,
                i.Group,
                i.CalendarName
            }));

            return Json(json);
        }

        [Route("addtrigger")]
        public JsonResult AddTrigger(TriggerRequestEntity triggerRequest)
        {
            try
            {
                var exists = scheduler.CheckExists(new TriggerKey(triggerRequest.TriggerName,
                                                                 triggerRequest.TriggerGroupName));

                if (exists)
                {
                    return Json("已经存在同名的trigger，请更换！");
                }

                var forJobName = triggerRequest.ForJobName.Split('.');
                var trigger = TriggerBuilder.Create().ForJob(forJobName[1], forJobName[0])
                                            .WithIdentity(triggerRequest.TriggerName, triggerRequest.TriggerGroupName)
                                            .WithCronSchedule(triggerRequest.CronExpress)
                                            .WithDescription(triggerRequest.Description)
                                            .Build();

                scheduler.ScheduleJob(trigger);

                return Json("1");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("pausetrigger")]
        public JsonResult PauseTrigger(string name, string group)
        {
            try
            {
                //暂停 “某一个trigger”
                scheduler.PauseTrigger(new TriggerKey(name, group));

                return Json("1");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("resumetrigger")]
        public JsonResult ResumeTrigger(string name, string group)
        {
            try
            {
                //暂停 “某一个trigger”
                scheduler.ResumeTrigger(new TriggerKey(name, group));

                return Json("1");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("removetrigger")]
        public JsonResult RemoveTrigger(string name, string group)
        {
            try
            {
                scheduler.UnscheduleJob(new TriggerKey(name, group));

                return Json("1");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("edittrigger")]
        public JsonResult EditTrigger(TriggerRequestEntity triggerRequest)
        {
            try
            {
                //编辑Trigger
                var forJobName = triggerRequest.ForJobName.Split('.');

                var trigger = TriggerBuilder.Create().ForJob(forJobName[1], forJobName[0])
                                            .WithIdentity(triggerRequest.TriggerName, triggerRequest.TriggerGroupName)
                                            .WithCronSchedule(triggerRequest.CronExpress)
                                            .WithDescription(triggerRequest.Description)
                                            .Build();

                //编辑trigger操作
                scheduler.RescheduleJob(new TriggerKey(triggerRequest.TriggerName, triggerRequest.TriggerGroupName), trigger);

                return Json("1");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("getmeta")]
        public JsonResult GetMeta()
        {
            try
            {
                var meta = scheduler.GetMetaData();

                var json = JsonConvert.SerializeObject(meta);

                return Json(json);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("resumescheduler")]
        public JsonResult ResumeScheduler()
        {
            try
            {
                if (scheduler.InStandbyMode)
                {
                    //只有暂停的状态，才能重新开启
                    scheduler.Start();
                }

                return Json(1);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("pausescheduler")]
        public JsonResult PauseScheduler()
        {
            try
            {
                if (scheduler.IsStarted)
                {
                    //暂停scheduler
                    scheduler.Standby();
                }

                return Json(1);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("shutdownscheduler")]
        public JsonResult ShutDownScheduler()
        {
            try
            {
                if (scheduler.IsStarted || scheduler.InStandbyMode)
                {
                    //暂停scheduler
                    scheduler.Shutdown();
                }

                return Json(1);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("addcalendar")]
        public JsonResult AddCalendar(CalendarRequestEntity calendarReq)
        {
            try
            {
                var calendar = scheduler.GetCalendar(calendarReq.calendarname);

                //获取calendar
                if (calendar != null)
                {
                    return Json("已经有同名的calendar添加，请勿添加");
                }

                if (calendarReq.calendartype == "DailyCalendar")
                {
                    var segment = calendarReq.selectdate.Split(',');
                    var starttime = DateBuilder.DateOf(Convert.ToInt32(segment[0]), 0, 0).DateTime;
                    var endtime = DateBuilder.DateOf(Convert.ToInt32(segment[1]), 0, 0).DateTime;

                    DailyCalendar dailyCalendar = new DailyCalendar(starttime, endtime);

                    scheduler.AddCalendar(calendarReq.calendarname, dailyCalendar, true, true);

                    //将这个calendar设置给trigger
                    var tkeys = calendarReq.triggerkey.Split('.');

                    var trigger = scheduler.GetTrigger(new TriggerKey(tkeys[1], tkeys[0]));

                    if (trigger != null)
                    {
                        var newTrigger = trigger.GetTriggerBuilder().ModifiedByCalendar(calendarReq.calendarname).Build();

                        scheduler.RescheduleJob(trigger.Key, newTrigger);
                    }
                }

                return Json(1);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("editcalendar")]
        public JsonResult EditCalendar(CalendarRequestEntity calendarReq)
        {
            try
            {
                var calendar = scheduler.GetCalendar(calendarReq.calendarname);

                //获取calendar
                if (calendar == null)
                {
                    return Json("当前scheduler中没有calendar数据，请先进行添加");
                }

                if (calendarReq.calendartype == "DailyCalendar")
                {
                    var segment = calendarReq.selectdate.Split(',');
                    var starttime = DateBuilder.DateOf(Convert.ToInt32(segment[0]), 0, 0).DateTime;
                    var endtime = DateBuilder.DateOf(Convert.ToInt32(segment[1]), 0, 0).DateTime;

                    DailyCalendar dailyCalendar = new DailyCalendar(starttime, endtime);

                    //这里的add，是可以更新指定的 calendarname，还可以更新 trigger 触发器
                    scheduler.AddCalendar(calendarReq.calendarname, dailyCalendar, true, true);
                }

                return Json(1);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("removecalendar")]
        public JsonResult RemoveCalendar(string calendarname)
        {
            try
            {
                //遍历所有的trigger，然后删除 deleteCalendar
                var triggerkeys = scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.AnyGroup());

                foreach (var tkey in triggerkeys)
                {
                    var trigger = scheduler.GetTrigger(tkey);

                    if (trigger.CalendarName == calendarname)
                    {
                        var newtrigger = trigger.GetTriggerBuilder().ModifiedByCalendar(null).Build();

                        scheduler.RescheduleJob(newtrigger.Key, newtrigger);
                    }
                }

                //只有先解除引用，才能删除calendar
                var isSuccess = scheduler.DeleteCalendar(calendarname);

                return Json(1);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("calendarlist")]
        public JsonResult CalendarList()
        {
            try
            {
                List<DailyCalendar> calendars = new List<DailyCalendar>();

                var calendarNameList = scheduler.GetCalendarNames();

                var response = from calendarname in calendarNameList
                               let calendar = (DailyCalendar)scheduler.GetCalendar(calendarname)
                               select new
                               {
                                   CalendarName = calendarname,
                                   RangeStartTime = calendar.GetTimeRangeStartingTimeUtc(DateTimeOffset.Now)
                                                            .ToString("yyyy-MM-dd HH:mm:ss"),
                                   RangeEndTime = calendar.GetTimeRangeEndingTimeUtc(DateTimeOffset.Now)
                                                          .ToString("yyyy-MM-dd HH:mm:ss"),
                                   calendar.Description
                               };

                var json = JsonConvert.SerializeObject(response);

                return Json(json);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
