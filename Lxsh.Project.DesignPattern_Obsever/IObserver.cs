/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.DesignPattern_Obsever
*文件名： IObserver
*创建人： Lxsh
*创建时间：2018/12/21 17:04:14
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/21 17:04:14
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.DesignPattern_Obsever
{
   public interface IObserver
    {
        void Modidy(string SubjectState);
    }
}