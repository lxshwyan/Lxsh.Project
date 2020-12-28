using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lxsh.Project.MapDataDemo
{
    public partial class Form1 : Form
    {
        private List<Province> Provinces = new List<Province>();
        private int level = 1;
        private Province CurrentProvince = new Province();
        private City CurrentCity = new City();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
        }
        private void AddListView(List<Province> Provinces)
        {
            TreeNode tn = new TreeNode("root");
            tn.Name = "全部";
            tn.Text = "全部";


            //将数据集加载到树形控件当中
            foreach (Province row in Provinces)
            {
                string strValue = row.provinceName;
                BindTreeData(tn, row.citys, strValue, row);
            }

            treeView1.Nodes.Add(tn);
            treeView1.Nodes[0].Expand();
         
        }

        private void BindTreeData(TreeNode tn, List<City> dtData, string strValue, Province row)
        {
            TreeNode tn1 = new TreeNode();
            tn1.Name = strValue;
            tn1.Text = strValue + "-" + row.car + "-" + row.idcard; ;
            tn1.Tag = row;
            tn.Nodes.Add(tn1);
            if (dtData != null)
            {
                if (dtData.Count > 0)
                {
                    foreach (City dr in dtData)
                    {
                        TreeNode tn2 = new TreeNode();
                        tn2.Name = dr.citysName;
                        tn2.Text = dr.citysName+"-"+dr.car+"-"+dr.idcard;
                        tn2.Tag = dr;
                        tn1.Nodes.Add(tn2);
                    }
                }
            }
            
        }

        public static List<T> ReadList<T>(string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    string str = streamReader.ReadToEnd(); ;
                    var st= JsonConvert.DeserializeObject<List<T>>(str);

                    return st;
                }
            }
        }


        public static void SaveList<T>(string path, List<T> data)
        {
            var strjson = JsonConvert.SerializeObject(data);
            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
                {
                    streamWriter.Write(strjson);
                }
            }
        }
       
        private void button2_Click(object sender, EventArgs e)
        {
           var data = Provinces.Where(item => item.provinceName == txtSheng.Text).FirstOrDefault();
          
            if (data != null)
            {
                data.car = txtShengChepai.Text;
                data.idcard = txtShengshengFfen.Text;
            }


            var sdata = Provinces.Where(item => item.provinceName == txtSheng.Text).FirstOrDefault().citys.Where(item => item.citysName == txtShi.Text).FirstOrDefault();
            if (sdata!=null)
            {
                sdata.car = txtShiChepai.Text;
                sdata.idcard = txtShishengFfen.Text;
            }
            SaveList(Application.StartupPath + "\\olderArea.js", Provinces);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level==1)
            {
                CurrentProvince = e.Node.Tag as Province;
                if (CurrentProvince != null)
                {
                    this.txtSheng.Text = CurrentProvince.provinceName;
                    this.txtShengChepai.Text = CurrentProvince.car;
                    this.txtShengshengFfen.Text = CurrentProvince.idcard;
                }
                else
                {
                    this.txtSheng.Text = "";
                    this.txtShengChepai.Text = "";
                    this.txtShengshengFfen.Text = "";
                }
               
            }
            else if (e.Node.Level==2)
            {
                CurrentCity = e.Node.Tag as City;
                if (CurrentCity != null)
                {
                    this.txtShi.Text = CurrentCity.citysName;
                    this.txtShiChepai.Text = CurrentCity.car;
                    this.txtShishengFfen.Text = CurrentCity.idcard;
                }
                else
                {
                    this.txtShi.Text = "";
                    this.txtShiChepai.Text ="";
                    this.txtShishengFfen.Text = "";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            Provinces = ReadList<Province>(Application.StartupPath + "\\olderArea.js");
            AddListView(Provinces);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            AddListView(Provinces);
        }
    }
    public class City
    {
        public string citysName { get; set; }
        public string car { get; set; }
        public string idcard { get; set; }
        public List<string> boundaries { get; set; }
    }
    public class Province
    {
        public string provinceName { get; set; }
        public string car { get; set; }
        public string idcard { get; set; }
        public List<City> citys { get; set; }
        public string[] boundaries { get; set; }
    }

}
