using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lxsh.Project.Common.Pool
{
    /// <summary>
    /// 对象池接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPool<T> where T : class
    {
        /// <summary>
        /// 对象大小
        /// </summary>
        Int32 Max { get; set; }
        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        T Get();
        /// <summary>
        /// 归还
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        Boolean Put(T value);
        /// <summary>
        /// 清空
        /// </summary>
        /// <returns></returns>
        Int32 Clear();
    }

    public class Pool<T> : IPool<T> where T : class
    {
        /// <summary>对象池大小。默认CPU*2，初始化后改变无效</summary>
        public int Max { get; set; }
        private Item[] _items;
        private T _current;
        struct Item
        {
            public T Value;
        }
        #region 构造
        /// <summary>
        /// 实例化对象池，默认大小CPU*2
        /// </summary>
        /// <param name="max"></param>
        public Pool(Int32 max = 0)
        {
            if (max < 0) max = Environment.ProcessorCount * 2;
            Max = max;
        }
        #endregion
        private void Init()
        {
            if (_items != null) return;
            lock (this)
            {
                if (_items != null) return;

                _items = new Item[Max - 1];
            }
        }
        public virtual int Clear()
        {
            var count = 0;

            if (_current != null)
            {
                _current = null;
                count++;
            }

            var items = _items;
            for (var i = 0; i < items.Length; ++i)
            {
                if (items[i].Value != null)
                {
                    items[i].Value = null;
                    count++;
                }
            }
            _items = null;

            return count;
        }

        public virtual T Get()
        {
            var val = _current;

            if (val != null && Interlocked.CompareExchange(ref _current, null, val) == val) return val;

            Init();

            var items = _items;
            for (var i = 0; i < items.Length; i++)
            {
                val = items[i].Value;
                if (val != null && Interlocked.CompareExchange(ref items[i].Value, null, val) == val) return val;
            }

            return OnCreate();
        }

        public virtual bool Put(T value)
        {
            // 最热的一个对象在外层，便于快速存取
            if (_current == null && Interlocked.CompareExchange(ref _current, value, null) == null) return true;

            Init();

            var items = _items;
            for (var i = 0; i < items.Length; ++i)
            {
                if (Interlocked.CompareExchange(ref items[i].Value, value, null) == null) return true;
            }

            return false;
        }
        #region 重载
        /// <summary>创建实例</summary>
        /// <returns></returns>
        protected virtual T OnCreate() => Activator.CreateInstance(typeof(T), true) as T;

    }
    /// <summary>内存流池</summary>
    public class MemoryStreamPool : Pool<MemoryStream>
    {
        /// <summary>初始容量。默认1024个</summary>
        public Int32 InitialCapacity { get; set; } = 1024;

        /// <summary>最大容量。超过该大小时不进入池内，默认64k</summary>
        public Int32 MaximumCapacity { get; set; } = 64 * 1024;

        /// <summary>创建</summary>
        /// <returns></returns>
        protected override MemoryStream OnCreate() => new(InitialCapacity);

        /// <summary>归还</summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override Boolean Put(MemoryStream value)
        {
            if (value.Capacity > MaximumCapacity) return false;

            value.Position = 0;
            value.SetLength(0);

            return true;
        }
    }
}
