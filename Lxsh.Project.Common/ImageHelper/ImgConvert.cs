using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lxsh.Project.Common.ImageHelper
{
   public  class ImgConvert
    {
        /// <summary>
        /// 字节数组转换
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static Bitmap BytesToBitmap(byte[] buffer)
        {
            Bitmap bitmapResult = null;
            if (buffer != null && buffer.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream(buffer))
                {
                    try
                    {
                        bitmapResult = new Bitmap(ms);
                        return bitmapResult;
                    }
                    catch (Exception error)
                    {
                        return bitmapResult;
                    }
                    finally
                    {
                        ms.Close();
                        ms.Dispose();
                    }
                }
            }
            return null;

        }
        /// <summary>
        /// 字节数组转换为Image对象
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static Image BytesToImage(byte[] buffer)
        {
            if (buffer != null && buffer.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream(buffer))
                {
                    Image image = null;
                    try
                    {
                        image = Image.FromStream(ms);
                        return image;
                    }
                    catch (Exception error)
                    {
                        return image;
                    }
                    finally
                    {
                        ms.Close();
                        ms.Dispose();
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 按照一定的比率进行放大或缩小
        /// </summary>
        /// <param name="Percent">缩略图的宽度百分比 如：需要百分之80，就填0.8</param> 
        /// <param name="rotateFlipType">
        ///顺时针旋转90度     RotateFlipType.Rotate90FlipNone
        ///逆时针旋转90度     RotateFlipType.Rotate270FlipNone
        ///水平翻转           RotateFlipType.Rotate180FlipY
        ///垂直翻转          RotateFlipType.Rotate180FlipX
        ///保持原样           RotateFlipType.RotateNoneFlipNone
        /// </param>
        /// <param name="IsTransparent">背景是否透明</param>
        /// <returns>Bitmap对象</returns>
        public static Bitmap GetImage_Graphics(Image ResourceImage, double Percent, RotateFlipType rotateFlipType, bool IsTransparent)
        {
            Bitmap ResultBmp = null;
            try
            {
                if (ResourceImage != null)
                {
                    ResourceImage.RotateFlip(rotateFlipType);
                    int _newWidth = (int)Math.Round(ResourceImage.Width * Percent);
                    int _newHeight = (int)Math.Round(ResourceImage.Height * Percent);
                    ResultBmp = new System.Drawing.Bitmap(_newWidth, _newHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);//创建图片对象
                    Graphics g = null;
                    try
                    {
                        g = Graphics.FromImage(ResultBmp);//创建画板并加载空白图像
                        if (g != null)
                        {
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor; //System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;//设置保真模式为高度保真
                            g.DrawImage(ResourceImage, new Rectangle(0, 0, _newWidth, _newHeight), 0, 0, ResourceImage.Width, ResourceImage.Height, GraphicsUnit.Pixel);//开始画图
                            if (IsTransparent)
                            {
                                ResultBmp.MakeTransparent(System.Drawing.Color.Transparent);
                            }
                        }
                    }
                    catch (Exception errr)
                    {
                     
                        Thread.Sleep(100);
                    }
                    finally
                    {
                        if (g != null)
                        {
                            g.Dispose();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
             
                Thread.Sleep(100);
                return null;
            }
            finally
            {
                if (ResourceImage != null)
                {
                    ResourceImage.Dispose();
                }
            }
            return ResultBmp;
        }
        /// <summary>
        /// 图片等比缩放
        /// </summary>
        /// <param name="sImgFilePath"></param>
        /// <param name="Percent"></param>
        /// <returns></returns>
        public static bool ChangeImgSize(string sImgFilePath, double Percent, string sNewImgFilePath)
        {
            Image img = null;
            Bitmap bp = null;
            bool bSuccess = false;
            try
            {
                if (File.Exists(sImgFilePath) == false)
                {
                
                    return false;
                }
                img = Image.FromFile(sImgFilePath);
                if (img != null)
                {
                    bp = GetImage_Graphics(img, Percent, RotateFlipType.RotateNoneFlipNone, true);
                    if (bp != null)
                    {
                        //string sDirectory = FilePathUtils.getDirectory(sNewImgFilePath);
                        //if (sDirectory.EndsWith("\\") == false)
                        //{
                        //    sDirectory = string.Format("{0}\\", sDirectory);
                        //}
                        bp.Save(sNewImgFilePath);
                        bSuccess = true;
                    }
                }
            }
            catch (Exception ex)
            {
              
            }
            finally
            {
                if (img != null)
                {
                    img.Dispose();
                }
                if (bp != null)
                {
                    bp.Dispose();
                }
            }
            return bSuccess;
        }
        /// <summary>
        /// Resize图片
        /// </summary>
        /// <param name="bmp">原始Bitmap</param>
        /// <param name="newW">新的宽度</param>
        /// <param name="newH">新的高度</param>
        /// <returns>处理以后的Bitmap</returns>
        public static Bitmap ResizeBmp(Bitmap bmp, int newW, int newH)
        {
            try
            {
                Bitmap b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);
                g.SmoothingMode = SmoothingMode.HighSpeed;
                g.CompositingQuality = CompositingQuality.HighSpeed;
                g.InterpolationMode = InterpolationMode.Low;
                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();

                return b;
            }
            catch
            {
                return null;
            }
        }

        /// 将图片Image转换成Byte[]
        /// </summary>
        /// <param name="Image">image对象</param>
        /// <param name="imageFormat">后缀名</param>
        /// <returns></returns>
        public static byte[] ImageToBytes(Image Image, System.Drawing.Imaging.ImageFormat imageFormat)
        {
            if (Image == null) { return null; }
            byte[] data = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (Bitmap Bitmap = new Bitmap(Image))
                {
                    Bitmap.Save(ms, imageFormat);
                    ms.Position = 0;
                    data = new byte[ms.Length];
                    ms.Read(data, 0, Convert.ToInt32(ms.Length));
                    ms.Flush();
                }
            }
            return data;
        }

        /// <summary>
        /// 通过FileStream 来打开文件，这样就可以实现不锁定Image文件，到时可以让多用户同时访问Image文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Bitmap ReadImageFile(string path)
        {
            if (string.IsNullOrEmpty(path))
                return null;
            if (File.Exists(path) == false)
                return null;
            int filelength = 0;
            Bitmap bit = null;
            Byte[] image = null;
            try
            {
                using (FileStream fs = File.OpenRead(path))//OpenRead
                {
                    filelength = (int)fs.Length; //获得文件长度 
                    image = new Byte[filelength]; //建立一个字节数组 
                    fs.Read(image, 0, filelength); //按字节流读取 
                    System.Drawing.Image result = System.Drawing.Image.FromStream(fs);
                    bit = new Bitmap(result);
                    if (fs != null)
                    {
                        fs.Close();
                        fs.Dispose();
                    }
                }
            }
            catch (Exception err)
            {
              
            }
            finally
            {
                if (image != null)
                {
                    image = null;
                }
            }
            return bit;
        }
    }


}

