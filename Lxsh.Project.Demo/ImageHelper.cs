using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.Demo
{
  public  class ImageHelper
    {

        #region 姓名生成图片处理
        /// <summary>
        /// 获取姓名对应的颜色值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetNameColor(string name)
        {
            if (string.IsNullOrEmpty(name) || name.Length <= 0)
                throw new Exception("name不能为空");
            //获取名字第一个字,转换成 16进制 图片
            string str = "";
            foreach (var item in name)
            {
                str += Convert.ToUInt16(item);
            }
            if (str.Length < 4)
            {
                str += new Random().Next(100, 1000);
            }
            string color = "#" + str.Substring(1, 3);
            return color;
        }
        /// <summary>
        /// 获取姓名对应的图片 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Bitmap GetNameImage(string name, int width = 1396, int height = 132)
        {
            string color = GetNameColor(name);
            string firstName = name;//.Substring(0, 1);
            Bitmap img = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(img);
            Brush brush = new SolidBrush(ColorTranslator.FromHtml(color));
            g.FillRectangle(brush, 0, 0, width, height);
            //填充文字
            Font font = new Font("微软雅黑", 50);
            SizeF firstSize = g.MeasureString(firstName, font);
            g.DrawString(firstName, font, Brushes.White, new PointF((img.Width - firstSize.Width) / 2, (img.Height - firstSize.Height) / 2));
            g.Dispose();
            return img;
        }
        /// <summary>
        /// 保存图片到磁盘
        /// </summary>
        /// <param name="name"></param>
        /// <param name="targetFile"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Bitmap SaveNameImage(string name, string targetFile, int width = 1396, int height = 132)
        {
            Bitmap img = GetNameImage(name, width, height);
            img.Save(targetFile, System.Drawing.Imaging.ImageFormat.Jpeg);
            img.Dispose();
            return img;
        }
        #endregion
    }
}
