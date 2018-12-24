using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lxsh.Project.DesignPattern_Command
{
    public class TvCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("打开电视");
        }

        public void Undo()
        {
            Console.WriteLine("关闭电视");
        }
    }
}