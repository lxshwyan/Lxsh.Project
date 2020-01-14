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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lxsh.Project.CsRedisNetDemo
{
   public class PubMsg
    {
       // public CSRedisClient csRedisClient;
        public PubMsg()
        {
           //csRedisClient = new CSRedis.CSRedisClient("192.168.137.112:6379,defaultDatabase=0,poolsize=50,ssl=false,writeBuffer=10240");
            RedisHelper.Initialization(new CSRedis.CSRedisClient("192.168.137.111:6379,pass=123,defaultDatabase=10,poolsize=50,ssl=false,writeBuffer=10240"));

        }
      
        public void Subscribe(string chanl, Action<CSRedis.CSRedisClient.SubscribeMessageEventArgs> action)
        {
            RedisHelper.Subscribe((chanl, action));  
        }
        public void Publish(string chanl, string msg)
        {
            RedisHelper.Publish(chanl, msg);
         
        }

        public CSRedisClientLock Lock(string key)
        {
            return RedisHelper.Lock(key, 10);
        }
        public  bool SetNx(string key, long time, double expireMS)
        {
            if (RedisHelper.SetNx(key, time))
            {
                if (expireMS > 0)
                    RedisHelper.Expire(key, TimeSpan.FromMilliseconds(expireMS));
                return true;
            }
            return false;
        }

        public  bool Remove(string key)
        {
            return RedisHelper.Del(key) > 0;
        }
    }
    public static class SerializeExtension
    {
        public static string ToJson(this object obj, bool ignoreNull = false)
        {
            string result;
            if (obj == null)
            {
                result = null;
            }
            else
            {
                result = JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings
                {
                    DateFormatString = "yyyy-MM-dd HH:mm:ss",
                    NullValueHandling = (ignoreNull ? NullValueHandling.Ignore : NullValueHandling.Include)
                });
            }
            return result;
        }
    }
}