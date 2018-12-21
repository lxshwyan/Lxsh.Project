/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.DesignPattern_Filter
*文件名： ClassFilter
*创建人： Lxsh
*创建时间：2018/12/21 15:19:26
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/21 15:19:26
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
    public class ClassFilter : IFilter
    {
        public List<Student> Filter(List<Student> student)
        {   
            return student.FindAll(s => s.ClassID == 1);
        }
    }
}