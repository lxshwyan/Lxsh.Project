using System;
using System.Configuration;

namespace Lxsh.Project.Common
{
    /// <summary>
    /// 缓存帮助类
    /// </summary>
    public class CacheHelper
    {
        /// <summary>
        /// 静态构造函数，初始化缓存类型
        /// </summary>
        static CacheHelper( )
        {         
            string OpenRedisCache = ConfigurationManager.AppSettings["OpenRedisCache"]; 
            if (!string.IsNullOrEmpty(OpenRedisCache)&&(OpenRedisCache.ToUpper() == "TRUE"))
            {
                string RedisConfig = ConfigurationManager.AppSettings["RedisConfig"];
                if (string.IsNullOrEmpty(RedisConfig))
                {
                    throw new Exception("未配置Redis缓存连接！");
                }   
                RedisCache = new RedisCache(RedisConfig);
            }   
            SystemCache = new SystemCache();  
            string CacheType = ConfigurationManager.AppSettings["CacheType"];
            if (string.IsNullOrEmpty(OpenRedisCache))
            {
                throw new Exception("未配置缓存类型！");
            }
            switch (CacheType)
            {
                case "SystemCach": Cache = SystemCache;break;
                case "RedisCache": Cache = RedisCache;break;
                default:throw new Exception("请指定缓存类型！");
            }
        }

        /// <summary>
        /// 默认缓存
        /// </summary>
        public static ICache Cache { get; }

        /// <summary>
        /// 系统缓存
        /// </summary>
        public static ICache SystemCache { get; }

        /// <summary>
        /// Redis缓存
        /// </summary>
        public static ICache RedisCache { get; }
    }
    /// <summary>
    /// 默认缓存类型
    /// </summary>
    public enum CacheType
    {
        /// <summary>
        /// 系统缓存
        /// </summary>
        SystemCache,

        /// <summary>
        /// Redis缓存
        /// </summary>
        RedisCache
    }
}
