using System;

namespace Lxsh.Project.Model
{
    /// <summary>
    /// ���ݿ�����
    /// </summary>    
    public class Base_DatabaseLink
    {

        /// <summary>
        /// ��������
        /// </summary>    
        public String Id { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        public String LinkName { get; set; }

        /// <summary>
        /// �����ַ���
        /// </summary>
        public String ConnectionStr { get; set; }

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        public String DbType { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        public String SortNum { get; set; }

    }
}