/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lxsh.Project.CsRedisDemo
*文件名： PubMsg
*创建人： Lxsh
*创建时间：2019/8/12 9:52:19
*描述
*=======================================================================
*修改标记
*修改时间：2019/8/12 9:52:19
*修改人：Lxsh
*描述：
************************************************************************/
using CSRedis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lxsh.Project.CsRedisDemo
{
   public class PubMsg
    {
        public CSRedisClient csRedisClient;
        public PubMsg()
        {
           csRedisClient = new CSRedis.CSRedisClient("127.0.0.1:6379,password=123,defaultDatabase=0,poolsize=50,ssl=false,writeBuffer=10240");
        }
      
        public void Subscribe(string chanl, Action<CSRedis.CSRedisClient.SubscribeMessageEventArgs> action)
        {
            csRedisClient.Subscribe((chanl, action));
        }
        public void Publish(string chanl, string msg)
        {
            csRedisClient.Publish(chanl, msg);
        }

        public CSRedisClientLock Lock(string key)
        {
            return csRedisClient.Lock(key, 10);
        }
        public  bool SetNx(string key, long time, double expireMS)
        {
            if (csRedisClient.SetNx(key, time))
            {
                if (expireMS > 0)
                    csRedisClient.Expire(key, TimeSpan.FromMilliseconds(expireMS));
                return true;
            }
            return false;
        }

        public  bool Remove(string key)
        {
            return csRedisClient.Del(key) > 0;
        }
    }
}