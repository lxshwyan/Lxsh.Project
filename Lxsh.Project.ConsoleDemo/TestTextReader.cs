/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lxsh.Project.ConsoleDemo
*文件名： TestTextReader
*创建人： Lxsh
*创建时间：2019/10/23 16:46:45
*描述
*=======================================================================
*修改标记
*修改时间：2019/10/23 16:46:45
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lxsh.Project.ConsoleDemo
{
   public class TestTextReader
    {
        public static TestTextReader _instance;
        private static object objLock = new object();

        static TestTextReader()
        {
            if (_instance == null)
            {
                lock (objLock)
                {
                    if (_instance == null)
                        _instance = new TestTextReader();
                }
            }  
        }
        public void Reader()
        {
            string text = System.Environment.CurrentDirectory + "//1.text";
            using (TextReader reader=new StringReader(text))
            {
                while (reader.Peek() != -1)
                {
                    Console.WriteLine("Peek = {0}", (char)reader.Peek());
                    Console.WriteLine("Read = {0}", (char)reader.Read());

                }
                reader.Close();  
            }
        }
    }
}