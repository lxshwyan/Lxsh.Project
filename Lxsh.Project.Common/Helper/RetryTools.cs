using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.Common.Helper
{
   public class RetryTools
    {
        static int sleepMillisecondsTimeout = 1000;
        /// <summary>
        /// 若發生 Exception (資料庫查詢逾時)，重複執行相同動作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handler"></param>
        /// <param name="retryTimes">預設重試 3次，傳入 0直接 return default(T)</param>
        /// <returns></returns>
        public static T Retry<T>(Func<T> handler, int retryTimes = 3)
        {
            if (retryTimes <= 0)
            {
                return default(T);
            }

            try
            {
                return handler();
            }
            catch (Exception e)
            {
                retryTimes--;
                System.Threading.Thread.Sleep(sleepMillisecondsTimeout);
                return Retry(handler, retryTimes);
            }
        }

        /// <summary>
        /// 傳入多個動作，遇到 Exception依序執行 (資料庫查詢逾時，改用不同條件查詢)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handlers"></param>
        /// <returns></returns>
        public static T Retry<T>(params Func<T>[] handlers)
        {
            for (int i = 0; i < handlers.Length; i++)
            {
                var handler = handlers[i];
                try
                {
                    return handler();
                }
                catch (Exception e)
                {
                    System.Threading.Thread.Sleep(sleepMillisecondsTimeout);
                }
            }
            return default(T);
        }

        /// <summary>
        /// 若發生 Exception (資料庫查詢逾時)，重複執行相同動作
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="retryTimes">預設重試 3次，傳入 0直接 return</param>
        public static void Retry(Action handler, int retryTimes = 3)
        {
            if (retryTimes <= 0)
            {
                return;
            }

            try
            {
                handler();
            }
            catch (Exception e)
            {
                retryTimes--;
                System.Threading.Thread.Sleep(sleepMillisecondsTimeout);
                Retry(handler, retryTimes);
            }
        }
    }

}
