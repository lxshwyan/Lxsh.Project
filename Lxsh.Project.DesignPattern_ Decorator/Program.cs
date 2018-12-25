using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.DesignPattern__Decorator
{
    class Program
    {
        static void Main(string[] args)
        {
          
            ApplePhone applePhone = new ApplePhone();
            Decrator decratora = new DecratorA();
            Decrator decratorB = new DecratorB();
            Decrator decratorC = new DecratorC();

            decratora.SetDecrator(applePhone);  

            decratorB.SetDecrator(decratora);

            decratorC.SetDecrator(decratorB);

            decratorC.Show();
        }
    }
}
