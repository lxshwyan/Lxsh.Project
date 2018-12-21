/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.DesignPattern_Proxy
*文件名： IDatabase
*创建人： Lxsh
*创建时间：2018/12/21 13:47:09
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/21 13:47:09
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.DesignPattern_Proxy
{
   public abstract class IDatabase
    {
        public abstract   void Add();
        public abstract void Remove();
    }
}