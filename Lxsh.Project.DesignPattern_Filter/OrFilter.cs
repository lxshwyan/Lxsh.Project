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
using System.Threading.Tasks;

namespace Lxsh.Project.DesignPattern_Filter
{
    public class OrFilter : IFilter
    {
        List<IFilter> filters = new List<IFilter>();
        public OrFilter(List<IFilter> filters)
        {
            this.filters = filters;
        }
        public List<Student> Filter(List<Student> student)
        {
            var hashset = new HashSet<Student>();

            foreach (var filterItem in filters)
            {
                var filterstudent = filterItem.Filter(student);

             
                foreach (var Tstudent in filterstudent)
                {
                    hashset.Add(Tstudent);
                }
            }

            return hashset.ToList();
        }
    }
}