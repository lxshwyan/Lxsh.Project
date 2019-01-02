using System;                                       //
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lxsh.Project.Thread.Demo
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());  
            #region Thread  ThreadPool  Task  
            //System.Threading.Thread thread = new System.Threading.Thread(()=> {  
            //    for (int i = 0; i < 2; i++)
            //    {
            //        System.Threading.Thread.Sleep(1000);
            //        Console.WriteLine($"测试验证多线程{i}");
            //      //  var test = Enumerable.Repeat(1, 10000).Select(m => m).ToList();
            //    }
            //});
            //thread.Start();
            //thread.Join();
            //Console.WriteLine("工作线程执行完成！！！！");

            //while (true)
            //{
            //    Console.Write(">");
            //    string str = Console.ReadLine();
            //    Console.WriteLine("请确认:"+ str);
            //}


            //Release         
            /// Thread.VolatileRead(ref isStop);
            /// 

            //    var path = Environment.CurrentDirectory + "//1.txt";
            //    var list = System.IO.File.ReadAllLines(path).Select(i => Convert.ToInt32(i)).ToList();
            //    for (int i = 0; i < 5; i++)
            //    {
            //        var watch = Stopwatch.StartNew();

            //        var mylist = BubbleSort(list);

            //        watch.Stop();

            //        Console.WriteLine(watch.Elapsed);
            //    }

            //    Console.Read();


            //ThreadPool.QueueUserWorkItem((obj) =>
            //{
            //    var func = obj as Func<string>;
            //    Console.WriteLine("我是工作线程:{0}, content={1}", System.Threading.Thread.CurrentThread.ManagedThreadId,
            //                                                    func());
            //},new Func<string> (TestFun)); //   ,new Func<string> (()=>"hello world") ;

            //Console.WriteLine("主线程ID：{0}", System.Threading.Thread.CurrentThread.ManagedThreadId);



            //ThreadPool.RegisterWaitForSingleObject(new AutoResetEvent(false), new WaitOrTimerCallback((obj, b) =>
            //{
            //    Console.WriteLine("我是定时器任务：{0},datatime={1}",obj,DateTime.Now.ToString());
            //}), "hello world", 1000, false);

            //第一种启动 Task
            //Task task = new Task(() =>
            //{
            //    Console.WriteLine("我是工作线程:{0}, datatime={1}", System.Threading.Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString());
            //});
            //task.Start();

            //第二种启动  Factory
            //Task.Factory.StartNew(() =>
            //{
            //    Console.WriteLine("我是工作线程:{0}, datatime={1}", System.Threading.Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString());
            //});

            //第三种启动  Run
            //Task.Run(() =>
            //{
            //    Console.WriteLine("我是工作线程:{0}, datatime={1}", System.Threading.Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString());
            //});

            ////第四种启动  同步执行（阻塞主线程）
            //var taskp = new Task(() =>
            // {
            //     System.Threading.Thread.Sleep(1000);
            //     Console.WriteLine("taskp我是工作线程:{0}, datatime={1}", System.Threading.Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString());
            // });
            //taskp.RunSynchronously();

            // Task task1 = new Task(() =>
            // {
            //     System.Threading.Thread.Sleep(2000);
            //     Console.WriteLine("task1我是工作线程:{0}, datatime={1}", System.Threading.Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString());
            // });
            // task1.Start();  
            //Task task2 = new Task(() =>
            // {
            //     System.Threading.Thread.Sleep(1000);
            //     Console.WriteLine("task2我是工作线程:{0}, datatime={1}", System.Threading.Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString());
            // });
            // task2.Start();
            //  task1.Wait();
            //  task2.Wait();
            //  Task.WaitAll(task1, task2); //两个线程全部完成后才执行后面 阻塞主线程
            //  Task.WaitAny(task1, task2);  //两个线程有一个完成后就执行后面     阻塞主线程
            // Task.WhenAll(task1, task2).ContinueWith(t => { Console.WriteLine("两个线程执行完成"); });//不阻塞主线程

            //  Console.WriteLine("工作线程执行完成！！！！");

            #endregion

            #region Task Enum    AttachedToParent   
            //1.AttachedToParent  ：指定将任务附加到任务层次结构中的某个父级     
            //建立了父子关系。。。  父任务想要继续执行，必须等待子任务执行完毕。。。。
            Task task = new Task(() =>{
                Task task1 = new Task(() => { System.Threading.Thread.Sleep(1000); Console.WriteLine("task1线程执行完成"); }, TaskCreationOptions.AttachedToParent);
                Task task2 = new Task(() => { System.Threading.Thread.Sleep(1000); Console.WriteLine("task2线程执行完成"); }, TaskCreationOptions.AttachedToParent);
                task1.Start();
                task2.Start();   
            });
           // TaskCreationOptions.DenyChildAttach   阻止子task附加
            task.Start();
            task.Wait();
            Console.WriteLine("主线程执行完成");
            #endregion



            Console.Read();

        }
        public static string TestFun()
        {
            return "hello world";
        }
        //冒泡排序算法
        static List<int> BubbleSort(List<int> list)
        {
            int temp;
            //第一层循环： 表明要比较的次数，比如list.count个数，肯定要比较count-1次
            for (int i = 0; i < list.Count - 1; i++)
            {
                //list.count-1：取数据最后一个数下标，
                //j>i: 从后往前的的下标一定大于从前往后的下标，否则就超越了。
                for (int j = list.Count - 1; j > i; j--)
                {
                    //如果前面一个数大于后面一个数则交换
                    if (list[j - 1] > list[j])
                    {
                        temp = list[j - 1];
                        list[j - 1] = list[j];
                        list[j] = temp;
                    }
                }
            }
            return list;
        }
    }
}
