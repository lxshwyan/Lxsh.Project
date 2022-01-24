using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Diagnostics;

namespace ConsoleAppTestSQL
{
    class Program
    {
        static void Main(string[] args)
        {
          
            DisposTest dispos=null;
            dispos?.Call();

            var d = dispos ?? new DisposTest();

        }


        /// <summary>
        /// 获取所有列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAll()
        {
            string str = System.Configuration.ConfigurationManager.AppSettings["strConn"];
            DbHelperSQL db = new DbHelperSQL(str);
           
            string strSql = " SELECT * FROM ABDoorPoliceInfo  ";
            DataSet ds = db.Query(strSql);
            if (ds == null || ds.Tables.Count < 1)
            {
                Console.WriteLine(ds.Tables.Count);
                return null;
            }
            Console.WriteLine(ds.Tables.Count);
            return ds.Tables[0];
        }
        public static void RunCMDCommand(out string outPut, params string[] command)
        {
            using (Process pc = new Process())
            {
                pc.StartInfo.FileName = "cmd.exe";
                pc.StartInfo.CreateNoWindow = true;//隐藏窗口运行
                pc.StartInfo.RedirectStandardError = true;//重定向错误流
                pc.StartInfo.RedirectStandardInput = true;//重定向输入流
                pc.StartInfo.RedirectStandardOutput = true;//重定向输出流
                pc.StartInfo.UseShellExecute = false;
                pc.Start();
                int lenght = command.Length;
                foreach (string com in command)
                {
                    pc.StandardInput.WriteLine(com);//输入CMD命令
                }
                pc.StandardInput.WriteLine("exit");//结束执行，很重要的
                pc.StandardInput.AutoFlush = true;

                outPut = pc.StandardOutput.ReadToEnd();//读取结果        

                pc.WaitForExit();
                pc.Close();
            }
        }

    }
}
