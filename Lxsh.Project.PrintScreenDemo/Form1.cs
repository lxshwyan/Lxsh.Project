using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lxsh.Project.PrintScreenDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Screen screen = new Screen();
        private void btnMap_Click(object sender, EventArgs e)
        {
            screen.ScreenShot("http://192.168.137.254:8893/", AppDomain.CurrentDomain.BaseDirectory + @"\map\1.jpeg", 1920, 1080,200,200,1920,1);
        }
    }
}
