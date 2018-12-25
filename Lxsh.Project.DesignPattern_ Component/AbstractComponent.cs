using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lxsh.Project.DesignPattern__Component
{
    public abstract class AbstractComponent
    {
        public string name;

        public AbstractComponent(string name)
        {
            this.name = name;
        }
        public abstract void Add(AbstractComponent c);


        public abstract void Remove(AbstractComponent c);



        public abstract void Display(int depth);
       
    }
}