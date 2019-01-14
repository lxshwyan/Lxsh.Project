/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lxsh.Project.Thread.Demo
*文件名： EumDemo
*创建人： Lxsh
*创建时间：2019/1/2 15:23:40
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/2 15:23:40
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.Thread.Demo
{
    [Flags]
    public  enum Permission
    {
        create = 1,
        read = 2,
        update = 4,
        delete = 8,
    
    }
}