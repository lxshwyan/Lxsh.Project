/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lxsh.Project.DesignPattern_TemplateMethod
*文件名： SQLRepository
*创建人： Lxsh
*创建时间：2019/4/30 10:32:54
*描述
*=======================================================================
*修改标记
*修改时间：2019/4/30 10:32:54
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.DesignPattern_TemplateMethod
{
    public   class MySQLRepository<T>  :Repository<T> where T:class
    {
        public override void BulkInsert<T>(List<T> entities)
        {
            //   可以直接利用sql语句执行,
        }
    }
}