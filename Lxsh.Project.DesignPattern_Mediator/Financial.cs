/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.DesignPattern_Mediator
*文件名： Financial
*创建人： Lxsh
*创建时间：2018/12/24 10:52:47
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/24 10:52:47
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
//财务部门
   public sealed class Financial : Department
   {
       public Financial(Mediator m) : base(m) { }

       public override void Process()
       {
           Console.WriteLine("汇报工作！没钱了，钱太多了！怎么花?");
       }

       public override void Apply()
       {
           Console.WriteLine("数钱！");
       }
   }
}