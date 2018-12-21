using System; 

namespace Lxsh.Project.Model
{
    /// <summary>
    /// AppId权限表
    /// </summary>   
    public class Base_PermissionAppId
    {

        /// <summary>
        /// 代理主键
        /// </summary>   
        public String Id { get; set; }

        /// <summary>
        /// AppId
        /// </summary>
        public String AppId { get; set; }

        /// <summary>
        /// 权限值
        /// </summary>
        public String PermissionValue { get; set; }

    }
}