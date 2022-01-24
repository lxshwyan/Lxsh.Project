using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
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
        /// <summary>
        /// 透明控件
        /// </summary>
        /// <param name="control"></param>
        public static void AddAssociateCoverPanel(Control control)
        {
            Control parent = control.Parent;
            control.Size = parent.ClientSize;
            control.Location = new Point(0, 0);
            Bitmap controlImage = new Bitmap(parent.Width, parent.Height);
            parent.DrawToBitmap(controlImage, new Rectangle(Point.Empty, parent.ClientSize));
            SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(128, 22, 25, 51));
            Graphics g = Graphics.FromImage(controlImage);
            g.FillRectangle(semiTransBrush, new Rectangle(Point.Empty, controlImage.Size));
            control.BackgroundImage = controlImage;
            parent.Controls.SetChildIndex(control, 0);
            control.BringToFront();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect">窗体大小</param>
        /// <param name="radius">圆角大小</param>
        /// <returns></returns>
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = 20;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();

            path.AddArc(arcRect, 180, 90);//左上角

            arcRect.X = rect.Right - diameter;//右上角
            path.AddArc(arcRect, 270, 90);

            arcRect.Y = rect.Bottom - diameter;// 右下角
            path.AddArc(arcRect, 0, 90);

            arcRect.X = rect.Left;// 左下角
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();
            return path;
        }
        public void SetWindowRegion()
        {
            System.Drawing.Drawing2D.GraphicsPath FormPath;
            FormPath = new System.Drawing.Drawing2D.GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            FormPath = GetRoundedRectPath(rect, 100);
            this.Region = new Region(FormPath);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //if (this.WindowState == FormWindowState.Normal)
            //{
            //    SetWindowRegion();
            //}
            //else
            //{
            //    this.Region = null;
            //}
        }
        private void Draw(Rectangle rectangle, Graphics g, int _radius, bool cusp, Color begin_color, Color end_color)
        {
            int span = 2;
            //抗锯齿
            g.SmoothingMode = SmoothingMode.AntiAlias;
            //渐变填充
            LinearGradientBrush myLinearGradientBrush = new LinearGradientBrush(rectangle, begin_color, end_color, LinearGradientMode.Vertical);
            //画尖角
            if (cusp)
            {
                span = 10;
                PointF p1 = new PointF(rectangle.Width - 12, rectangle.Y + 10);
                PointF p2 = new PointF(rectangle.Width - 12, rectangle.Y + 30);
                PointF p3 = new PointF(rectangle.Width, rectangle.Y + 20);
                PointF[] ptsArray = { p1, p2, p3 };
                g.FillPolygon(myLinearGradientBrush, ptsArray);
            }
            //填充
            g.FillPath(myLinearGradientBrush, DrawRoundRect(rectangle.X, rectangle.Y, rectangle.Width - span, rectangle.Height - 1, _radius));
        }
        public static GraphicsPath DrawRoundRect(int x, int y, int width, int height, int radius)
        {
            //四边圆角
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(x, y, radius, radius, 180, 90);
            gp.AddArc(width - radius, y, radius, radius, 270, 90);
            gp.AddArc(width - radius, height - radius, radius, radius, 0, 90);
            gp.AddArc(x, height - radius, radius, radius, 90, 90);
            gp.CloseAllFigures();
            return gp;
        }

        private async void   button1_Click(object sender, EventArgs e)
        {
           
           this.button1.Text =await task211();

            Console.WriteLine("主线程线程执行");
        }

        public  async Task<string> task211()
        {
            Task<string> task2 = Task.Factory.StartNew<string>(() =>
            {
                Thread.Sleep(10000);
                return $"hello, task2的ID为{ Thread.CurrentThread.ManagedThreadId}";
            });
            return await task2;
        }
        public  async Task TaskVoid()
        {
            //throw new Exception("Task线程异常");

            int age = int.Parse("Ten");
            await Task.Run(() => { Console.WriteLine("Task线程执行"); });
        }
    }
}
