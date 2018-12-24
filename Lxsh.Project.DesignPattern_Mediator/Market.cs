/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.DesignPattern_Mediator
*文件名： Market
*创建人： Lxsh
*创建时间：2018/12/24 10:53:13
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/24 10:53:13
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
    /// <summary>
    /// 市场部门
    /// </summary>
    public sealed class Market : Department
   {
       public Market(Mediator mediator) : base(mediator) { }

       public override void Process()
        {
           Console.WriteLine("汇报工作！项目承接的进度，需要资金支持！");
           GetMediator.Command(this);
       }

       public override void Apply()
        {
           Console.WriteLine("跑去接项目！");
       }
   }
}