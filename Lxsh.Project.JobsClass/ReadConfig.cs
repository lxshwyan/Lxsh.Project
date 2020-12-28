using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.JobsClass
{
   public class ReadConfig
    {
        public static string GetConfig(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }
    }
}
