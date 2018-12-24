using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lxsh.Project.DesignPattern_Command
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}