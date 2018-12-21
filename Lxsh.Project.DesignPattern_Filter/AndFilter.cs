/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.DesignPattern_Filter
*文件名： OrFilter
*创建人： Lxsh
*创建时间：2018/12/21 15:22:14
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/21 15:22:14
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lxsh.Project.DesignPattern_Filter
{
    public class AndFilter : IFilter
    {
        List<IFilter> filters = new List<IFilter>();

        public AndFilter(List<IFilter> filters)
        {
            this.filters = filters;
        }

        /// <summary>
        /// 将一组filter AND
        /// </summary>
        /// <param name="persons"></param>
        /// <returns></returns>
        public List<Student> Filter(List<Student> persons)
        {
            var temp_Students = new List<Student>(persons);

            foreach (var filterItem in filters)
            {
                temp_Students = filterItem.Filter(temp_Students);
            }

            return temp_Students;
        }
    }
}