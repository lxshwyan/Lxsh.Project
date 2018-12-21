/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.DesignPattern_TemplateMethod
*文件名： ZhangSan
*创建人： Lxsh
*创建时间：2018/12/20 17:29:04
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/20 17:29:04
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
    public class ZhangSan : Exam
    {

       

        public override string Name
        {
            get
            {
                return "张三";
            }

        }
        public override string Answer()
        {
            return "暖和的很呢";
        }
    }
}