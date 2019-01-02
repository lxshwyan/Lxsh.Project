using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Lxsh.Project.Thread.Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();     
        }
        System.Threading.Thread thread;
        private void Form1_Load(object sender, EventArgs e)
        {
            int index = 0;
           thread = new System.Threading.Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        System.Threading.Thread.Sleep(500);
                        if (this.textBox1.InvokeRequired)
                        {
                            this.textBox1.Invoke(new Action<string>(str => { this.textBox1.AppendText(str + ","); }), index++.ToString());
                        }
                        else
                        {
                            this.textBox1.AppendText((index++ + ",").ToString());
                        }
                    }
                    catch (Exception)
                    {
                      
                    }
                   
                }
            });
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            thread.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            thread.Suspend();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            thread.Resume();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            thread.Interrupt();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            thread.Abort();
        }
    }
}
