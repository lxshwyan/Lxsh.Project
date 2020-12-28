using CefSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lxsh.Project.CefSharpDemo
{
    public partial class Form1 : Form
    {
        CefSharp.WinForms.ChromiumWebBrowser webCom = null;
        public Form1()
        {
            InitializeComponent();
        }
        private void InitBrowser()
        {
            //webCom = new CefSharp.WinForms.ChromiumWebBrowser("http://192.168.137.134:8886");
            webCom = new CefSharp.WinForms.ChromiumWebBrowser("http://192.168.137.252:8084/Monitor/Index?dataAssertGroupGuid=47a3f5c6-dde5-482d-a270-3c0b37ff694d");
            webCom.Dock = DockStyle.Fill;
            webCom.FrameLoadEnd += WebCom_FrameLoadEnd;
            webCom.FrameLoadStart += WebCom_FrameLoadStart;
            this.Controls.Add(webCom);
           
         //   webCom.Load("http://192.168.137.134:8886");

        }

        private void WebCom_FrameLoadStart(object sender, FrameLoadStartEventArgs e)
        {
          //  MessageBox.Show("开始加载网页");
        }

        private void WebCom_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            button1_Click(null, null);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitBrowser();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          //  webCom.ExecuteScriptAsync("xxx()");  //调用js方法
          // webCom.GetBrowser().MainFrame.ExecuteJavaScriptAsync("alert('这是c#调用的js,给文本框赋值！')");  //调用js方法

           webCom.GetBrowser().MainFrame.ExecuteJavaScriptAsync("document.getElementById('EmployeeID').value='admin'");  //调用控件赋值
           webCom.GetBrowser().MainFrame.ExecuteJavaScriptAsync("document.getElementById('Password').value='d2nzjy'");  //调用控件赋值
           webCom.GetBrowser().MainFrame.ExecuteJavaScriptAsync("document.getElementById('btnLogin').click()");    //调用控件赋值

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var pluginsPath = Path.Combine(Environment.CurrentDirectory, "Plugins1");
            var path1 = Environment.GetEnvironmentVariable("PATH");
            if (!Environment.GetEnvironmentVariable("PATH").Contains(pluginsPath))
            {
                var path = Environment.GetEnvironmentVariable("PATH") + ";" + pluginsPath;
                Environment.SetEnvironmentVariable("path", path, EnvironmentVariableTarget.Process);
            }
            else
            {
                MessageBox.Show("环境变量里面已经存在该路径");
            }
           
        }
    }
}
