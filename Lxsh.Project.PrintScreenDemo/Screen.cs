using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lxsh.Project.PrintScreenDemo
{
  public  class Screen
    {
        protected static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public void ScreenShot(string url, string path
          , int width = 400, int height = 300
          , int left = 50, int top = 50
          , int resizeMaxWidth = 1920, int wait = 1)
        {
            if (!string.IsNullOrEmpty(url) && !string.IsNullOrEmpty(path))
            {
                ScreenShotParam requestParam = new ScreenShotParam
                {
                    Url = url,
                    SavePath = path,
                    Width = width,
                    Height = height,
                    Left = left,
                    Top = top,
                    ResizeMaxWidth = resizeMaxWidth,
                    Wait = wait != 0
                };
                startPrintScreen(requestParam);
            }
        }

        void startPrintScreen(ScreenShotParam requestParam)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(do_PrintScreen));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start(requestParam);
            if (requestParam.Wait)
            {
                thread.Join();
                FileInfo result = new FileInfo(requestParam.SavePath);
                long minSize = 1 * 1024;// 太小可能是空白圖，重抓
                int maxRepeat = 2;
                while ((!result.Exists || result.Length <= minSize) && maxRepeat > 0)
                {
                    thread = new Thread(new ParameterizedThreadStart(do_PrintScreen));
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start(requestParam);
                    thread.Join();
                    maxRepeat--;
                }
            }
        }

        void do_PrintScreen(object param)
        {
            try
            {
                ScreenShotParam screenShotParam = (ScreenShotParam)param;
                string requestUrl = screenShotParam.Url;
                string savePath = screenShotParam.SavePath;
                WebBrowser wb = new WebBrowser();
                wb.ScrollBarsEnabled = false;
                wb.ScriptErrorsSuppressed = true;
                wb.Navigate(requestUrl);
                logger.Debug("wb.Navigate");
                DateTime startTime = DateTime.Now;
                TimeSpan waitTime = new TimeSpan(0, 0, 0, 10, 0);// 10 second
                while (wb.ReadyState != WebBrowserReadyState.Complete)
                {
                    Application.DoEvents();
                    if (DateTime.Now - startTime > waitTime)
                    {
                        wb.Dispose();
                        logger.Debug("wb.Dispose() timeout");
                        return;
                    }
                }

                wb.Width = screenShotParam.Left + screenShotParam.Width + screenShotParam.Left; // wb.Document.Body.ScrollRectangle.Width (避掉左右側的邊線);
                wb.Height = screenShotParam.Top + screenShotParam.Height; // wb.Document.Body.ScrollRectangle.Height;
                wb.ScrollBarsEnabled = false;
                wb.Document.Body.Style = "overflow:hidden";//hide scroll bar
                var doc = (wb.Document.DomDocument) as mshtml.IHTMLDocument2;
                var style = doc.createStyleSheet("", 0);
                style.cssText = @"img { border-style: none; }";

                Bitmap bitmap = new Bitmap(wb.Width, wb.Height);
                wb.DrawToBitmap(bitmap, new Rectangle(0, 0, wb.Width, wb.Height));
                wb.Dispose();
                logger.Debug("wb.Dispose()");

                bitmap = CutImage(bitmap, new Rectangle(screenShotParam.Left, screenShotParam.Top, screenShotParam.Width, screenShotParam.Height));
                bool needResize = screenShotParam.Width > screenShotParam.ResizeMaxWidth || screenShotParam.Height > screenShotParam.ResizeMaxWidth;
                if (needResize)
                {
                    double greaterLength = bitmap.Width > bitmap.Height ? bitmap.Width : bitmap.Height;
                    double ratio = screenShotParam.ResizeMaxWidth / greaterLength;
                    bitmap = Resize(bitmap, ratio);
                }

                bitmap.Save(savePath, System.Drawing.Imaging.ImageFormat.Gif);
                bitmap.Dispose();
                logger.Debug("bitmap.Dispose();");
                logger.Debug("finish");

            }
            catch (Exception ex)
            {
                logger.Info($"exception: {ex.Message}");
            }
        }

        private static Bitmap CutImage(Bitmap source, Rectangle section)
        {
            // An empty bitmap which will hold the cropped image
            Bitmap bmp = new Bitmap(section.Width, section.Height);
            //using (Bitmap bmp = new Bitmap(section.Width, section.Height))
            {
                Graphics g = Graphics.FromImage(bmp);

                // Draw the given area (section) of the source image
                // at location 0,0 on the empty bitmap (bmp)
                g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);

                return bmp;
            }
        }

        private static Bitmap Resize(Bitmap originImage, Double times)
        {
            int width = Convert.ToInt32(originImage.Width * times);
            int height = Convert.ToInt32(originImage.Height * times);

            return ResizeProcess(originImage, originImage.Width, originImage.Height, width, height);
        }

        private static Bitmap ResizeProcess(Bitmap originImage, int oriwidth, int oriheight, int width, int height)
        {
            Bitmap resizedbitmap = new Bitmap(width, height);
            //using (Bitmap resizedbitmap = new Bitmap(width, height))
            {
                Graphics g = Graphics.FromImage(resizedbitmap);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.Clear(Color.Transparent);
                g.DrawImage(originImage, new Rectangle(0, 0, width, height), new Rectangle(0, 0, oriwidth, oriheight), GraphicsUnit.Pixel);
                return resizedbitmap;
            }
        }
    }

    public class ScreenShotParam
    {
        public string Url { get; set; }
        public string SavePath { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        /// <summary>
        /// 長邊縮到指定長度
        /// </summary>
        public int ResizeMaxWidth { get; set; }
        public bool Wait { get; set; }
    }
}
