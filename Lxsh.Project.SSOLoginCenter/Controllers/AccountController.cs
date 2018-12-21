using Lxsh.Project.Bussiness.Interface;
using Lxsh.Project.Common;
using Lxsh.Project.Common.Log;
using Lxsh.Project.SSOLoginCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Lxsh.Project.SSOLoginCenter.Controllers
{
    public class AccountController : Controller
    {
        private static Logger logger = Logger.CreateLogger(typeof(AccountController));
        public IUserService UserService { get; set; }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }    
        public ActionResult Login(string returnUrl=null)
        {    
            logger.Debug($"未登录的跳转：{returnUrl}");
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.ParamName = Constants.CookieName;
            return View();
        }
        [HttpPost]
        public JsonResult Login(LoginModel modle)
        {
            var result = new LoginResult();
            var url = string.Empty;
            if (modle.verify == "noVerify") //不检查验证码
            {
                this.HttpContext.Session["CheckCode"] = modle.verify;
            }
            UserManage.LoginResult loginResult = this.HttpContext.UserLogin(modle.UserName, modle.Password, modle.verify, UserService);
            if (loginResult==UserManage.LoginResult.Success)
            {
              FormsAuthentication.SetAuthCookie(modle.UserName,false);
                //创造票据
                FormsAuthenticationTicket ticket=new FormsAuthenticationTicket (modle.UserName,false, Constants.ExpiresDay);
                string ticString = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticString)
                {
                    Name = Constants.CookieName,
                    HttpOnly = true,
                    Secure = FormsAuthentication.RequireSSL,
                    Domain = FormsAuthentication.CookieDomain,
                    Path = FormsAuthentication.FormsCookiePath,
                    Expires = DateTime.Now.AddDays(Constants.ExpiresDay)
                };
                //写入Cookie
                Response.Cookies.Remove(cookie.Name);
                Response.Cookies.Add(cookie);    
                if (string.IsNullOrEmpty(modle.ReturnUrl))
                    url = "/";
                else
                    url = modle.ReturnUrl;
                //将登录信息写入缓存
                var info = Constants.ICacheManager.Get(modle.UserName);
                if (info == null || string.IsNullOrEmpty(info.ToString()))
                    Constants.ICacheManager.Add(modle.UserName, ticString);
                else
                    Constants.ICacheManager.Update(modle.UserName, a => ticString);

                result.IsSucceed = true;
                result.Ticket = ticString;
                result.UserInfo = modle.UserName;
                result.ReturnUrl = url;
            }
            else
                result.ErrorMsg = loginResult.GetRemark(); 
            //登录失败，返回错误信息
            return Json(result);
        }

        /// <summary>
        /// 分站未登录时首先重定向到此页
        /// </summary>
        [HttpGet]
        public ActionResult SSOLogin(string returnUrl = null)
        {
            //若已登录后直接访问 则重定向到登录界面
            if (string.IsNullOrEmpty(returnUrl))
                return RedirectToAction("Login", "Account");
            //若已登录需要跳转，则跳转到原页面并附带上Ticket和UserName
            var cookie = Request.Cookies[Constants.CookieName];
            var url = returnUrl.AddParam(Constants.CookieName, cookie.Value);
            url = url.AddParam(Constants.UserName, User.Identity.Name);
            logger.Info($"已验证的跳转:{returnUrl}  将要跳转的Url：{url}");
            return Redirect(url);
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout(string returnUrl = null)
        {
            //系统退出
            FormsAuthentication.SignOut();
            //清除Cookie
            HttpContext.Request.Cookies.Clear();
            //清空本用户的登录缓存
            Constants.ICacheManager.Remove(User.Identity.Name);
            if (string.IsNullOrEmpty(returnUrl))
                return Redirect(FormsAuthentication.LoginUrl);
            return Redirect($"{FormsAuthentication.LoginUrl}?ReturnUrl={returnUrl}");
        }
    }
}