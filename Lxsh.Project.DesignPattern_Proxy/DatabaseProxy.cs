/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.DesignPattern_Proxy
*文件名： DatabaseProxy
*创建人： Lxsh
*创建时间：2018/12/21 13:49:39
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/21 13:49:39
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
    public class DatabaseProxy : IDatabase
    {
        SQLDatabase sql = new SQLDatabase();
        public override void Add()
        {
            sql.Add();
        }

        public override void Remove()
        {
            sql.Remove();
        }
    }
}