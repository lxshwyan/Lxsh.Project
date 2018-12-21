using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.DesignPattern_Proxy
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseProxy databaseProxy = new DatabaseProxy();
            databaseProxy.Add();
            databaseProxy.Remove();
        }
    }
}
