using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lxsh.Project.Demo
{
    static class Program
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string lpszClass, string lpszWindow);
        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        private static extern int FindWindowEx(int hwndParent, int hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", EntryPoint = "GetWindowRect")]
        private static extern int GetWindowRect(int hwnd, ref System.Drawing.Rectangle lpRect);
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(int hwnd, int wMsg, int wParam, int lParam);

        private static readonly int WM_MOUSEMOVE = 512;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += Application_ApplicationExit;
            Application.Run(new Form2());
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 刷新任务栏图标
        /// </summary>
        public static void RefreshTaskbarIcon()
        {
            //任务栏窗口
            int one = FindWindow("Shell_TrayWnd", null);
            //任务栏右边托盘图标+时间区
            int two = FindWindowEx(one, 0, "TrayNotifyWnd", null);
            //不同系统可能有可能没有这层
            int three = FindWindowEx(two, 0, "SysPager", null);
            //托盘图标窗口
            int foor;
            if (three > 0)
            {
                foor = FindWindowEx(three, 0, "ToolbarWindow32", null);
            }
            else
            {
                foor = FindWindowEx(two, 0, "ToolbarWindow32", null);
            }
            if (foor > 0)
            {
                System.Drawing.Rectangle r = new System.Drawing.Rectangle();
                GetWindowRect(foor, ref r);
                //从任务栏左上角从左到右 MOUSEMOVE一遍，所有图标状态会被更新
                for (int x = 0; x < (r.Right - r.Left) - r.X; x++)
                {
                    SendMessage(foor, WM_MOUSEMOVE, 0, (1 << 16) | x);
                }
            }
        }
    }
}
