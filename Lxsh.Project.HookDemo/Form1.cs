using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lxsh.Project.HookDemo
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        KeyboardHook kh;

        private void Form1_Load(object sender, EventArgs e)

        {

            kh = new KeyboardHook();

            kh.SetHook();

            kh.OnKeyDownEvent += kh_OnKeyDownEvent;

        }

        void kh_OnKeyDownEvent(object sender, KeyEventArgs e)

        {

            if (e.KeyData == (Keys.S | Keys.Control)) { this.Show(); }//Ctrl+S显示窗口

            if (e.KeyData == (Keys.H | Keys.Control)) { this.Hide(); }//Ctrl+H隐藏窗口

            if (e.KeyData == (Keys.C | Keys.Control)) { this.Close(); }//Ctrl+C 关闭窗口 

            if (e.KeyData == (Keys.A | Keys.Control | Keys.Alt)) { this.Text = "你发现了什么？"; }//Ctrl+Alt+A
            if (e.KeyData ==Keys.Space) { this.Text = "空格键"; }
            if (e.KeyData == Keys.Enter) { this.Text = "Enter"; }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)

        {

            kh.UnHook();

        }
    }
}
