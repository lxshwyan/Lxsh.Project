using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lxsh.Project.SSOLoginCenter
{
    /// <summary>
    /// 验证接口
    /// </summary>
    public class ValidateController : ApiController
    {
        /// <summary>
        /// 验证当前是否登录
        /// </summary>
        [HttpGet]
        public bool ValidateTicket(string userName, string userAuthTicket)
        {
            if (string.IsNullOrEmpty(userName)) return false;
            if (string.IsNullOrEmpty(userAuthTicket)) return false;

            var info = Constants.ICacheManager.Get(userName);
            if (info == null || string.IsNullOrEmpty(info.ToString())) return false;
            if (!info.ToString().Equals(userAuthTicket)) return false; 
            return true;
        }
    }
}