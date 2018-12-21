using Lxsh.Project.Common;
using MongoDB.Driver;
using NewLife.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lxsh.Project.Demo
{
    public partial class Form1 : Form
    {
        TimeCost ts;
        public Form1()
        {
            InitializeComponent();
            ts = new TimeCost("test");
          
        var client = new MongoClient("mongodb://127.0.0.1:27017");
            var mydb = client.GetDatabase("lxshProject");
            var mycollention = mydb.GetCollection<Test>("person");
            //新增数据
            //mycollention.InsertOne(new Test()
            //{
            //    Name = "lxsh",
            //    _id = 13131
            //});
            //获取数据
            ExpressionFilterDefinition<Test> expression = new ExpressionFilterDefinition<Test>(i => i.Name == "lxsh");
            List<Test> tests=   mycollention.Find<Test>(expression).ToList();
        }
        public class Test
        {
            public int _id { get; set; }

            public string Name { get; set; }
        }
      
    }
}
