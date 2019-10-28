using Lxsh.Project.ConsoleDemo.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Lxsh.Project.ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            // TestRegex._instance.RegexWrite();
            //  TestSQLite._instance.Insert();
            // TestTextReader._instance.Reader();

            Console.WriteLine(TestRandoPort._instance.PortIsUsed().Count); ;
           
        }
    }
}
