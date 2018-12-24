/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.DesignPattern_Memento
*文件名： ContactMemento
*创建人： Lxsh
*创建时间：2018/12/24 10:16:10
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/24 10:16:10
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
   public class ContactMemento
    {
        // 保存发起人的内部状态
        public List<ContactPerson> ContactPersonBack;

        public ContactMemento(List<ContactPerson> persons)
        {
            ContactPersonBack = persons;
        }
    }
}