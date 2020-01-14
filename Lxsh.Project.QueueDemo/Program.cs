using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lxsh.Project.QueueDemo
{
    class Program
    {
        #region 队列相关
        static Queue<ThreadParam> _tasks = new Queue<ThreadParam>();
        static readonly object _locker = new object();
        static EventWaitHandle _wh = new AutoResetEvent(false);
        static Thread _worker;
        #endregion
        static void Main(string[] args)
        {
            _worker = new Thread(Work);
            _worker.Start();

            while (true)
            {
             
                if (Console.ReadLine()=="1")
                {
                    AddMssage();
                }
              
            }
        }
        #region 队列相关
        static void AddMssage()
        {
            EnqueueTask(new ThreadParam() { dtStartTime = DateTime.Now });
        }
        static void Work()
        {
            while (true)
            {
                ThreadParam work = null;
                lock (_locker)
                {
                    if (_tasks.Count > 0)
                    {
                        work = _tasks.Dequeue();
                        if (null == work)
                        {
                            return;
                        }
                    }
                }

              //  Console.WriteLine("while reached");
                if (work != null)
                {
                    Preview(work);
                }
                else
                {
                   // Console.WriteLine("WaitOne in Work");
                   _wh.WaitOne();
                    Console.WriteLine("WaitOne OK in Work");
                }
            }
        }
        static void Preview(object previewParam)
        {
            ThreadParam _threadParam = (ThreadParam)previewParam;
            if (null == _threadParam)
            {
                return;
            }
            Console.WriteLine($"执行了一个方法：{_threadParam.dtStartTime}");
        }
        static void EnqueueTask(ThreadParam obj)
        {
            lock (_locker)
            {
                _tasks.Enqueue(obj);
            }     
         //   Console.WriteLine("fireEvent in EnqueueTask");
           _wh.Set();  // 给Work线程发信号
            //InsertOperLogInfo("发信号", "发信号", 0);
        }

        static void Dispose()
        {
            EnqueueTask(null);
            _worker.Join();
            _wh.Close();
        }
        #endregion
    }

}
