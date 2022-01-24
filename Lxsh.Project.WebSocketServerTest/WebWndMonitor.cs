using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Lxsh.Project.WebSocketServerTest
{
    /// <summary>
    /// 此处封装所有功能具体的实现
    /// </summary>
    public class SfWebPlus
    {
        public delegate void WndRectHandler(Rectangle rect);
        public delegate void FpHandler(int fp);
        public delegate void RealPlayHandler(string assertID, string assertName, string assertOwner, string assertBrand, string videoLink, string customerID, string szCriminaInfo);
        public delegate void BackPlayHandler(string assertID, string assertName, string assertOwner, string assertBrand, string videoLink, string customerID, string startTime, string endTime);
        public delegate string SnapImageBase64Handler();      
        private static Rectangle LastRect;
        private static DateTime LastRectTime;
        private readonly static object LOCKOBJ = new object();

        /// <summary>
        /// 窗体区域变化
        /// </summary>
        public static WndRectHandler OnWndRect;

        /// <summary>
        /// 控件重置（浏览器刷新、关闭后）
        /// </summary>
        public static EventHandler OnWndStopReset;

        /// <summary>
        /// 重启player
        /// </summary>
        public static EventHandler OnReloadPlayer;

        /// <summary>
        /// 截图事件
        /// </summary>
        public static SnapImageBase64Handler OnSnapImageBase64;

        /// <summary>
        /// 实时预览
        /// </summary>
        public static RealPlayHandler OnRealPlay;

        /// <summary>
        /// 回放
        /// </summary>
        public static BackPlayHandler OnBackPlay;

        /// <summary>
        /// 分屏
        /// </summary>
        public static FpHandler OnFp;      
        /// <summary>
        /// 控件中当前激活的窗体是哪个窗体
        /// </summary>
        public static IntPtr ActiveFormHandle = IntPtr.Zero;

        /// <summary>
        /// 通知客户端变换位置
        /// </summary>
        /// <param name="wndWidth">总页面宽</param>
        /// <param name="wndHeight">总页面高</param>
        /// <param name="cliWidth">控件宽</param>
        /// <param name="cliHeight">控件高</param>
        /// <param name="cliX">控件top</param>
        /// <param name="cliY">控件left</param>
        public static void SendHostWndRect(int wndWidth, int wndHeight, int cliWidth, int cliHeight, int cliX, int cliY)
        {
            Rectangle rect = WebWndRectFlow.Instance.TryGetMainWindowRect(SfWebPlusCall.LastConnectedPort, ActiveFormHandle);

            if (rect.Height > Screen.PrimaryScreen.WorkingArea.Height)
            {
                int marginHeight = rect.Height - Screen.PrimaryScreen.WorkingArea.Height;
                rect.Height = Screen.PrimaryScreen.WorkingArea.Height;
            }
            if (rect.Width > Screen.PrimaryScreen.WorkingArea.Width)
            {
                rect.Width = Screen.PrimaryScreen.WorkingArea.Width;
            }

            int offHeight = rect.Height - wndHeight;
            int offWidth = rect.Width - wndWidth;

            if (rect.Size == Screen.PrimaryScreen.WorkingArea.Size)
            {
                rect.X = 0;
                rect.Y = 0;
            }
            else
            {
                offWidth += -8;
                offHeight += -8;
            }

            //监区地址栏收藏夹等
            rect.Y += offHeight;
            rect.X += offWidth;

            //加页面可视区坐标
            rect.Y += cliY;
            rect.X += cliX;

            rect.Height = cliHeight;
            rect.Width = cliWidth;

            if (rect.Width < 1 && rect.Height < 1)
            {
                rect = Rectangle.Empty;
            }

            lock (LOCKOBJ)
            {
                LastRect = rect;
                LastRectTime = DateTime.Now;
            }
            OnWndRect?.Invoke(rect);
        }

        /// <summary>
        /// 获取截图Base64图片，如果当前在实时视频，获取实时视频截图，如果在回放视频则获取回放截图
        /// </summary>
        public static string GetSnapImageBase64()
        {
            return OnSnapImageBase64?.Invoke();
        }

        /// <summary>
        /// 重启player
        /// </summary>
        public static void ReloadPlayer()
        {
            OnReloadPlayer?.Invoke(null, null);
        }

        /// <summary>
        /// 分屏
        /// </summary>
        /// <param name="fp"></param>
        public static void Fp(int fp)
        {
            OnFp?.Invoke(fp);
        }

        //实时
        public static void RealPlay(string assertID, string assertName, string assertOwner, string assertBrand, string videoLink, string customerID, string szCriminaInfo)
        {
            OnRealPlay?.Invoke(assertID, assertName, assertOwner, assertBrand, videoLink, customerID, szCriminaInfo);
        }

        //回放
        public static void BackPlay(string assertID, string assertName, string assertOwner, string assertBrand, string videoLink, string customerID, string startTime, string endTime)
        {
            OnBackPlay?.Invoke(assertID, assertName, assertOwner, assertBrand, videoLink, customerID, startTime, endTime);
        }

    }

    //服务端消息接收
    public class SfWebPlusCall : WebSocketBehavior
    {
        public static int LastConnectedPort = 0;

        protected override void OnOpen()
        {
            ////只响应一个连接
            //if (Sessions.Count > 1)
            //{
            //    Context.WebSocket.Close();
            //    return;
            //}
            //LastConnectedPort = Context.UserEndPoint.Port;
            Context.WebSocket.Send("FDSFDSFDSFD");
            base.OnOpen();
        }

        protected override void OnClose(CloseEventArgs e)
        {
            if (Sessions.Count > 0)
            {
                return;
            }
            SfWebPlus.OnWndStopReset?.Invoke(null, null);
            LastConnectedPort = 0;
            base.OnClose(e);
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            Send(e.Data);
            SfWebPlusCallModel result = null;
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            try
            {
                result = jss.Deserialize<SfWebPlusCallModel>(e.Data);
                result.Success = true;

                MethodInfo method = typeof(SfWebPlus).GetMethod(result.Name, BindingFlags.Static | BindingFlags.Public);//公用静态方法
                ParameterInfo[] paras = method.GetParameters();

                Dictionary<string, object> jsonArray = null;
                if (result.JsonString != null && paras != null && paras.Length > 0)
                {
                    jsonArray = jss.Deserialize<Dictionary<string, object>>(result.JsonString);
                }

                object[] objArray = null;
                if (paras == null)
                {
                    objArray = null;
                }
                else
                {
                    objArray = new object[paras.Length];
                    for (int ii = 0; ii < paras.Length; ii++)
                    {
                        objArray[ii] = jsonArray[paras[ii].Name];
                    }
                }
                object resultObj = method.Invoke(null, objArray);
                if (resultObj == null)
                {
                    result.JsonString = null;
                }
                else
                {
                    if (method.ReturnType.IsPrimitive || method.ReturnType == typeof(string))
                    {
                        result.JsonString = resultObj?.ToString();
                    }
                    else
                    {
                        result.JsonString = jss.Serialize(resultObj);
                    }
                }
            }
            catch (Exception ex)
            {
                result.JsonString = jss.Serialize(ex.Message);
                result.Success = false;
            }
            //发送返回值
            Send(jss.Serialize(result));
        }

        public class SfWebPlusCallModel
        {
            public string Name { get; set; }

            public bool Success { get; set; }

            public string JsonString { get; set; }
        }
    }


    /// <summary>
    /// 监视进程的主窗口
    /// </summary>
    public class WebWndRectFlow
    {
        public delegate bool EnumThreadWindowsCallback(IntPtr hWnd, IntPtr lParam);
        public readonly static WebWndRectFlow Instance = new WebWndRectFlow();

        private int TcpPort = 0;
        private int ProcessId = 0;
        private IntPtr MainWindowHandle = IntPtr.Zero;
        private const int INET = 2; //代表IPV4

        private WebWndRectFlow() { }


        public System.Drawing.Rectangle TryGetMainWindowRect(int port, IntPtr controlHandle)
        {
            if (port != TcpPort)
            {
                MainWindowHandle = IntPtr.Zero;
                ProcessId = 0;
                TcpPort = port;
                TrySetProcessId(port);
                TrySetMainWindowHandle(this.ProcessId);
            }

            if (ProcessId > 0 && MainWindowHandle != IntPtr.Zero)
            {
                IntPtr forge = GetForegroundWindow();
                if (MainWindowHandle != forge && controlHandle != forge)
                {
                    return Rectangle.Empty;
                }

                RECT rect = new RECT();
                if (GetWindowRect(MainWindowHandle, ref rect))
                {
                    Rectangle windowRect = new Rectangle();
                    windowRect.Height = (rect.Bottom - rect.Top);
                    windowRect.Width = (rect.Right - rect.Left);
                    windowRect.X = rect.Left;
                    windowRect.Y = rect.Top;
                    return windowRect;
                }
                else
                {
                    MainWindowHandle = IntPtr.Zero;
                    ProcessId = 0;
                    return Rectangle.Empty;
                }
            }
            else
            {
                return Rectangle.Empty;
            }
        }

        //根据端口找PID
        private void TrySetProcessId(int port)
        {
            int bufferSize = 0;

            uint result = GetExtendedTcpTable(IntPtr.Zero, ref bufferSize, true, INET, TcpTableClass.TCP_TABLE_OWNER_PID_ALL);
            IntPtr tcpTableRecordsPtr = Marshal.AllocHGlobal(bufferSize);

            try
            {
                result = GetExtendedTcpTable(tcpTableRecordsPtr, ref bufferSize, true, INET, TcpTableClass.TCP_TABLE_OWNER_PID_ALL);

                MIB_TCPTABLE_OWNER_PID tcpRecordsTable = (MIB_TCPTABLE_OWNER_PID)Marshal.PtrToStructure(tcpTableRecordsPtr, typeof(MIB_TCPTABLE_OWNER_PID));

                IntPtr tableRowPtr = (IntPtr)((long)tcpTableRecordsPtr + Marshal.SizeOf(tcpRecordsTable.dwNumEntries));

                for (int row = 0; row < tcpRecordsTable.dwNumEntries; row++)
                {
                    MIB_TCPROW_OWNER_PID tcpRow = (MIB_TCPROW_OWNER_PID)Marshal.PtrToStructure(tableRowPtr, typeof(MIB_TCPROW_OWNER_PID));
                    int localPort = BitConverter.ToUInt16(new byte[2] { tcpRow.localPort[1], tcpRow.localPort[0] }, 0);
                    int remotePort = BitConverter.ToUInt16(new byte[2] { tcpRow.remotePort[1], tcpRow.remotePort[0] }, 0);
                    int pid = tcpRow.owningPid;

                    if (localPort == port)
                    {
                        Process p = Process.GetProcessById(pid);
                        //IE比较烦，要获取最先启动的那个主进程 这里不知道怎么找，暂时通过启动时间找，是否靠谱待验证
                        if (p.ProcessName == "iexplore")
                        {
                            DateTime time = DateTime.MaxValue;
                            Process[] procs = Process.GetProcessesByName("iexplore");
                            for (int ii = 0; ii < procs.Length; ii++)
                            {
                                if (time == DateTime.MaxValue)
                                {
                                    pid = procs[ii].Id;
                                    time = procs[ii].StartTime;
                                    continue;
                                }
                                else if (procs[ii].StartTime < time)
                                {
                                    pid = procs[ii].Id;
                                    time = procs[ii].StartTime;
                                    continue;
                                }
                            }
                        }
                        this.ProcessId = pid;
                        break;
                    }

                    tableRowPtr = (IntPtr)((long)tableRowPtr + Marshal.SizeOf(tcpRow));

                    //IPAddress localIp, IPAddress remoteIp, ushort localPort,ushort remotePort, int pId, MibTcpState state
                    //tcpTableRecords.Add(new TcpProcessRecord(
                    //                      new IPAddress(tcpRow.localAddr),
                    //                      new IPAddress(tcpRow.remoteAddr),
                    //                      BitConverter.ToUInt16(new byte[2] {
                    //                          tcpRow.localPort[1],
                    //                          tcpRow.localPort[0] }, 0),
                    //                      BitConverter.ToUInt16(new byte[2] {
                    //                          tcpRow.remotePort[1],
                    //                          tcpRow.remotePort[0] }, 0),
                    //                      tcpRow.owningPid, tcpRow.state));
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Marshal.FreeHGlobal(tcpTableRecordsPtr);
            }
        }

        //PID找主窗口
        private void TrySetMainWindowHandle(int processId)
        {
            MainWindowHandle = IntPtr.Zero;
            ProcessId = processId;
            EnumThreadWindowsCallback callback = new EnumThreadWindowsCallback(EnumWindowsCallback);
            EnumWindows(callback, IntPtr.Zero);
            GC.KeepAlive(callback);
        }

        private bool EnumWindowsCallback(IntPtr handle, IntPtr extraParameter)
        {
            int pid;
            GetWindowThreadProcessId(new HandleRef(this, handle), out pid);
            if (pid == this.ProcessId)
            {
                if (this.IsMainWindow(handle))
                {
                    MainWindowHandle = handle;
                    return false;
                }
            }
            return true;
        }

        private bool IsMainWindow(IntPtr handle)
        {
            return (!(GetWindow(new HandleRef(this, handle), 4) != IntPtr.Zero) && IsWindowVisible(new HandleRef(this, handle)));
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool EnumWindows(EnumThreadWindowsCallback callback, IntPtr extraData);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowThreadProcessId(HandleRef handle, out int processId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetWindow(HandleRef hWnd, int uCmd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool IsWindowVisible(HandleRef hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left; //最左坐标
            public int Top; //最上坐标
            public int Right; //最右坐标
            public int Bottom; //最下坐标
        }

        [DllImport("iphlpapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern uint GetExtendedTcpTable(IntPtr pTcpTable, ref int pdwSize, bool bOrder, int ulAf, TcpTableClass tableClass, uint reserved = 0);

        [StructLayout(LayoutKind.Sequential)]
        public struct MIB_TCPTABLE_OWNER_PID
        {
            public uint dwNumEntries;
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct,
                SizeConst = 1)]
            public MIB_TCPROW_OWNER_PID[] table;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MIB_TCPROW_OWNER_PID
        {
            public MibTcpState state;
            public uint localAddr;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] localPort;
            public uint remoteAddr;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] remotePort;
            public int owningPid;
        }

        public enum TcpTableClass
        {
            TCP_TABLE_BASIC_LISTENER,
            TCP_TABLE_BASIC_CONNECTIONS,
            TCP_TABLE_BASIC_ALL,
            TCP_TABLE_OWNER_PID_LISTENER,
            TCP_TABLE_OWNER_PID_CONNECTIONS,
            TCP_TABLE_OWNER_PID_ALL,
            TCP_TABLE_OWNER_MODULE_LISTENER,
            TCP_TABLE_OWNER_MODULE_CONNECTIONS,
            TCP_TABLE_OWNER_MODULE_ALL
        }

        public enum MibTcpState
        {
            CLOSED = 1,
            LISTENING = 2,
            SYN_SENT = 3,
            SYN_RCVD = 4,
            ESTABLISHED = 5,
            FIN_WAIT1 = 6,
            FIN_WAIT2 = 7,
            CLOSE_WAIT = 8,
            CLOSING = 9,
            LAST_ACK = 10,
            TIME_WAIT = 11,
            DELETE_TCB = 12,
            NONE = 0
        }
    }
}
