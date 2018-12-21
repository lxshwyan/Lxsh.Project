/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.DesignPattern_Iterator
*文件名： LxshEnumerable
*创建人： Lxsh
*创建时间：2018/12/21 8:39:49
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/21 8:39:49
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
    public class LxshEnumerable<T> : ILxshEnumerable<T>
    {
        private LxshAggregation<T> aggregation = new LxshAggregation<T>();
        private T current;
        private int index = 0;

        public T Current { get => current;}
        public LxshEnumerable(LxshAggregation<T> aggregation)
        {
            this.aggregation = aggregation;
        }
        public bool MoveNext()
        {
            if (index < aggregation.Length)
            {
                this.current = aggregation[index];

                index++;

                return true;
            } 
            return false;
        }
    }
}