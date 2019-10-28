using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lxsh.Project.SpeechSynthesizer.Demo
{
    public partial class FrmVideo : Form
    {
        public FrmVideo()
        {
            InitializeComponent();
        }
     
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
    }
}
