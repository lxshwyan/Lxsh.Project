/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.DesignPattern_Filter
*文件名： AgeFilter
*创建人： Lxsh
*创建时间：2018/12/21 15:13:40
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/21 15:13:40
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
    public class AgeFilter : IFilter
    {
        public List<Student> Filter(List<Student> student)
        {    
            return student.FindAll(i => i.Age < 20);
        }
    }
}