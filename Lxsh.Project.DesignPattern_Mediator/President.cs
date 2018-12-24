/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.DesignPattern_Mediator
*文件名： President
*创建人： Lxsh
*创建时间：2018/12/24 10:46:55
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/24 10:46:55
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
    public class President : Mediator
    {
        //总经理有各个部门的管理权限
        private Financial _financial;
        private Market _market;
        private Development _development;

        public void SetFinancial(Financial financial)
        {
            this._financial = financial;
        }
        public void SetDevelopment(Development development)
        {
            this._development = development;
        }
        public void SetMarket(Market market)
        {
            this._market = market;
        }

        public void Command(Department department)
        {
            if (department.GetType() == typeof(Market))
            {
                _financial.Process();
            }
        }
    }
}