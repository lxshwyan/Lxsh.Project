using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lxsh.Project.DesignPattern__Component
{
    public class Composite : AbstractComponent
    {
        private List<AbstractComponent> children = new List<AbstractComponent>();
        public Composite(string name) : base(name) { }
        public override void Add(AbstractComponent c)
        {
            this.children.Add(c);
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new string('-', depth) + name);
            foreach (AbstractComponent component in children)
            {
                component.Display(depth + 1);
            }
        }

        public override void Remove(AbstractComponent c)
        {
            this.children.Remove(c);
        }
    }
}