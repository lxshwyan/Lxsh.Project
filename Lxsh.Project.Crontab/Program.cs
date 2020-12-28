using Lxsh.Project.JobsClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.Crontab
{
    class Program
    {
        static void Main(string[] args)
        {
            string SignalRURI = "http://192.168.137.110:6178";
            SignalRURI = ReadConfig.GetConfig(SignalRURI)?? SignalRURI;
            SignalRServer.Start(SignalRURI);
            var task = new TaskFeFactory();
            task.CreateFactoryObj();
            Console.ReadLine();
        }
    }
}
