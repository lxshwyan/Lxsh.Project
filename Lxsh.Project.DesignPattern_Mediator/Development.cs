/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.DesignPattern_Mediator
*文件名： Development
*创建人： Lxsh
*创建时间：2018/12/24 10:51:55
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/24 10:51:55
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.DesignPattern_Mediator
{
    //开发部门
    public class Development : Department
    {
        public Development(Mediator m) : base(m) { }

        public override void Process()
        {
            Console.WriteLine("我们是开发部门，要进行项目开发，没钱了，需要资金支持！");
        }

        public override void Apply()
        {
            Console.WriteLine("专心科研，开发项目！");
        }
    }
}