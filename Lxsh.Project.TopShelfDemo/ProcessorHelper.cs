/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lxsh.Project.TopShelfDemo
*文件名： ProcessorHelper
*创建人： Lxsh
*创建时间：2019/4/19 13:52:33
*描述
*=======================================================================
*修改标记
*修改时间：2019/4/19 13:52:33
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.TopShelfDemo
{
    public class ProcessorHelper
    {
        /// <summary>
        /// 获取当前计算机所有的进程列表(集合)
        /// </summary>
        /// <returns></returns>
        public static List<Process> GetProcessList()
        {
            return GetProcesses().ToList();
        }

        /// <summary>
        /// 获取当前计算机所有的进程列表(数组)
        /// </summary>
        /// <returns></returns>
        public static Process[] GetProcesses()
        {
            var processList = Process.GetProcesses();
            return processList;
        }

        /// <summary>
        /// 判断指定的进程是否存在
        /// </summary>
        /// <param name="processName"></param>
        /// <returns></returns>
        public static bool IsProcessExists(string processName)
        {
            return Process.GetProcessesByName(processName).Length > 0;
        }
        /// <summary>
        /// 判断用户是否系统进程信息 （或系统进程启动的）
        /// </summary>
        /// <param name="processName"></param>
        /// <returns></returns>
        public static bool IsSystemProcess(string processName)
        {
            Process[] process = Process.GetProcessesByName(processName);
            foreach (var item in process)
            {
                string userName = GetProcessUserName(item.Id);
                Console.WriteLine(userName);
                if (userName == "SYSTEM" || userName == "NETWORK SERVICE" || userName == "LOCAL SERVICE")
                {
                    return true;
                }
            }   
            return false;
        }
        private static string GetProcessUserName(int pID)
        {
            string text1 = null;

            SelectQuery query1 = new SelectQuery("Select * from Win32_Process WHERE processID=" + pID);
            ManagementObjectSearcher searcher1 = new ManagementObjectSearcher(query1);

            try
            {
                foreach (ManagementObject disk in searcher1.Get())
                {
                    ManagementBaseObject inPar = null;
                    ManagementBaseObject outPar = null;

                    inPar = disk.GetMethodParameters("GetOwner");

                    outPar = disk.InvokeMethod("GetOwner", inPar, null);

                    text1 = outPar["User"].ToString();
                    break;
                }
            }
            catch
            {
                text1 = "";
            }

            return text1;
        }
        public static void KillProcess(string processName)
        {
           
             Process[] process = Process.GetProcessesByName(processName);
                foreach (var item in process)
                {
                     item.Kill();
                }
            
        }
        /// <summary>
        /// 启动一个指定路径的应用程序
        /// </summary>
        /// <param name="applicationPath"></param>
        /// <param name="args"></param>
        public static void RunProcess(string applicationPath, string args = "")
        {
            try
            {
                ProcessExtensions.StartProcessAsCurrentUser(applicationPath, args);
            }
            catch (Exception e)
            {
                var psi = new ProcessStartInfo
                {
                    FileName = applicationPath,
                    WindowStyle = ProcessWindowStyle.Normal,
                    Arguments = args
                };
                Process.Start(psi);
            }
        }
    }
}