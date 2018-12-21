/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.DesignPattern_Prototype
*文件名： Student
*创建人： Lxsh
*创建时间：2018/12/21 10:52:55
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/21 10:52:55
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.DesignPattern_Prototype
{
    [Serializable]
    public class Student : ICloneable
    {
        public String Name { get; set; }
        public int Age { get; set; }     
        public StudentClass studentclass { get; set; }
                                                        
        public object Clone()
        {
            return  this.MemberwiseClone();
        }
    }
}