using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lxsh.Project.DesignPattern_Mediator
{
    public abstract class Department
    {
         //持有中介者(总经理)的引用
        private Mediator mediator;

        protected Department(Mediator mediator)
        {
            this.mediator = mediator;
        }

        public Mediator GetMediator
        {
            get { return mediator; }
            private set { this.mediator = value; }
        }

        //做本部门的事情
        public abstract void Process();

        //向总经理发出申请
        public abstract void Apply();
       
    }
}