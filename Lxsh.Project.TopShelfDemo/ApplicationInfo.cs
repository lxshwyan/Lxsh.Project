/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lxsh.Project.TopShelfDemo
*文件名： ApplicationInfo
*创建人： Lxsh
*创建时间：2019/4/19 13:58:11
*描述
*=======================================================================
*修改标记
*修改时间：2019/4/19 13:58:11
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.TopShelfDemo
{
   public  class ApplicationInfo
    {
        /// <summary>
        /// 进程中显示名称
        /// </summary>
        public string ProcessName { get; set; }
        /// <summary>
        /// 应用程序安装路jing
        /// </summary>
        public string AppFilePath { get; set; }
        /// <summary>
        /// 应用程序的名称
        /// </summary>
        public string  AppDisplayName{ get; set; }
        /// <summary>
        /// 启动参数
        /// </summary>
        public string Args { get; set; }
    }
}