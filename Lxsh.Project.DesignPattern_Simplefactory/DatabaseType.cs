/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.DesignPattern_Simplefactory
*文件名： DatabaseType
*创建人： Lxsh
*创建时间：2018/12/20 14:58:37
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/20 14:58:37
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Lxsh.Project.DesignPattern_Simplefactory
{    
    /// <summary>
    ///  数据库类型
    /// </summary>
    public enum DatabaseType
    {
        SqlServer,
        MySQL,
        PostgreSQL,
        SQLite,
        InMemory,
        Oracle,
        MariaDB,
        MyCat,
        Firebird,
        DB2,
        Access
    }
}