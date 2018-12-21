using SqlSugar;
using System;


namespace Lxsh.Project.Model
{
  
    /// <summary>
    /// 系统，用户表
    /// </summary>    
    public class Base_User
    { 
        public int Id { get; set; }

        /// <summary>
        /// 用户Id,逻辑主键
        /// </summary>
        public String UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public String UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public String Password { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public String RealName { get; set; }

        /// <summary>
        /// 性别(1为男，0为女)
        /// </summary>
        public Int32? Sex { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? Birthday { get; set; }
       
       /// <summary>
       /// 账户状态
       /// </summary>
        public Int32 State { get; set; }
        /// <summary>
        ///  手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public string DepartmentID { get; set; }
    }
}