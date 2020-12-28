using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lxsh.Project.GDIDemo
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ConsoleLogTextWriter logSW = new ConsoleLogTextWriter();
            Console.SetOut(logSW);
            Console.WriteLine("程序启动");
            Application.Run(new Form1());
        }
    }
    /// <summary>
    /// 捕获控制台输出并写入日志文件（推荐通过日志接口写日志）
    /// </summary>
    public class ConsoleLogTextWriter : TextWriter
    {
        public ConsoleLogTextWriter() : base() { }

        public override Encoding Encoding { get { return Encoding.UTF8; } }

        public override void Write(string value)
        {
            Log.WriteLog(value);
        }
        public override void WriteLine(string value)
        {
            Log.WriteLog(value);
        }
        public override void Close()
        {
            base.Close();
        }
    }
    /// <summary>
    /// 日志类（只作演示使用，可自己定义实现）
    /// </summary>
  public  class Log
    {
        public static void WriteLog(string msg)
        {
            string path = "测试用日志文件.log";
            try
            {
                FileStream fs;
                StreamWriter sw;
                StringBuilder sbr = new StringBuilder(16);
                if (!System.IO.File.Exists(path))
                {
                    fs = new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite);
                    sw = new StreamWriter(fs, Encoding.UTF8);
                    sbr.Append("日志开始-");
                    sbr.Append(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                    sbr.AppendLine();
                }
                else
                {
                    fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                    sw = new StreamWriter(fs, Encoding.UTF8);
                }
                sbr.Append("--");
                sbr.Append(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
                sbr.Append("--");
                sbr.Append(msg);
                sw.WriteLine(sbr.ToString());

                sw.Flush();
                sw.Close();
                fs.Close();
                sbr.Clear();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
