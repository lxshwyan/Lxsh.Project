/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lxsh.Project.Dapper.Demo
*文件名： Test
*创建人： Lxsh
*创建时间：2019/1/4 16:57:03
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/4 16:57:03
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.Dapper.Demo
{
   public class Test
    {
        public  string str { get; set; }
        public string MyProperty { get; set; }
        public void Say()
        {
          
        }
    }

    public class SonTest:Test
    {
        public  string Name { get; set; }    
        public void Say()
        {
           
            base.Say();
        }
       
    }
}