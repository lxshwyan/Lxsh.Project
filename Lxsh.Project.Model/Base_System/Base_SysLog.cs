using System;

namespace Lxsh.Project.Model
{
    /// <summary>
    /// 系统日志表
    /// </summary>     
    public class Base_SysLog
    {

        /// <summary>
        /// 代理主键
        /// </summary>   
        public String Id { get; set; }

        /// <summary>
        /// 日志类型
        /// </summary>
        public String LogType { get; set; }

        /// <summary>
        /// 日志内容
        /// </summary>
        public String LogContent { get; set; }

        /// <summary>
        /// 操作员用户名
        /// </summary>
        public String OpUserName { get; set; }

        /// <summary>
        /// 日志记录时间
        /// </summary>
        public DateTime? OpTime { get; set; }

    }
}