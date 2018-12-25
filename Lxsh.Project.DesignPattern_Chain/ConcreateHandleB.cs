using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lxsh.Project.DesignPattern_Chain
{
    public class ConcreateHandleB : AbstractHander
    {
        public override void Request(ContentMsg content)
        {
            if (content.Day > 3)
            {
                Console.WriteLine("B无法处理");
                this.Hander?.Request(content);
            }
            else
            {
               
                Console.WriteLine("B处理完成流程结束");
            }

        }
    }
}