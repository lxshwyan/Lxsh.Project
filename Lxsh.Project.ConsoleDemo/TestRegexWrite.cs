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
using System.Linq;
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

    }
}