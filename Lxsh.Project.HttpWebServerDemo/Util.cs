using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class Util
    {
        public static string GetAppSetting(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }
    }
}
