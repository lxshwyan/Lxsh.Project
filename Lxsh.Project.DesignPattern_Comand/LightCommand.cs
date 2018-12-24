using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lxsh.Project.DesignPattern_Command
{
    public class LightCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("开灯");
        }

        public void Undo()
        {
            Console.WriteLine("关灯");
        }
    }
}