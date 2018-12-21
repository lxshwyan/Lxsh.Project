
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Lxsh.Project.Common.Helper
{
  public  class ProcessHelper
    {
     /// <summary>
        /// 执行Cmd
        /// </summary>
        /// <param name="argument">cmd命令</param>
        /// <param name="msg">返回信息</param>
        /// <param name="directoryPath">路径</param>
        /// <param name="closed">是否关闭</param>
        public static void RunCmd(string argument, out string msg, string directoryPath = "", bool redirect = false)
        {
            msg = string.Empty;
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments=redirect?@"/c "+argument:@"/k "+argument;
            startInfo.UseShellExecute=false;                        //是否需要启动windows shell
            startInfo.CreateNoWindow=false;
            startInfo.RedirectStandardError=redirect;    //是否重定向错误
            startInfo.RedirectStandardInput = redirect;    //是否重定向输入   是则不能在cmd命令行中输入
            startInfo.RedirectStandardOutput = redirect;      //是否重定向输出,是则不会在cmd命令行中输出
            startInfo.WorkingDirectory=directoryPath;       //指定当前命令所在文件位置，
            process.StartInfo = startInfo;
            process.Start();
            if (redirect)
            {
                process.StandardInput.Close();
                msg = process.StandardOutput.ReadToEnd();  //在重定向输出时才能获取
            }
            //else
            //{
            //    process.WaitForExit();//等待进程退出
            //}
        }
     /// <summary>
        /// 启动exe
        /// </summary>
        /// <param name="filePath">程序路径</param>
        /// <param name="argument">参数</param>
        /// <param name="waitTime">等待时间，毫秒计</param>
        public static void RunExe(string filePath, string argument, int waitTime = 0)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new Exception("filePath is empty");
            }
            if (!File.Exists(filePath))
            {
                throw new Exception(filePath + " is not exist");
            }
            string directory = Path.GetDirectoryName(filePath); 
            try
            {
                Process p = new Process();
                p.StartInfo.FileName = filePath;
                p.StartInfo.WorkingDirectory = directory;
                p.StartInfo.Arguments = argument;
                p.StartInfo.ErrorDialog = false;
                //p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;//与CreateNoWindow联合使用可以隐藏进程运行的窗体
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardError = true;
                p.EnableRaisingEvents = true;                      // 启用Exited事件
                p.Exited += P_Exited;
                p.Start();
                if (waitTime > 0)
                {
                    p.WaitForExit(waitTime);
                }

                if (p.ExitCode == 0)//正常退出
                {
                    //TODO记录日志
                    System.Console.WriteLine("执行完毕！");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("系统错误：", ex);
            }

        }

        private static void P_Exited(object sender, EventArgs e)
        {
            System.Console.WriteLine("系统退出！");
        }

        public static void RunSysExe(string filePath, string argument)
        {
            Process p = new Process(); 
            p.StartInfo.FileName = filePath;        // "iexplore.exe";   //IE
            p.StartInfo.Arguments = argument;// "http://www.baidu.com";
            p.Start();
        }
}
}