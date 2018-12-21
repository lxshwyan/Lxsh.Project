﻿/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.DesignPattern_Prototype
*文件名： Prototype
*创建人： Lxsh
*创建时间：2018/12/21 10:50:42
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/21 10:50:42
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
    [Serializable]    //用C#提供的接口ICloneable代替我们的抽象类Prototype
    public  abstract class Prototype
      {
          public abstract Prototype Clone();
     }
}