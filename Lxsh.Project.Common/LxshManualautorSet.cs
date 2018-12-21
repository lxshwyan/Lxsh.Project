/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.Common
*文件名： LxshManualautorSet
*创建人： Lxsh
*创建时间：2018/12/19 13:49:09
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/19 13:49:09
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lxsh.Project.Common
{
   public  class LxshManualautorSet
    {
        private object sigLock = new object();
        private bool paused = true;
        public void WaitOne()
        {
            lock (sigLock)
            {
                while (paused)
                {
                    Monitor.Wait(sigLock);   
                }
            }
        }
        public void SetAll()
        {
            lock (sigLock)
            {
                paused = false;
                Monitor.PulseAll(sigLock);
            }
        }
        public void Set()
        {
            lock (sigLock)
            {
                paused = false;
                Monitor.Pulse(sigLock);
            }
        }
    }
}