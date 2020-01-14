using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lxsh.Project.WordsLibrary.Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] words = new[] { "敏感词1", "敏感词2", "含有", "垃圾","3" }; //敏感词组 可自行在网上 搜索下载   
            //敏感词库 类可被继承，如果想实现自定义 敏感词导入方法 可以 对 LoadWords 方法进行 重写
            var library = new WordsLibrary(words); //实例化 敏感词库  
            string text = "在任意一个文本中都可能包含敏感词1、2、3等等，只要含有敏感词都会被找出来，比如：垃圾";
            ContentCheck check = new ContentCheck(library, text);  //实例化 内容检测类
            var list = check.FindSensitiveWords();    //调用 查找敏感词方法 返回敏感词列表
            var str = check.SensitiveWordsReplace();  //调用 敏感词替换方法 返回处理过的字符串
        }
    }
}
