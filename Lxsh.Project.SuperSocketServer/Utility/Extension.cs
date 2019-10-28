/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lxsh.Project.SuperSocketServer.Utility
*文件名： Extension
*创建人： Lxsh
*创建时间：2019/8/12 16:58:56
*描述
*=======================================================================
*修改标记
*修改时间：2019/8/12 16:58:56
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class Extension
    {
        public static string Format(this string word)
        {
            return $"{word}\r\n";
        }
    }
}