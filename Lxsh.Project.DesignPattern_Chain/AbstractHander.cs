using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lxsh.Project.DesignPattern_Chain
{
    public abstract class AbstractHander
    {
        public AbstractHander Hander ; 
        public abstract void Request(ContentMsg content);
        
    }
}