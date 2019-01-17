/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lxsh.Project.NetCore.ConsoleDemo.Define
*文件名： PropertiesConfigurationProvider
*创建人： Lxsh
*创建时间：2019/1/14 14:07:17
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/14 14:07:17
*修改人：Lxsh
*描述：
************************************************************************/
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lxsh.Project.NetCore.ConsoleDemo.Define
{
    public class PropertiesConfigurationProvider: ConfigurationProvider
    {
        private string path = string.Empty;
        public PropertiesConfigurationProvider(string path)
        {
            this.path = path;
        }

        /// <summary>
        /// 用于解析 “*.properties”
        /// </summary>
        public override void Load()
        {
            var lines = System.IO.File.ReadAllLines(this.path);

            var dict = new Dictionary<string, string>();

            foreach (var line in lines)
            {
                var items = line.Split('=');

                dict.Add(items[0], items[1]);
            }

            this.Data = dict;
        }
    }
}