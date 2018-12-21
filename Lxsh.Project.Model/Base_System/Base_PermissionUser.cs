using System;


namespace Lxsh.Project.Model
{
    /// <summary>
    /// 用户权限表
    /// </summary>               
    public class Base_PermissionUser
    {

        /// <summary>
        /// 代理主键
        /// </summary>      
        public String Id { get; set; }

        /// <summary>
        /// 用户主键Id
        /// </summary>
        public String UserId { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public String PermissionValue { get; set; }

    }
}