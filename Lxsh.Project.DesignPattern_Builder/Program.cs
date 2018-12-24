using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.DesignPattern_Builder
{
    class Program
    {
        static void Main(string[] args)
        {
            BuildlerDirector buildler = new BuildlerDirector();
            Console.WriteLine("===============开始创建胖人=====================");
            buildler.SetPerson(new FatPerson());
            buildler.CreatePerson();
            Console.WriteLine("===============开始创建瘦人=====================");

            buildler.SetPerson(new ThinPerson());
            buildler.CreatePerson();
        }
    }
}
