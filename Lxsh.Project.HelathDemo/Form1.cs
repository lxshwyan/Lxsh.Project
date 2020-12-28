using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lxsh.Project.HelathDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //CreateQR(6, this.txtBox.Text.Trim(), Color.Red, Color.FromArgb(54,98,161));
          //  CreateQR(6, this.txtBox.Text.Trim(), Color.Blue, Color.FromArgb(54, 98, 161));

            CreateQR(6, this.txtBox.Text.Trim(), Color.Green, Color.FromArgb(54, 98, 161));
        }
        private void CreateQR(int pixelsPerModule, string info, Color qrColor, Color qrBackgroundColor)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(info, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(pixelsPerModule, qrColor, qrBackgroundColor, true);
            picBoxQRCode.Image = qrCodeImage;
        }
        
}
}
