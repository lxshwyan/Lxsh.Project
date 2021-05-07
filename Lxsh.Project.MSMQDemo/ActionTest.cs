using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.MSMQDemo
{
    public class ActionTest
    {
        Action<string> action;
        int i = 0;
        public ActionTest test(Action<string> _action)
        {
            action = _action;
            action("初始化");
            return this;
        }
        public ActionTest test()
        {
            action($"正常打印{i++}"+ Environment.NewLine);
            return this;
        }
    }
}
