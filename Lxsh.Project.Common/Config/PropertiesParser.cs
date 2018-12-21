/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.Common.Config
*文件名： PropertiesParser
*创建人： Lxsh
*创建时间：2018/12/19 10:20:50
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/19 10:20:50
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.Common.Config
{
   public class PropertiesParser
    {
        /// <summary>
        /// Reads the properties from file system.
        /// </summary>
        /// <param name="fileName">The file name to read resources from.</param>
        /// <returns></returns>
        public static Dictionary<string, string> ReadFromFileResource(string fileName)
        {
            return ReadFromStream(File.OpenRead(fileName));
        }
        public static Dictionary<string, string> ReadFromStream(Stream stream)
        {
            Dictionary<string, string> props = new Dictionary<string, string>(); 
            using (StreamReader sr = new StreamReader(stream, Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    line = line.TrimStart();    
                    if (line.StartsWith("#"))
                    {
                        // comment line 
                        continue;
                    }
                    if (line.StartsWith("!END"))
                    {
                        // special end condition
                        break;
                    }
                    string[] lineItems = line.Split(new char[] { '=' }, 2);
                    if (lineItems.Length == 2)
                    {
                        props[lineItems[0].Trim()] = lineItems[1].Trim();
                    }
                }
            }
            return props;
        }

    }
}