/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lxsh.Project.ConsoleDemo
*文件名： TestRegexWrite
*创建人： Lxsh
*创建时间：2019/10/17 10:30:51
*描述
*=======================================================================
*修改标记
*修改时间：2019/10/17 10:30:51
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Lxsh.Project.ConsoleDemo
{
  
   public class TestRegex
    {
        public static TestRegex _instance;
        private static  object objLock = new object();

        static TestRegex()
        {
            if (_instance==null)
            {
                lock (objLock)
                {
                    if (_instance == null)
                        _instance = new TestRegex();
                } 
            }  
        }
        public  void RegexWrite()
        {
            //var text = "sadas asds 12312 asd     asdas  asd";
            Console.WriteLine("请输入要处理的字符串：");
            var text = Console.ReadLine();
            var pattern = "([\\s]{2,})";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(text))
            {
                Console.WriteLine($"去除连续空格:{Regex.Replace(text, pattern, " ")}");
            }
            else
                Console.WriteLine($"没有连续空格，不需要处理！");


            RegexWrite();
        }
        /// <summary>
        /// 生成文件的
        /// </summary>
        /// <param name="calssName"></param>
        public void create(string calssName)
        {
            //获取程序集
            var createClass = Assembly.Load("Entity");
            //反射出所有的类
            List<Type> ts = createClass.GetTypes().ToList();
            //循环生成
            ts.ForEach(x =>
            {
                //x.Namespace获取命名空间
                var ss = "using " + x.Namespace + ";\n" +
                         "using CoreFramework." + calssName + ";\n" +
                         "using System;\n" +
                         "using System.Collections.Generic;\n" +
                         "using System.Text;\n\n" +

                         "namespace I" + calssName + "\n" +
                            "{\n" +
                                "\tpublic interface I" + x.Name.Substring(0, x.Name.Length - 6) + "" + calssName + " : IBase" + calssName + "<" + x.Name + ">\n" +
                                "\t{\n" +

                                "\t}\n" +
                            "}\n";
                //创建文件夹
                if (!Directory.Exists(@"C:\Users\Desktop\I" + calssName + ""))
                {
                    Directory.CreateDirectory(@"C:\Users\Desktop\I" + calssName + "");
                }
                //创建文件夹
                if (!Directory.Exists(@"C:\Users\Desktop\I" + calssName + @"\" + x.Name.Substring(0, x.Name.Length - 6) + ""))
                {
                    Directory.CreateDirectory(@"C:\Users\Desktop\I" + calssName + @"\" + x.Name.Substring(0, x.Name.Length - 6) + "");
                }
                //保存 开启文件流
                using (FileStream fs = new FileStream(@"C:\Users\Desktop\I" + calssName + @"\" + x.Name.Substring(0, x.Name.Length - 6) + @"\I" + x.Name.Substring(0, x.Name.Length - 6) + "" + calssName + ".cs", FileMode.Create))
                {
                    //文本写入 开启读写流
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write(ss);
                    }
                }
            });
        }

    }
}