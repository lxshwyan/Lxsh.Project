/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lxsh.Project.NetCore.ConsoleDemo.Define
*文件名： PropertiesConfigurationExtensions
*创建人： Lxsh
*创建时间：2019/1/14 14:09:01
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/14 14:09:01
*修改人：Lxsh
*描述：
************************************************************************/
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lxsh.Project.NetCore.ConsoleDemo.Define
{
   public static class PropertiesConfigurationExtensions
    {
        public static IConfigurationBuilder AddPropertiesFile(this IConfigurationBuilder builder, string path)
        {
            builder.Add(new PropertiesConfigurationSource(path));

            return builder;
        }
    }
}