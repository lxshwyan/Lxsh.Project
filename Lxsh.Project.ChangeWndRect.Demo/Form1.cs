using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Lxsh.Project.ChangeWndRect.Demo
{
    public partial class Form1 : Form
    {
        Thread thread;
        FrmShowBase FrmShowBase = new FrmShowBase();
        public Form1()
        { 
          
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {

            ShowSubFrom(FrmShowBase, this.pannelVideo);
        }
        private void button2_Click(object sender, EventArgs e)
        {    
            HideSubFrom(FrmShowBase);
        }
        public void ShowSubFrom(Form frmShow, Control ParentControl)
        {
            frmShow.Show();
            frmShow.TopMost = true;
            thread = new Thread(() =>
            {
                while (true)
                {
                    InvokeIfRequired(() =>
                    {    
                        frmShow.Height = ParentControl.Height;
                        frmShow.Width = ParentControl.Width;
                        frmShow.Location = new Point(this.PointToScreen(ParentControl.Location).X, this.PointToScreen(ParentControl.Location).Y);   
                        frmShow.Visible = ParentControl.Visible;
                    }
                    );
                    Thread.Sleep(100);
                }
            });
            thread.IsBackground = true;
            thread.Start();
        }
        public void HideSubFrom(Form frmShow)
        {
            thread.Abort();
            frmShow.Hide();
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

        private void button3_Click(object sender, EventArgs e)
        {
            this.pannelVideo.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.pannelVideo.Visible = true;
        }
    }

    public enum RetCode
    {
        OK = 0,                   //成功
        Error = 1,                //失败
        DoorNotExist = 13,        //门禁不存在
        UserUnlogin = 100,      //用户未登录
        UserExist = 101,        //注册用户已存在
        UserNotExist = 102,       //登录用户不存在
        UserPwdError = 103,       //登录密码错误
        UserStopped = 104,        //登录用户已停用
        UserExpire = 105,         //登录用户已过期
        UserLocked = 106,         //登录用户已锁定
        UserKeyError = 107,       //登录认证密钥错误
        UserNotRight = 108,     //用户无权限
    }
}
