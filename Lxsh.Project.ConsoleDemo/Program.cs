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
            TestType();
            // TestRegex._instance.RegexWrite();
            //  TestSQLite._instance.Insert();
            // TestTextReader._instance.Reader(); 
            //Console.WriteLine(TestRandoPort._instance.PortIsUsed().Count); ;
            //  TestYiled._instance.TestYieldMethod();
            // TestYiled._instance.TestMethod();     

           // TestMemoryStream._instance.TestStream();
        }
        private static void TestType()
        {
            string TypenName = System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName;
            TypenName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            Type type = typeof(Program);
            Console.WriteLine(TypenName);

            TestRandoPort obj = Activator.CreateInstance(typeof(TestRandoPort)) as TestRandoPort;
            Console.WriteLine(obj.PortIsUsed().Count);
        }
    }
}
