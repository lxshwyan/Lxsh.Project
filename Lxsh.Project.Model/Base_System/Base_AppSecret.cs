using System;

namespace Lxsh.Project.Model
{
    /// <summary>
    /// 应用密钥
    /// </summary>
   
    public class Base_AppSecret
    {     
        /// <summary>
        /// 代理主键
        /// </summary>   
        public String Id { get; set; }

        /// <summary>
        /// 应用Id
        /// </summary>
        public String AppId { get; set; }

        /// <summary>
        /// 应用密钥
        /// </summary>
        public String AppSecret { get; set; }

        /// <summary>
        /// 应用名
        /// </summary>
        public String AppName { get; set; }
    }
}