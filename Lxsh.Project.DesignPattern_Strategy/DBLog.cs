/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.DesignPattern_Strategy
*文件名： DBLog
*创建人： Lxsh
*创建时间：2018/12/21 16:07:45
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/21 16:07:45
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.DesignPattern_Strategy
{
    class DBLog : Abstractlog
    {
        public override void WriteLog(string msg)
        {
            Console.WriteLine("日志信息已记录到数据库中");
        }
    }
}