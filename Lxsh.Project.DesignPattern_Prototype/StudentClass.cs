/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.DesignPattern_Prototype
*文件名： StudentClass
*创建人： Lxsh
*创建时间：2018/12/21 10:58:37
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/21 10:58:37
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
   public class StudentClass: ICloneable
    {
        public int ClassID { get; set; }
        public string ClassName { get; set; }
       
        public  object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}