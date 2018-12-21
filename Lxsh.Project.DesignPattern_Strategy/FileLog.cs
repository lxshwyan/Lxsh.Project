/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.DesignPattern_Strategy
*文件名： FileLog
*创建人： Lxsh
*创建时间：2018/12/21 16:08:06
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/21 16:08:06
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
    public class FileLog : Abstractlog
    {
        public override void WriteLog(string msg)
        {
            Console.WriteLine("日志信息已记录本地");
        }
    }
}