using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lxsh.Project.GDIDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
        }
        #region 验证码
        private void WriteYZM()
        {
            Random r = new Random();
            string str = "";
            for (int i = 0; i < 5; i++)
            {
                int num = r.Next(0, 10);
                str += num;

            }
            Bitmap bmp = new Bitmap(150, 40);
            Graphics g = Graphics.FromImage(bmp);
            for (int i = 0; i < 5; i++)
            {
                Point p = new Point(i * 20, 0);
                string[] fonts = { "微软雅黑", "宋体", "黑体", "隶书", "仿宋" };
                Color[] colors = { Color.Yellow, Color.Blue, Color.Black, Color.Red, Color.Green };
                g.DrawString(str[i].ToString(), new Font(fonts[r.Next(0, 5)], 20, FontStyle.Bold), new SolidBrush(colors[r.Next(0, 5)]), p);

            }
            for (int i = 0; i < 20; i++)
            {
                Point p1 = new Point(r.Next(0, bmp.Width), r.Next(0, bmp.Height));
                Point p2 = new Point(r.Next(0, bmp.Width), r.Next(0, bmp.Height));
                g.DrawLine(new Pen(Brushes.Green), p1, p2);
            }
            for (int i = 0; i < 500; i++)
            {
                Point p = new Point(r.Next(0, bmp.Width), r.Next(0, bmp.Height));
                bmp.SetPixel(p.X, p.Y, Color.Black);
            }

            pictureBox1.Image = bmp;

        }
        private void WriteTP()
        {
            Random r = new Random();
            string str = "";
            for (int i = 0; i < 5; i++)
            {
                int num = r.Next(0, 10);
                str += num;

            }

            Graphics g = Graphics.FromImage(pictureBox2.Image);
            for (int i = 0; i < 5; i++)
            {
                Point p = new Point(i * 20, 0);
                string[] fonts = { "微软雅黑", "宋体", "黑体", "隶书", "仿宋" };
                Color[] colors = { Color.Yellow, Color.Blue, Color.Black, Color.Red, Color.Green };
                g.DrawString(str[i].ToString(), new Font(fonts[r.Next(0, 5)], 20, FontStyle.Bold), new SolidBrush(colors[r.Next(0, 5)]), p);

            }
            for (int i = 0; i < 20; i++)
            {
                Point p1 = new Point(r.Next(0, pictureBox2.Image.Width), r.Next(0, pictureBox2.Image.Height));
                Point p2 = new Point(r.Next(0, pictureBox2.Image.Width), r.Next(0, pictureBox2.Image.Height));
                g.DrawLine(new Pen(Brushes.Green), p1, p2);
            }

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            WriteYZM();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            WriteTP();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            WriteTP();
        }
        #endregion

        #region 生成图片
        private void gervImage()
        {
           
            Bitmap bitmap = new Bitmap(400, 300);
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.FillRectangle(Brushes.Red, 1, 1, 399, 299);
            //字符串
            g.DrawString("我爱中华", new Font("宋体", 50), new SolidBrush(Color.SkyBlue), new Point(90, 10));
            g.DrawArc(new Pen(Color.SkyBlue), new RectangleF(180, 60, 100, 100), 60, 60);

            //画本地图
            System.Drawing.Image orangeImage =
            System.Drawing.Image.FromFile(System.Environment.CurrentDirectory + "\\test.png");
            g.DrawImageUnscaled(orangeImage, 0, 0);


            pictureBox3.Image = bitmap;
            bitmap.Save(System.Environment.CurrentDirectory+"\\map.jpg", ImageFormat.Gif);

        }     
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            gervImage();
        }
    }
}
