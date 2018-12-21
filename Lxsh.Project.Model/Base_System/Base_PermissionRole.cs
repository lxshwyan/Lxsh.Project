using System;     
namespace Lxsh.Project.Model
{
    /// <summary>
    /// 角色权限表
    /// </summary>  
    public class Base_PermissionRole
    {

        /// <summary>
        /// 逻辑主键
        /// </summary>     
        public String Id { get; set; }

        /// <summary>
        /// 角色主键Id
        /// </summary>
        public String RoleId { get; set; }

        /// <summary>
        /// 权限值
        /// </summary>
        public String PermissionValue { get; set; }

    }
}