using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.Common
{
    public partial class KillProcessHelper
    {
        /// <summary>
        /// 刷新托盘区域图标
        /// </summary>
        public static void RefreshTrayIcon()
        {
            IntPtr vHandle = TrayToolbarWindow32();
            IntPtr vProcessId = IntPtr.Zero;
            Win32API.GetWindowThreadProcessId(vHandle, ref vProcessId);
            IntPtr vProcess = Win32API.OpenProcess(Win32API.PROCESS_VM_OPERATION | Win32API.PROCESS_VM_READ | Win32API.PROCESS_VM_WRITE, IntPtr.Zero, vProcessId);
            IntPtr vPointer = Win32API.VirtualAllocEx(vProcess, (int)IntPtr.Zero, 0x1000, Win32API.MEM_RESERVE | Win32API.MEM_COMMIT, Win32API.PAGE_READWRITE);
            try
            {
                Rect r;
                Win32API.GetWindowRect(vHandle, out r);
                int width = r.Right - r.Left;
                int height = r.Bottom - r.Top;
                //从任务栏中间从左到右 MOUSEMOVE一遍，所有图标状态会被更新 
                for (int x = 1; x < width; x++)
                {
                    Win32API.SendMessage(vHandle, Win32API.WM_MOUSEMOVE, 0, MakeLParam(x, height / 2));
                }
            }
            finally
            {
                Win32API.VirtualFreeEx(vProcess, vPointer, 0, Win32API.MEM_RELEASE);
                Win32API.CloseHandle(vProcess);
            }
        }

        /// <summary>
        /// 刷新通知栏区域图标
        /// </summary>
        public static void RefreshNotifyIcon()
        {
            IntPtr vHandle = NotifyIconOverflowWindow();
            IntPtr vProcessId = IntPtr.Zero;
            Win32API.GetWindowThreadProcessId(vHandle, ref vProcessId);
            IntPtr vProcess = Win32API.OpenProcess(Win32API.PROCESS_VM_OPERATION | Win32API.PROCESS_VM_READ | Win32API.PROCESS_VM_WRITE, IntPtr.Zero, vProcessId);
            IntPtr vPointer = Win32API.VirtualAllocEx(vProcess, (int)IntPtr.Zero, 0x1000, Win32API.MEM_RESERVE | Win32API.MEM_COMMIT, Win32API.PAGE_READWRITE);
            try
            {
                Rect r;
                Win32API.GetWindowRect(vHandle, out r);
                int width = r.Right - r.Left;
                int height = r.Bottom - r.Top;
                //从任务栏中间从左到右 MOUSEMOVE一遍，所有图标状态会被更新 
                for (int x = 1; x < width; x++)
                {
                    for (int y = 0; y < height; y += 4)
                    {
                        Win32API.SendMessage(vHandle, Win32API.WM_MOUSEMOVE, 0, MakeLParam(x, y));
                    }
                }
            }
            finally
            {
                Win32API.VirtualFreeEx(vProcess, vPointer, 0, Win32API.MEM_RELEASE);
                Win32API.CloseHandle(vProcess);
            }
        }

        //获取托盘指针
        private static IntPtr TrayToolbarWindow32()
        {
            IntPtr h = IntPtr.Zero;
            IntPtr hTemp = IntPtr.Zero;

            h = Win32API.FindWindow("Shell_TrayWnd", null); //托盘容器
            h = Win32API.FindWindowEx(h, IntPtr.Zero, "TrayNotifyWnd", null);//找到托盘
            h = Win32API.FindWindowEx(h, IntPtr.Zero, "SysPager", null);

            hTemp = Win32API.FindWindowEx(h, IntPtr.Zero, "ToolbarWindow32", null);

            return hTemp;
        }

        private static IntPtr NotifyIconOverflowWindow()
        {
            IntPtr h = IntPtr.Zero;
            IntPtr hTemp = IntPtr.Zero;

            h = Win32API.FindWindow("NotifyIconOverflowWindow", null); //托盘容器
            hTemp = Win32API.FindWindowEx(h, IntPtr.Zero, "ToolbarWindow32", null);

            return hTemp;
        }

        private static IntPtr MakeLParam(int LoWord, int HiWord)
        {
            return (IntPtr)((HiWord << 16) | (LoWord & 0xffff));
        }
    }
    /// <summary>
    /// 操作Windows窗体,系统托盘所用的API函数
    /// </summary>
    public class Win32API
    {
        public const int WM_USER = 0x400;
        public const int WM_CLOSE = 0x10;
        public const int WM_GETTEXT = 0x000D;
        public const int WM_SETTEXT = 0x000C;
        public const int WM_MOUSEMOVE = 0x0200;

        public const int STANDARD_RIGHTS_REQUIRED = 0xF0000;
        public const int SYNCHRONIZE = 0x100000;
        public const int PROCESS_ALL_ACCESS = STANDARD_RIGHTS_REQUIRED | SYNCHRONIZE | 0xFFF;
        public const int PROCESS_TERMINATE = 0x1;

        public const int PROCESS_VM_OPERATION = 0x8;
        public const int PROCESS_VM_READ = 0x10;
        public const int PROCESS_VM_WRITE = 0x20;

        public const int MEM_RESERVE = 0x2000;
        public const int MEM_COMMIT = 0x1000;
        public const int MEM_RELEASE = 0x8000;

        public const int PAGE_READWRITE = 0x4;

        public const int TB_BUTTONCOUNT = (WM_USER + 24);
        public const int TB_HIDEBUTTON = (WM_USER + 4);
        public const int TB_GETBUTTON = (WM_USER + 23);
        public const int TB_GETBUTTONTEXT = WM_USER + 75;
        public const int TB_GETBITMAP = (WM_USER + 44);
        public const int TB_DELETEBUTTON = (WM_USER + 22);
        public const int TB_ADDBUTTONS = (WM_USER + 20);
        public const int TB_INSERTBUTTON = (WM_USER + 21);
        public const int TB_ISBUTTONHIDDEN = (WM_USER + 12);
        public const int ILD_NORMAL = 0x0;
        public const int TPM_NONOTIFY = 0x80;

        public const int WS_VISIBLE = 268435456;//窗体可见
        public const int WS_MINIMIZEBOX = 131072;//有最小化按钮
        public const int WS_MAXIMIZEBOX = 65536;//有最大化按钮
        public const int WS_BORDER = 8388608;//窗体有边框
        public const int GWL_STYLE = (-16);//窗体样式
        public const int GW_HWNDFIRST = 0;
        public const int GW_HWNDNEXT = 2;
        public const int SW_HIDE = 0;
        public const int SW_SHOW = 5;

        [DllImport("User32.Dll")]
        public static extern void GetClassName(IntPtr hwnd, StringBuilder s, int nMaxCount);

        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow", SetLastError = true)]
        public static extern void SetForegroundWindow(IntPtr hwnd);

        [DllImport("user32.dll", EntryPoint = "GetDlgItem", SetLastError = true)]
        public static extern IntPtr GetDlgItem(int nID, IntPtr phWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int RegisterWindowMessage(string msg);

        [DllImport("kernel32", EntryPoint = "OpenProcess")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, IntPtr bInheritHandle, IntPtr dwProcessId);

        [DllImport("kernel32", EntryPoint = "CloseHandle")]
        public static extern int CloseHandle(IntPtr hObject);

        [DllImport("user32", EntryPoint = "GetWindowThreadProcessId")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hwnd, ref IntPtr lpdwProcessId);

        [DllImport("user32.dll")]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        [DllImport("user32", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, IntPtr lParam);

        [DllImport("kernel32", EntryPoint = "ReadProcessMemory")]
        public static extern int ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, ref IntPtr lpBuffer, int nSize, int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", EntryPoint = "ReadProcessMemory")]
        public static extern bool ReadProcessMemoryEx(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, ref uint vNumberOfBytesRead);

        [DllImport("kernel32", EntryPoint = "ReadProcessMemory")]
        public static extern int ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, int lpNumberOfBytesWritten);

        [DllImport("kernel32", EntryPoint = "WriteProcessMemory")]
        public static extern int WriteProcessMemory(IntPtr hProcess, ref int lpBaseAddress, ref int lpBuffer, int nSize, ref int lpNumberOfBytesWritten);

        [DllImport("kernel32", EntryPoint = "VirtualAllocEx")]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, int lpAddress, int dwSize, int flAllocationType, int flProtect);

        [DllImport("kernel32", EntryPoint = "VirtualFreeEx")]
        public static extern int VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, int dwFreeType);

        [DllImport("User32.dll")]
        public extern static int GetWindow(int hWnd, int wCmd);

        [DllImport("User32.dll")]
        public extern static int GetWindowLongA(int hWnd, int wIndx);

        [DllImport("user32.dll")]
        public static extern bool GetWindowText(int hWnd, StringBuilder title, int maxBufSize);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public extern static int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int GetWindowRect(IntPtr hwnd, out Rect lpRect);

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "FindWindowEx", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", EntryPoint = "ShowWindow", SetLastError = true)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);
    }

    public struct Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }
}
