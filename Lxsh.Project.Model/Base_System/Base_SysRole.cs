using System;

namespace Lxsh.Project.Model
{
    /// <summary>
    /// 系统角色
    /// </summary>
    public class Base_SysRole
    {

        /// <summary>
        /// 代理主键
        /// </summary>    
        public String Id { get; set; }

        /// <summary>
        /// 逻辑主键，角色Id
        /// </summary>
        public String RoleId { get; set; }

        /// <summary>
        /// 角色名
        /// </summary>
        public String RoleName { get; set; }

    }
}