/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.WcfServiceLib
*文件名： EventAlarmInfo
*创建人： Lxsh
*创建时间：2018/12/29 10:27:56
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/29 10:27:56
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.WcfServiceLib
{
    public class EventAlarmInfo : IEventAlarm
    {
        public void DoWork()
        {
            Console.WriteLine("EventAlarmInfo----DoWork");
        }
    }
}