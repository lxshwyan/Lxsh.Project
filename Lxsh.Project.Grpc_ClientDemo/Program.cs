using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Lxsh.Project.Grpc_ClientDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            KillBrowserSubprocess();
            var redis = new FreeRedis.RedisClient("127.0.0.1:6379");
            //redis.HDel("lxsh", redis.HKeys("lxsh"));
            //for (int i = 0; i < 10000; i++)
            //{
            //   redis.HSet<string>("lxsh", Guid.NewGuid().ToString(), new Random().Next(1, 100000).ToString());
            //}
            //var listUser1 = redis.HKeys("lxsh");// redis.HGetAll("lxsh");
            for (int i = 0; i < 10000; i++)
            {
                redis.SAdd("user1", 1);
            }

            for (int i = 0; i < 10000; i++)
            {
                redis.SAdd("user2", new Random().Next(1, 100000).ToString());
            }
            var user1 = redis.SMeMembers("user1");
        }


        static void KillBrowserSubprocess()
        {
            try
            {
                string SYSPath = System.AppDomain.CurrentDomain.BaseDirectory;
                Process[] processs = Process.GetProcessesByName("Lxsh.Project.Grpc_ClientDemo");
                if (processs != null && processs.Length > 0)
                {
                    for (int i = 0; i < processs.Length; i++)
                    {
                        Process process = processs[i];

                        bool bKill = false;
                        if (process.MainModule != null)
                        {
                            string FileName = process.MainModule.FileName;
                            if (SYSPath.Contains(FileName) || FileName.Contains(SYSPath))
                            {
                                bKill = true;
                            }
                        }
                        if (bKill)
                        {
                            process.Kill();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString() + "清理CefSharp.BrowserSubprocess异常");
            }

        }
    }
}
