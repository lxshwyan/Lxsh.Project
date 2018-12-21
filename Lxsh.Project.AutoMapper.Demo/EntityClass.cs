/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.AutoMapper.Demo
*文件名： EntityClass
*创建人： Lxsh
*创建时间：2018/12/20 11:46:32
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/20 11:46:32
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Lxsh.Project.AutoMapper.Demo
{
    public class EntityUser
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int Sex { get; set; }
        public int ClassID { get; set; }
    }
    public class EntityClass
    {
        public string Name { get; set; }      
        public int ClassID { get; set; }
    }
}