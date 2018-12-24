/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.DesignPattern_Mediator
*文件名： Mediator
*创建人： Lxsh
*创建时间：2018/12/24 10:46:05
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/24 10:46:05
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.DesignPattern_Mediator
{
      //抽象中介者角色
      public interface Mediator
      {
          void Command(Department department);
      }
}