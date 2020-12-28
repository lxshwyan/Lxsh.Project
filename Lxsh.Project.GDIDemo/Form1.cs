using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lxsh.Project.GDIDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                        ControlStyles.ResizeRedraw |
                        ControlStyles.AllPaintingInWmPaint, true);


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
        int Row = 100;
        int colums = 100;
        //用于存储需要直线连接的元素
        List<Tuple<Label, Label>> lines = new List<Tuple<Label, Label>>();
        //  int iPosition = 50;
        // <summary>
        /// 初始化网格
        /// </summary>
        private void InitGridLine()
        {

            Graphics gp = Graphics.FromHwnd(this.panel1.Handle);
            DrawGrid(gp);
            
        }
        //绘制网格
        private void DrawGrid(Graphics gp)
        {
            for (int i = 0; i < Row; i++)
            {
                gp.DrawLine(new Pen(Color.LightCyan), (i + 1) * panel1.Width / Row, 0, (i + 1) * panel1.Width / Row, panel1.Height);
            }
            for (int i = 0; i < colums; i++)
            {
                gp.DrawLine(new Pen(Color.LightCyan), 0, (i + 1) * panel1.Height / colums, panel1.Width, (i + 1) * panel1.Height / colums);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
           

            foreach (Tuple<Label, Label> t in lines)
            {
                Point p1 = new Point(t.Item1.Left + t.Item1.Width / 2,
                                     t.Item1.Top + t.Item1.Height / 2);
                Point p2 = new Point(t.Item2.Left + t.Item2.Width / 2,
                                     t.Item2.Top + t.Item2.Height / 2);

                e.Graphics.DrawLine(Pens.Black, p1, p2);
            }
            InitGridLine();
            base.OnPaint(e);
        }

        /// <summary>
        /// 绘制元素，此处以Label为例
        /// </summary>
        /// <returns></returns>
        private Label createBlock(string lblName, Point pPosition)
        {
            try
            {
                Label label = new Label();
                label.AutoSize = false;
                //TODO：如需动态生成每个标签元素位置，请根据实际情况，初始化标签的Location即可。此处默认X=150，Y 以75间隔递增
                label.Location = new Point(pPosition.X-45, pPosition.Y-18);
             //   iPosition = iPosition + 75;
                label.Size = new Size(89, 36);
                label.BackColor = Color.DarkOliveGreen;
                label.ForeColor = Color.Black;
                label.FlatStyle = FlatStyle.Flat;
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Text = lblName;
                //TODO;可以绑定标签元素的右键事件
                //label.ContextMenuStrip = contextBlock;
                this.panel1.Controls.Add(label);
                //拖拽移动
                MoveBlock(label);
                return label;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }
        /// <summary>
        /// 绘制元素，此处以Label为例
        /// </summary>
        /// <returns></returns>
        private Tuple<Label, Label> createBlock2(string lblName, string lblName1, Point pPosition)
        {
            return new Tuple<Label, Label>(createBlock(lblName, new Point(pPosition.X, pPosition.Y - 100)), createBlock(lblName1, new Point(pPosition.X, pPosition.Y)));
        }
        Point fPoint = new Point();
        //标签移动效果
        private void MoveBlock(Label block, Label endBlock = null)
        {
            block.MouseDown += (ss, ee) =>
            {
                if (ee.Button == System.Windows.Forms.MouseButtons.Left)
                    fPoint = Control.MousePosition;
            };
            block.MouseMove += (ss, ee) =>
            {
                if (ee.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    Point temp = Control.MousePosition;
                    Point res = new Point(fPoint.X - temp.X, fPoint.Y - temp.Y);

                    block.Location = new Point(block.Location.X - res.X,
                                               block.Location.Y - res.Y);
                    fPoint = temp;
                    this.panel1.Invalidate();   // <------- draw the new lines
                }
            };
        }
      
        int i = 0;

        private void panel1_DoubleClick(object sender, EventArgs e)
        {
            lines.Add( createBlock2($"lable—{i++}", $"lable—{i++}", this.panel1.PointToClient(Control.MousePosition)));
            this.panel1.Invalidate();   // <------- draw the new lines
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.pictureBox4.ImageLocation = "http://192.168.137.252:8084/wwwroot/images/login.png";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Console.WriteLine("测试日志");
        }

        private void button4_Click(object sender, EventArgs e)
        {
           // Task.Run(() =>
            //{

            //    while (true)
            //    {
            //        this.pictureBox4.ImageLocation = "";
            //        this.pictureBox4.ImageLocation = "http://192.168.137.252:8084/wwwroot/images/login.png";
            //        Thread.Sleep(500);
            //        this.pictureBox4.ImageLocation = "http://192.168.137.252:8084/wwwroot/images/bigguohui.png";
            //        Thread.Sleep(500);
            //    }

            //});

            ThreadPool.QueueUserWorkItem(new WaitCallback(obj =>
            {

                while (true)
                {
                    this.pictureBox4.ImageLocation = "http://192.168.137.250/w111wwroot/images";
                    this.pictureBox4.ImageLocation = "http://192.168.137.252:8084/wwwroot/images/login.png";
                    Thread.Sleep(500);
                    this.pictureBox4.ImageLocation =  "http://192.168.137.252:8084/wwwroot/images/bigguohui.png";
                    Thread.Sleep(500);
                }
            }));
          
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.pictureBox4.ImageLocation = "http://192.168.137.252:8084/wwwroot/images/bigguohui.png";
           
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            MessageBox.Show("1111");
        }
    }
}
