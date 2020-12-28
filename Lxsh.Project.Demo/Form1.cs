
using MongoDB.Driver;
using NewLife.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace Lxsh.Project.Demo
{
    public partial class Form1 : Form
    {
        TimeCost ts;
        public string MyProperty { get; set; } = "Form1Form1Form1Form1";
      
        public Form1()
        {

            InitializeComponent();
            pictureBox1.MouseEnter += PictureBox1_MouseEnter;
            pictureBox1.MouseLeave += PictureBox1_MouseLeave;
            pictureBox2.MouseEnter += PictureBox2_MouseEnter;
            pictureBox2.MouseLeave += PictureBox2_MouseLeave;

            //    ts = new TimeCost("test");

            //var client = new MongoClient("mongodb://127.0.0.1:27017");
            //    var mydb = client.GetDatabase("lxshProject");
            //    var mycollention = mydb.GetCollection<Test>("person");
            //    //新增数据
            //    //mycollention.InsertOne(new Test()
            //    //{
            //    //    Name = "lxsh",
            //    //    _id = 13131
            //    //});
            //    //获取数据
            //    ExpressionFilterDefinition<Test> expression = new ExpressionFilterDefinition<Test>(i => i.Name == "lxsh");
            //    List<Test> tests=   mycollention.Find<Test>(expression).ToList();
        }

        private void PictureBox1_MouseLeave(object sender, EventArgs e)
        {
            (sender as PictureBox).Image = new Bitmap((sender as PictureBox).Image).ChangeColor(Color.FromArgb(235,125,0));
        }

        private void PictureBox1_MouseEnter(object sender, EventArgs e)
        {
            (sender as PictureBox).Image = new Bitmap((sender as PictureBox).Image).ChangeColor(Color.Red);
        }
        private void PictureBox2_MouseLeave(object sender, EventArgs e)
        {
            (sender as PictureBox).Image = new Bitmap((sender as PictureBox).Image).ChangeColor(Color.FromArgb(235, 125, 0));
        }

        private void PictureBox2_MouseEnter(object sender, EventArgs e)
        {
            (sender as PictureBox).Image = new Bitmap((sender as PictureBox).Image).ChangeColor(Color.Red);
        }
        public class Test
        {
            public int _id { get; set; }

            public string Name { get; set; }
        }
      
        /// <summary>
        /// 对html字符串进行解码
        /// </summary>
        /// <param name="html">html字符串</param>
        public  string HtmlDecode(string html)
        {
            return HttpUtility.HtmlDecode(html);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string strUrl = $"http://192.168.137.252:6179/tool/voice?content='{UrlEncode("李先生")}'";
            axWindowsMediaPlayer1.URL = strUrl;
           
            //string  T=  GetPid(7819);
            //foreach (System.Diagnostics.Process item in System.Diagnostics.Process.GetProcesses())
            //{
            //    if (item.Id.ToString() == GetPid(7819))
            //    {
            //        item.Kill();
            //    }

            //}
            //  MessageBox.Show(T);
        }
        public async Task<int> GetVAsync(int t)
        {
            Console.WriteLine("调用开始");
            await Task.Run(() => { System.Threading.Thread.Sleep(2000); return t; });
            Console.WriteLine("调用完成");

            return 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IsSystemProcess("Lxsh.Project.Demo");
            KillProcess("Lxsh.Project.Demo");
            Console.WriteLine("主线程调用完成");
        }
        public static void KillProcess(string processName)
        {

            Process[] process = Process.GetProcessesByName(processName);
            foreach (var item in process)
            {
                item.Kill();
            }

        }
        /// <summary>
        /// 判断用户是否系统进程信息 （或系统进程启动的）
        /// </summary>
        /// <param name="processName"></param>
        /// <returns></returns>
        public static bool IsSystemProcess(string processName)
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.GetCurrentProcess();
          
            if (p.StartInfo.UserName.ToUpper() == "SYSTEM" || p.StartInfo.UserName.ToUpper() == "NETWORK SERVICE" || p.StartInfo.UserName.ToUpper() == "LOCAL SERVICE")
            {   
                return true;
            }
            return false;
        }
        public string GetPid(int nPort)
        {
            string pid = "-1";
            Process pro = new Process();
            List<int> ports = new List<int>();
            pro.StartInfo.FileName = "cmd.exe";
            pro.StartInfo.UseShellExecute = false;
            pro.StartInfo.RedirectStandardInput = true;
            pro.StartInfo.RedirectStandardOutput = true;
            pro.StartInfo.RedirectStandardError = true;
            pro.StartInfo.CreateNoWindow = true;
            pro.Start();
            pro.StandardInput.WriteLine("netstat -ano");
            pro.StandardInput.WriteLine("exit");
            Regex reg = new Regex("\\s+", RegexOptions.Compiled);
            string line = null;
            while ((line = pro.StandardOutput.ReadLine()) != null)
            {
                line = line.Trim();
                if (line.StartsWith("UDP", StringComparison.OrdinalIgnoreCase))
                {
                    line = reg.Replace(line, ",");
                    string[] arr = line.Split(',');
                    string soc = arr[1];
                    int pos = soc.LastIndexOf(':');
                    int pot = int.Parse(soc.Substring(pos + 1));
                    ports.Add(pot);
                    if (nPort == pot)
                    {
                        pid = arr[3];
                        pro.Close(); 
                        return pid;
                    }
                }

            }
            return pid;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Click -= button2_Click;
            MessageBox.Show("122");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SpeakProvider speakProvider = new SpeakProvider();
            speakProvider.SpeakAsync("13567946569");
        }
      
        private void button4_Click(object sender, EventArgs e)
        {
            
            Program.RefreshTaskbarIcon();
        }

        public string GetPath(string fileName)
        {
            return AppDomain.CurrentDomain.BaseDirectory + "\\" + fileName + ".json";
        }


        private void button5_Click(object sender, EventArgs e)
        {
            string _result = "";
            ConfigModel configModel = new ConfigModel();
            configModel.AssertGroupID = "2cafaef9-4c99-444b-bc85-88dd428a951b";
            var result=  ReadWriteJsonFile.SaveConfig<ConfigModel>("AB", configModel);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ConfigModel configModel = ReadWriteJsonFile.ReadConfig<ConfigModel>("AB");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ImageHelper.SaveNameImage("张三丰", AppDomain.CurrentDomain.BaseDirectory + "\\pic\\" + "张三丰.jpg");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SetViod(this.textBox1.Text);
        }
        private void SetViod(string viod)
        {
            string strUrl = $"http://192.168.137.252:6179/tool/voice?content='{UrlEncode(viod)}'";
            axWindowsMediaPlayer1.URL = strUrl;
        }
        /// <summary>
        /// 对html字符串进行编码
        /// </summary>
        /// <param name="html">html字符串</param>
        public string UrlEncode(string html)
        {
            return HttpUtility.UrlEncode(html, Encoding.GetEncoding("UTF-8"));
        }
    }

    public class DoorMessage
    {
        public int AssertTypeID;

        public int DevType { get; set; }
        public string AssertGroupID { get; set; }
        public int TypeID { get; set; }
        public string Expression { get; set; }
        public string JY { get; set; }
        public string Dept { get; set; }
        public string Photo { get; set; }
        public string DateTime { get; set; }
        public string Address { get; set; }
        public string CardID { get; set; }
        public string AssertName { get; set; }
        public string AssertID { get; set; }
        public string DoorID { get; set; }
        public string StatuesID { get; set; }
        public int CheckResult { get; set; }
    }
}

