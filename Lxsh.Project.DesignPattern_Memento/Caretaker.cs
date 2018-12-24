/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.DesignPattern_Memento
*文件名： Caretaker
*创建人： Lxsh
*创建时间：2018/12/24 10:17:21
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/24 10:17:21
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.DesignPattern_Memento
{
    // 管理角色
    public class Caretaker
    {
        public ContactMemento ContactM { get; set; }
    }
}