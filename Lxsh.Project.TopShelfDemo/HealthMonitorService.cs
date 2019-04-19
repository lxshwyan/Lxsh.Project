/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lxsh.Project.TopShelfDemo
*文件名： HealthMonitorService
*创建人： Lxsh
*创建时间：2019/4/19 13:57:19
*描述
*=======================================================================
*修改标记
*修改时间：2019/4/19 13:57:19
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Threading;
using NLog;

namespace Lxsh.Project.TopShelfDemo
{
   public  class HealthMonitorService
    {
       
        /// <summary>
        /// 检测周期(秒)
        /// </summary>
        private int _monitorInterval = 10;
        /// <summary>
        /// 要守护的应用程序列表
        /// </summary>
        private List<ApplicationInfo> _Apps;
        public  Logger logger = LogManager.GetLogger(nameof(HealthMonitorService));
        public HealthMonitorService()
        {
            //在这不能读取配置文件  ....
          
        }

        private List<ApplicationInfo> GetAppsInfo()
        {
            List<ApplicationInfo> apps = new List<ApplicationInfo>();
            FileStream fileStream = new FileStream("AppsInfo.xml", FileMode.Open);
            XmlTextReader xmlTextReader = new XmlTextReader(fileStream);
            while (xmlTextReader.Read())
            {
                if (xmlTextReader.Name == "File")
                {
                    string _ProcessName = xmlTextReader.GetAttribute("ProcessName");
                    string _AppDisplayName = xmlTextReader.GetAttribute("AppDisplayName");
                    string _AppFilePath = xmlTextReader.GetAttribute("AppFilePath");
                    string _Args = xmlTextReader.GetAttribute("Args");
                    if (!string.IsNullOrWhiteSpace(_ProcessName))
                    {
                        apps.Add(new ApplicationInfo()
                        {
                            AppDisplayName = _AppDisplayName,
                            ProcessName = _ProcessName,
                            AppFilePath = _AppFilePath,
                            Args = _Args

                        });
                    }
                   
                }
            }
            return apps;
        }
        /// <summary>
        /// 守护应用程序的方法
        /// </summary>
        private void Monitor()
        {
            
            foreach (var app in _Apps)
            {
                // 判断当前进程是存已启动
                if (ProcessorHelper.IsProcessExists(app.ProcessName))
                {
                    if (!ProcessorHelper.IsSystemProcess(app.ProcessName))
                    {
                          Console.WriteLine("【{0}】Application[{1}] already exists.",DateTime.Now.ToString(), app.ProcessName);
                          return;
                    }
                    else
                    {
                        logger.Info("[{0}],该进程为系统启动的进程需要结束",app.ProcessName );
                        ProcessorHelper.KillProcess(app.ProcessName);
                    }
                }
                try
                {
                    logger.Info("【{0}】Application[{1}] not exists.Prepare to restart. ",DateTime.Now.ToString(), app.ProcessName);
                    Console.WriteLine("【{0}】Application[{1}] not exists.Prepare to restart. ",DateTime.Now.ToString(), app.ProcessName);
                    // 当前主机进程列表中没有需要守护的进程名称，则启动这个进程对应的应用程序
                    ProcessorHelper.RunProcess(app.AppFilePath, app.Args);
                  
                }
                catch (Exception ex)
                {
                    logger.Fatal(ex,"Start {0} application failed",app.ProcessName );
                    Console.WriteLine("Start application failed:{0}", ex);
                }
            }
        }

        public void Start()
        {
            _Apps = GetAppsInfo();
            Task.Run(() =>
            {
                while (true)
                {
                    Monitor();
                    Thread.Sleep(_monitorInterval * 1000);
                }

            });

        }
        public void Stop()
        {  
        }
    }
}