using System;

namespace Lxsh.Project.Model
{
    /// <summary>
    /// ϵͳ��־��
    /// </summary>     
    public class Base_SysLog
    {

        /// <summary>
        /// ��������
        /// </summary>   
        public String Id { get; set; }

        /// <summary>
        /// ��־����
        /// </summary>
        public String LogType { get; set; }

        /// <summary>
        /// ��־����
        /// </summary>
        public String LogContent { get; set; }

        /// <summary>
        /// ����Ա�û���
        /// </summary>
        public String OpUserName { get; set; }

        /// <summary>
        /// ��־��¼ʱ��
        /// </summary>
        public DateTime? OpTime { get; set; }

    }
}