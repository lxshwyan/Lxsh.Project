using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lxsh.Project.SSOLoginCenter.Models
{
    public class LoginResult
    {
        /// <summary>
        /// 登录是否成功
        /// </summary>
        public bool IsSucceed { get; set; }
        /// <summary>
        /// 登录用户信息
        /// </summary>
        public string UserInfo { get; set; }
        /// <summary>
        /// 登录的票据
        /// </summary>
        public string Ticket { get; set; }
        /// <summary>
        /// 需要跳转的Url
        /// </summary>
        public string ReturnUrl { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg { get; set; }
    }
}