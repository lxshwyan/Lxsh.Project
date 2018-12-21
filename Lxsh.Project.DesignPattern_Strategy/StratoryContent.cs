/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.DesignPattern_Strategy
*文件名： StratoryContent
*创建人： Lxsh
*创建时间：2018/12/21 16:11:33
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/21 16:11:33
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
    public class StratoryContent : Abstractlog
    {
        Abstractlog abstractlog;
        public StratoryContent(Abstractlog abstractlog)
        {
            this.abstractlog = abstractlog;
        }
        public StratoryContent()
        {
            this.abstractlog = new FileLog();
        }
        public override void WriteLog(string msg)
        {
            this.abstractlog.WriteLog(msg);
        }
    }
}