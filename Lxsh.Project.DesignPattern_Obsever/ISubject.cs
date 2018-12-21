/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.DesignPattern_Obsever
*文件名： ISubject
*创建人： Lxsh
*创建时间：2018/12/21 17:08:55
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/21 17:08:55
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
    public interface ISubject
    {
        string SubjectState { get; set; }

        void Add(IObserver observer);

        void Remove(IObserver observer);

        void Nofity();
    }
}