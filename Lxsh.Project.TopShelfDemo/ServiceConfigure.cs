/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lxsh.Project.TopShelfDemo
*文件名： ServiceConfigure
*创建人： Lxsh
*创建时间：2019/4/19 13:56:59
*描述
*=======================================================================
*修改标记
*修改时间：2019/4/19 13:56:59
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace Lxsh.Project.TopShelfDemo
{
   public class ServiceConfigure
    {
        public static void Configure()
        {
            var rc = HostFactory.Run(host =>                                   
            {
                host.Service<HealthMonitorService>(service =>                  
                {
                    service.ConstructUsing(() => new HealthMonitorService());  
                    service.WhenStarted(s => s.Start());                       
                    service.WhenStopped(s => s.Stop());                        
                });

                host.RunAsLocalSystem();                                       

                host.EnableServiceRecovery(service =>                          
                {
                    service.RestartService(3);                                 
                });
                host.SetDescription("Windows service based on topshelf");      
                host.SetDisplayName("Topshelf demo service");                  
                host.SetServiceName("TopshelfDemoService");                    
                host.StartAutomaticallyDelayed();                              
            });

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());      
            Environment.ExitCode = exitCode;
        }
    }
}