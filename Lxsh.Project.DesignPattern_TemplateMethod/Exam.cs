/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.DesignPattern_TemplateMethod
*文件名： Exam
*创建人： Lxsh
*创建时间：2018/12/20 17:13:50
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/20 17:13:50
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.DesignPattern_TemplateMethod
{
    public abstract class Exam
    {
        public virtual string Name { get; set; }
        public void Questions()
        {
            Console.WriteLine(string.Format("{0} 今天暖和吗？ {1}", Name, Answer()));
        }

        public virtual string Answer()
        {
            return "不知道！";
        }
    }
}