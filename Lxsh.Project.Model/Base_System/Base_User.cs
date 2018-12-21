using SqlSugar;
using System;


namespace Lxsh.Project.Model
{
  
    /// <summary>
    /// ϵͳ���û���
    /// </summary>    
    public class Base_User
    { 
        public int Id { get; set; }

        /// <summary>
        /// �û�Id,�߼�����
        /// </summary>
        public String UserId { get; set; }

        /// <summary>
        /// �û���
        /// </summary>
        public String UserName { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public String Password { get; set; }

        /// <summary>
        /// ��ʵ����
        /// </summary>
        public String RealName { get; set; }

        /// <summary>
        /// �Ա�(1Ϊ�У�0ΪŮ)
        /// </summary>
        public Int32? Sex { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public DateTime? Birthday { get; set; }
       
       /// <summary>
       /// �˻�״̬
       /// </summary>
        public Int32 State { get; set; }
        /// <summary>
        ///  �ֻ�����
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// ����ID
        /// </summary>
        public string DepartmentID { get; set; }
    }
}