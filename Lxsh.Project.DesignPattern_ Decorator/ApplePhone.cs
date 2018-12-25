using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lxsh.Project.DesignPattern__Decorator
{
    public class ApplePhone : Phone
    {
        public override void Show()
        {
            Console.WriteLine("我是一个苹果手机");
        }
    }
}