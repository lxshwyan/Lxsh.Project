/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lxsh.Project.ConsoleDemo
*文件名： TestMemoryStream
*创建人： Lxsh
*创建时间：2019/11/24 17:01:30
*描述
*=======================================================================
*修改标记
*修改时间：2019/11/24 17:01:30
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
    public class TestMemoryStream
    {
        public static TestMemoryStream _instance;
        private static object objLock = new object();

        static TestMemoryStream()
        {
            if (_instance == null)
            {
                lock (objLock)
                {
                    if (_instance == null)
                        _instance = new TestMemoryStream();
                }
            }
        }
        public void TestStream()
        {
            var testBytes = new byte[256 * 1024 * 1024];
            var ms = new MemoryStream();
            using (ms)
            {
                for (int i = 0; i < 1000; i++)
                {
                    try
                    {
                        ms.Write(testBytes, 0, testBytes.Length);
                    }
                    catch
                    {
                        Console.WriteLine("该内存流已经使用了{0}M容量的内存,该内存流最大容量为{1}M,溢出时容量为{2}M",
                            GC.GetTotalMemory(false) / (1024 * 1024),//MemoryStream已经消耗内存量
                            ms.Capacity / (1024 * 1024), //MemoryStream最大的可用容量
                            ms.Length / (1024 * 1024));//MemoryStream当前流的长度（容量）
                        break;
                    }
                }
            }
            Console.ReadLine();
        }
    }
}