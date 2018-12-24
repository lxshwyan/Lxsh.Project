using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lxsh.Project.DesignPattern_State
{
    public class ConcreateStateC : State
    {
        public override void Handle(Context context)
        {
            Console.WriteLine("当前状态是 C.");
            context.State = new ConcreateStateA();
        }
    }
}