using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Lxsh.Project.WebSocketServerTest
{
    public partial class Form1 : Form
    {
        private WebSocketServer Ws_Server;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //事件
            SfWebPlus.OnWndRect = new SfWebPlus.WndRectHandler(DoWndRect);//窗口移动
            SfWebPlus.OnWndStopReset = new EventHandler(DoWndStopReset);//重置（关闭播放） 页面刷新或关闭
            SfWebPlus.OnReloadPlayer = new EventHandler(DoReloadPlayer);//重新加载播放器
            SfWebPlus.OnSnapImageBase64 = new SfWebPlus.SnapImageBase64Handler(SnapImageBase64);//实时或回放截图
            SfWebPlus.OnFp = new SfWebPlus.FpHandler(DoFp);//分屏
            SfWebPlus.OnRealPlay = new SfWebPlus.RealPlayHandler(DoRealPlay);//实时
            SfWebPlus.OnBackPlay = new SfWebPlus.BackPlayHandler(DoBackPlay);//回放
            SfWebPlus.ActiveFormHandle = this.Handle;

            //启动服务端
            Ws_Server = new WebSocketServer(8893, false);
            Ws_Server.Log.Level = LogLevel.Error;
            Ws_Server.AddWebSocketService<SfWebPlusCall>("/LxshProject");
            Ws_Server.Start();
        }

        private void DoReloadPlayer(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DoRealPlay(string assertID, string assertName, string assertOwner, string assertBrand, string videoLink, string customerID, string szCriminaInfo)
        {
            //MemoryStream ms = new MemoryStream();
            //Properties.Resources.ttt.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            //Image img = Image.FromStream(ms);
            //this.pictureBox1.BackgroundImage = img;
        }

        private void DoBackPlay(string assertID, string assertName, string assertOwner, string assertBrand, string videoLink, string customerID, string startTime, string endTime)
        {
            throw new NotImplementedException();
        }

        private void DoFp(int fp)
        {
            throw new NotImplementedException();
        }



        #region 事件
        /// <summary>
        /// 窗体改变位置 如果rect是empty则隐藏
        /// </summary>
        /// <param name="rect"></param>
        public void DoWndRect(Rectangle rect)
        {
            InvokeIfRequired(() =>
            {
                if (rect == Rectangle.Empty)
                {
                    this.Hide();
                    return;
                }
                else
                {
                    if (!this.Visible)
                    {
                        this.Show();
                        this.BringToFront();
                        this.Activate();
                    }
                    this.Height = rect.Height;
                    this.Width = rect.Width;
                    this.Location = new Point(rect.X, rect.Y);
                }
            });
        }

        /// <summary>
        /// 停止正在使用的功能并重置控件为初始状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DoWndStopReset(object sender, EventArgs e)
        {
            InvokeIfRequired(() =>
            {
               // this.pictureBox1.BackgroundImage = null;
                this.Hide();
            });
        }

        private string SnapImageBase64()
        {
            byte[] array = File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + "ttt.jpg");
            return Convert.ToBase64String(array);
        }

        #endregion

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //关闭服务端
            Ws_Server.Stop();
        }

        public void InvokeIfRequired(Action a)
        {
            if (this.IsDisposed || !this.IsHandleCreated)
            {
                return;
            }
            if (this.InvokeRequired)
            {
                this.Invoke(a);
            }
            else
            {
                a();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Form2 f = new 省局视频播放Demo.Form2();
            //SfWebPlus.ActiveFormHandle = f.Handle;
            //f.ShowDialog();
            //SfWebPlus.ActiveFormHandle = this.Handle;
        }

    }

}
