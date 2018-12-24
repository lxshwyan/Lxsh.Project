using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lxsh.Project.DesignPattern_Command
{
    public class MiPhone
    {
        Queue<ICommand> commands;

        public MiPhone()
        {
            commands = new Queue<ICommand>();
        }

        public void setCommand(ICommand command)
        {
            commands.Enqueue(command);
        }

        public void onButtonWasPushed()
        {
            commands.Dequeue().Execute();
        }

    }
}