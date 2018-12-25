using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lxsh.Project.DesignPattern__Decorator
{
    public  class Decrator : Phone
    {
        Phone phone;
        public  void SetDecrator(Phone phone)
        { 
            this.phone = phone;
        }
        public override void Show()
        {
            phone?.Show();
        }
    }
}