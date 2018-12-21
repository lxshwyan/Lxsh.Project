using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lxsh.Project.DesignPattern_TSingleton
{
   
   public class Singleton
    {
        private static Object _objectInstance = new object();
        public static Singleton _instance = null;
        private Singleton()
        {
            System.Threading.Thread.Sleep(5000);//模拟构造延迟测试
        } //构造方法私有化防止外面实例化

        #region 单例方法实现(饱汉试)
        //public static Singleton CreateInstance()
        //{
        //    if (_instance == null)        //双层判断优化性能+线程安全
        //    {
        //        lock (_objectInstance)
        //        {
        //            if (_instance == null)
        //            {
        //                _instance = new Singleton();
        //            }
        //        }
        //    }
        //    return _instance;
        //}
        #endregion

        #region 静态构造函数实现单例  (饿汉试)
        static Singleton()
        {
            if (_instance == null)        //双层判断优化性能+线程安全
            {
                lock (_objectInstance)
                {
                    if (_instance == null)
                    {
                        _instance = new Singleton();
                    }
                }
            }
        }
        #endregion
        public void Test()
        {
            Console.WriteLine("执行方法完成");
        }
    }
}