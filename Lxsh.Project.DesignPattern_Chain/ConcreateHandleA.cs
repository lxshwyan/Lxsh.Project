using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lxsh.Project.DesignPattern_Chain
{
    public class ConcreateHandleA : AbstractHander
    {

        public override void Request(ContentMsg content)
        {
            if (content.Day > 1)
            {
                Console.WriteLine("A无法处理");
                this.Hander?.Request(content);
            }
            else
            {
            
                Console.WriteLine("A处理完成流程结束");
            }

        }
    }

}