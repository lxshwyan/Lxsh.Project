using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lxsh.Project.DesignPattern__Component
{
    public class Leaf : AbstractComponent
    {
        public Leaf(string name) : base(name) { }
        public override void Add(AbstractComponent c)
        {
            Console.WriteLine("也节点不能加子节点");
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new string('-', depth)+name);
        }

        public override void Remove(AbstractComponent c)
        {
            Console.WriteLine("叶节点没有子节点");
        }
    }
}