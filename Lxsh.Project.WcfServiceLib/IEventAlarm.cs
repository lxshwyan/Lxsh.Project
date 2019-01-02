/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.WcfServiceLib
*文件名： EventAlarm
*创建人： Lxsh
*创建时间：2018/12/29 10:27:23
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/29 10:27:23
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.WcfServiceLib
{
    [ServiceContract(CallbackContract =(typeof(ILxshCallBack)))]
    public interface IEventAlarm
    {
        [OperationContract]
        void Login(string username);
    }
    public interface ILxshCallBack
    {
        [OperationContract]
        void Notify1(string msg);

        [OperationContract]
        void Notify2(string msg);
    }
}