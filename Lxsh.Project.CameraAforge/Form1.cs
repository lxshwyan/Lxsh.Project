using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lxsh.Project.CameraAforge
{
    public partial class Form1 : Form
    {
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoDevice;
        private VideoCapabilities[] videoCapabilities;
        private VideoCapabilities[] snapshotCapabilities;
        private object objLock = new object();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count != 0)
            {
                foreach (FilterInfo device in videoDevices)
                {
                    cmbCamera.Items.Add(device.Name);
                }
            }
            else
            {
                cmbCamera.Items.Add("没有找到摄像头");
            }

        }

        private void cmbCamera_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (videoDevices.Count != 0)
            {
                videoDevice = new VideoCaptureDevice(videoDevices[cmbCamera.SelectedIndex].MonikerString);
                GetDeviceResolution(videoDevice);
            }
        }
        private void GetDeviceResolution(VideoCaptureDevice videoCaptureDevice)
        {
            cmbResolution.Items.Clear();
            videoCapabilities = videoCaptureDevice.VideoCapabilities;
            foreach (VideoCapabilities capabilty in videoCapabilities)
            {
                cmbResolution.Items.Add($"{capabilty.FrameSize.Width} x {capabilty.FrameSize.Height}");
            }
            cmbResolution.SelectedIndex = 0;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (videoDevice != null)
            {
                if ((videoCapabilities != null) && (videoCapabilities.Length != 0))
                {
                    videoDevice.VideoResolution = videoCapabilities[cmbResolution.SelectedIndex];
                    //设置对应的事件
                    videoDevice.NewFrame += VideoDevice_NewFrame;

                    vispShoot.VideoSource = videoDevice;
                    vispShoot.Start();
                   
                    EnableControlStatus(false);
                }
            }
        }

        private void VideoDevice_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            lock (objLock)
                        {
                            Bitmap bmp = null;
                       // if (isMultiPhoto)
                            {
                                bmp = (System.Drawing.Bitmap)eventArgs.Frame.Clone();
                             picbPreview.Image = bmp;
                    //string imgFolder = Common.GetImagePath();
                    //string picName = string.Format("{0}\\{1}.jpg", imgFolder, DateTime.Now.ToString("yyyyMMddHHmmss"));
                    //Common.SaveImage(picName, bmp);
                }
                            //Write Videos
                // if (isRecordVideo)
                              {
                                //  bmp = (System.Drawing.Bitmap)eventArgs.Frame.Clone();
                                 // videoWriter.WriteVideoFrame(bmp);
                              }
                      }

        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            DisConnect();
            EnableControlStatus(true);
        }
        private void DisConnect()
        {
            if (vispShoot.VideoSource != null)
            {
                vispShoot.SignalToStop();
                vispShoot.WaitForStop();
                vispShoot.VideoSource = null;
            }
        }
        private void EnableControlStatus(bool status)
        {
            cmbCamera.Enabled = status;
            cmbResolution.Enabled = status;
            btnConnect.Enabled = status;
            btnShoot.Enabled = !status;
            btnDisconnect.Enabled = !status;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisConnect();
        }

        private void btnShoot_Click(object sender, EventArgs e)
        {
          
            Bitmap img = vispShoot.GetCurrentVideoFrame();
            picbPreview.Image = img;
           
        }
    }
}
