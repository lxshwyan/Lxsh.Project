using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lxsh.Project.SSOClientDemo.Controllers
{
    public class AccountController : Controller
    {
        private string LogOutUrl = ConfigurationManager.AppSettings["LogOutUrl"];

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout(string returnUrl)
        {
            HttpContext.Request.Cookies.Clear();
            return Redirect($"{LogOutUrl}?ReturnUrl={ returnUrl}");
        }
    }
}