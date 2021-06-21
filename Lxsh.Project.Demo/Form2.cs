using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
//https://www.cnblogs.com/wangshenhe/archive/2012/05/08/2490193.html  
//C# 控件实现内容拖动(DragDrop)功能(SamWang)
namespace Lxsh.Project.Demo
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            if (path.Split('.')[path.Split('.').Length - 1].ToUpper() != "EXE")
            {
                MessageBox.Show(path + "不是可执行程序");
                return;
            }
           
        }
        private void Form2_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Link;//拖动时的图标
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            SetCtrlDrag.SetCtrlDragEvent(this.textBox1);
        }

        private async void btnAsyns_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1");
            this.textBox1.Text = await GetJsonAsync();
            MessageBox.Show("4");

        }
        private async Task<string> GetJsonAsync()
        {
            using (var client=new HttpClient())
            {
                MessageBox.Show("2");
                string result = await client.GetStringAsync("http://192.168.137.252:6178/signalr/hubs");
                MessageBox.Show("3");
                return result;
            }
        }
    }
    public class SetCtrlDrag
  {
      public static void SetCtrlDragEvent(Control ctrl)
      {
          if(ctrl is TextBox)
          {
              TextBox tb = ctrl as TextBox;
              tb.AllowDrop = true;
              tb.DragEnter += (sender, e) =>
              {
                  e.Effect = DragDropEffects.Link;//拖动时的图标
              };
              tb.DragDrop += (sender, e) =>
              {
              ((TextBox)sender).Text = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                 };
          }
      }
  }
}
