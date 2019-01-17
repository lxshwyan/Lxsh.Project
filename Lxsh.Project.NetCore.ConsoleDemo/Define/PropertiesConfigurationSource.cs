/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lxsh.Project.NetCore.ConsoleDemo.Define
*文件名： PropertiesConfigurationSource
*创建人： Lxsh
*创建时间：2019/1/14 14:08:17
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/14 14:08:17
*修改人：Lxsh
*描述：
************************************************************************/
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lxsh.Project.NetCore.ConsoleDemo.Define
{
    public class PropertiesConfigurationSource : IConfigurationSource
    {
        private string path = string.Empty;
        public PropertiesConfigurationSource(string path)
        {
            this.path = path;
        }

        /// <summary>
        /// 生成Provider
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new PropertiesConfigurationProvider(this.path);
        }
    }
}