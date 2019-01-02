/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.WcfServiceLib
*文件名： EventAlarmInfo
*创建人： Lxsh
*创建时间：2018/12/29 10:27:56
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/29 10:27:56
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
   
    public class EventAlarmInfo : IEventAlarm
    {
        public static Dictionary<string, ILxshCallBack> channelDic = new Dictionary<string, ILxshCallBack>();
        public void Login(string username)
        {
            //获取当前client的对象 channel
            var callback = OperationContext.Current.GetCallbackChannel<ILxshCallBack>();
            
            channelDic[username] = callback;
            Console.WriteLine("当前username={0} 已登录", username);
        }
        public static void Modify()
        {
            while (true)
            {
                var input = Console.ReadLine();

                if (!string.IsNullOrEmpty(input))
                {
                    foreach (var item in channelDic)
                    {
                        item.Value.Notify1(input);
                        item.Value.Notify2(input);
                    }
                }
            }
        }
    }
}