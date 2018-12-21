/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.DesignPattern_Iterator
*文件名： Aggregation
*创建人： Lxsh
*创建时间：2018/12/21 8:40:14
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/21 8:40:14
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.DesignPattern_Iterator
{
   public class LxshAggregation<T>
    {
        private List<T> list = new List<T>();
      
        public LxshEnumerable<T> GetEnumerator()
        {
            return new LxshEnumerable<T>(this);
        }

        public void Add(T value)
        {
            list.Add(value);
        }

        public T this[int index]
        {
            get
            {
                return list[index];
            }
        }

        public int Length
        {
            get
            {
                return list.Count;
            }
        }
        
    }
}