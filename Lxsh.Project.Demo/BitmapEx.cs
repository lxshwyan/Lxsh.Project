using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.Demo
{
  public  static  class BitmapEx
    {
        public static Bitmap ChangeColor(this Bitmap originalBmp, Color color)
        {
            Bitmap bitmap = originalBmp.Clone() as Bitmap;//创建一个副本
            unsafe
            {
                Rectangle rectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                BitmapData bmpdata = bitmap.LockBits(rectangle, System.Drawing.Imaging.ImageLockMode.ReadWrite, bitmap.PixelFormat);//锁定到内存
                byte A;//Alpha
                unsafe
                {
                    byte* ptr = (byte*)(bmpdata.Scan0);//得到起始指针
                    for (int x = 0; x < bmpdata.Width; x++)
                    {
                        for (int y = 0; y < bmpdata.Height; y++)
                        {
                            A = ptr[3];
                            ptr[0] = color.B;
                            ptr[1] = color.G;
                            ptr[2] = color.R;
                            ptr[3] = ptr[3];
                            ptr += 4;
                        }
                        ptr += bmpdata.Stride - bmpdata.Width * 4;
                    }
                }
                bitmap.UnlockBits(bmpdata);
            }
            return bitmap;
        }
    }
}
