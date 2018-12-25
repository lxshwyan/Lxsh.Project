using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lxsh.Project.DesignPattern__Decorator
{
    public class DecratorA : Decrator
    {
        public override void Show()
        {   
            base.Show();
            Console.WriteLine("装饰A");
        }
    }
}