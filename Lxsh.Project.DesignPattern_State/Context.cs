using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lxsh.Project.DesignPattern_State
{
    public class Context
    {
         private State state;
        /// <summary>
        /// 定义Context的初始状态
        /// </summary>
        /// <param name="state"></param>
        public Context(State state)
        {
            this.state = state;
        }

        /// <summary>
        /// 可读写的状态属性，用于读取和设置新状态
        /// </summary>
        public State State
        {
            get { return state; }
            set { state = value; }
        }

        /// <summary>
        /// 对请求做处理，并设置下一个状态
        /// </summary>
        public void Request()
        {
            state.Handle(this);
        }
    }
}