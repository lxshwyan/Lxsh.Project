using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lxsh.Project.DesignPattern_Chain
{
    public class ConcreateHandleC : AbstractHander
    {
        public override void Request(ContentMsg content)
        {
            if (content.Day > 5)
            {
                Console.WriteLine("C无法处理");
                this.Hander?.Request(content);
            }
            else
            {
               
                Console.WriteLine("C处理完成流程结束");
            }

        }
    }
}