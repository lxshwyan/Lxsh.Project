
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lxsh.Project.ReadBadCode.Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           BarCode.BarCodeEvent += new BarCodeHook.BarCodeDelegate(BarCode_BarCodeEvent);
        }
        BarCodeHook BarCode = new BarCodeHook();

        private delegate void ShowInfoDelegate(BarCodeHook.BarCodes barCode);
        private void ShowInfo(BarCodeHook.BarCodes barCode)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ShowInfoDelegate(ShowInfo), new object[] { barCode });
            }
            else
            {
                textBox1.Text = barCode.KeyName;
                textBox2.Text = barCode.VirtKey.ToString();
                textBox3.Text = barCode.ScanCode.ToString();
                textBox4.Text = barCode.AscII.ToString();
                textBox5.Text = barCode.Chr.ToString();
                textBox6.Text = barCode.IsValid ? barCode.BarCode : "";
            }
        }

        void BarCode_BarCodeEvent(BarCodeHook.BarCodes barCode)
        {

            ShowInfo(barCode);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BarCode.Start();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            BarCode.Stop();
        }


        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text.Length > 0)
            {
                MessageBox.Show(textBox6.Text);
            }
        }

    }
}
