using System;     
namespace Lxsh.Project.Model
{
    /// <summary>
    /// ��ɫȨ�ޱ�
    /// </summary>  
    public class Base_PermissionRole
    {

        /// <summary>
        /// �߼�����
        /// </summary>     
        public String Id { get; set; }

        /// <summary>
        /// ��ɫ����Id
        /// </summary>
        public String RoleId { get; set; }

        /// <summary>
        /// Ȩ��ֵ
        /// </summary>
        public String PermissionValue { get; set; }

    }
}