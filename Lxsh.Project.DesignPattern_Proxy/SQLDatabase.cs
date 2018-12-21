
/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.DesignPattern_Proxy
*文件名： SQLDatabase
*创建人： Lxsh
*创建时间：2018/12/21 13:50:19
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/21 13:50:19
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
    public class SQLDatabase : IDatabase
    {
        public override void Add()
        {
            Console.WriteLine("sql数据添加成功");
        }

        public override void Remove()
        {
            Console.WriteLine("sql数据删除成功");
        }
    }
}