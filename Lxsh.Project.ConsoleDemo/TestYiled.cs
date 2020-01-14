/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lxsh.Project.ConsoleDemo
*文件名： TestYiled
*创建人： Lxsh
*创建时间：2019/10/31 11:22:24
*描述
*=======================================================================
*修改标记
*修改时间：2019/10/31 11:22:24
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lxsh.Project.ConsoleDemo
{
    public class TestYiled
    {
        public static TestYiled _instance;
        private static object objLock = new object();
       private static List<int> _numArray; //用来保存1-100 这100个整数   
        static TestYiled()
        {
            if (_instance == null)
            {
                lock (objLock)
                {
                    if (_instance == null)
                    {
                        _instance = new TestYiled();
                        _numArray = new List<int>(); //给集合变量开始在堆内存上开内存，并且把内存首地址交给这个_numArray变量      
                        for (int i = 1; i <= 100; i++)
                        {
                            _numArray.Add(i);  //把1到100保存在集合当中方便操作
                        }

                    }
                }
            }
        }
        //测试求1到100之间的全部偶数
        public void TestYieldMethod()
        {
            foreach (var item in GetAllEvenNumber())
            {
                Console.WriteLine($"  common return:{item}");   
            }
        }
        //测试求1到100之间的全部偶数
        public void TestMethod()
        {
            foreach (var item in GetAllEvenNumberOld())
            {
                Console.WriteLine($"  common return:{item}");
            }
        }
        private IEnumerable<int> GetAllEvenNumber()
        {
            foreach (int item in _numArray)
            {
                if (item % 2 == 0) //判断是不是偶数
                {
                    Console.WriteLine($"yield return:{item}");    
                    yield return item; //返回当前偶数

                }   
            }
     
        }
        /// <summary>
        /// 使用平常返回集合方法
        /// </summary>
        /// <returns></returns>
        static IEnumerable<int> GetAllEvenNumberOld()
        {
            var listNum = new List<int>();
            foreach (int num in _numArray)
            {
                if (num % 2 == 0) //判断是不是偶数
                {
                    Console.WriteLine($"  Old return:{num}");
                    listNum.Add(num); //返回当前偶数

                }
            }
            return listNum;
        }


    }
}