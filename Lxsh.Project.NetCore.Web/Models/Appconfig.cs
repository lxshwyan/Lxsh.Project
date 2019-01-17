using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lxsh.Project.NetCore.Web.Models
{ 
    public class Appconfig
    {
        public Logging Logging { get; set; }
        public string AllowedHosts { get; set; }
    }

    public class Logging
    {
        public Loglevel LogLevel { get; set; }
    }

    public class Loglevel
    {
        public string Default { get; set; }
    }

}
