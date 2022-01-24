using AForge.Video.DirectShow;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;

namespace Lxsh.Project.HealthCodeDemo
{
    public partial class Form1 : Form
    {
        public event EventHandler<HandEventD<string>> OnValueChanged;
        public Form1()
        {
            InitializeComponent();
        }
        VideoCaptureDevice _camera;
        private void button1_Click(object sender, EventArgs e)
        {
            _camera = new VideoCaptureDevice(new FilterInfoCollection(FilterCategory.VideoInputDevice)[0].MonikerString);
            _camera.NewFrame += camera_NewFrame;
            _camera.Start();
            timer1.Enabled = true;
            OnValueChanged += Form1_OnValueChanged;
        }

        private void Form1_OnValueChanged(object sender, HandEventD<string> e)
        {
            //e.Tvalue
        }

        private void camera_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            //将摄像头每帧图像显示到pictureBox
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                var img = (Bitmap)pictureBox1.Image.Clone();
                var barcodeReader = new BarcodeReader();
                var result = barcodeReader.Decode(img);
                if (result != null)
                {
                    try
                    {
                        var healthCode = JsonConvert.DeserializeAnonymousType(result.Text,
                                                new { Color = "" });
                        if (healthCode != null)
                        {
                            var color = healthCode.Color;
                            if (color == "green")
                            {
                                label1.Text = "绿码";
                                label1.ForeColor = Color.Green;
                            }
                            else if (color == "red")
                            {
                                label1.Text = "红码";
                                label1.ForeColor = Color.Red;
                            }
                            else if (color == "yellow")
                            {
                                label1.Text = "黄码";
                                label1.ForeColor = Color.Yellow;
                            }
                            else
                            {
                                label1.Text = "异常";
                            }
                        }
                    }
                    catch (Exception)
                    {

                        
                    }
                   
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _camera?.Stop();
        }
    }

    public class HandEventD<T> : EventArgs
    {
        public T Tvalue { get; set; }
    }
}
