using Lxsh.Project.Bussiness.Interface;
using Lxsh.Project.Common;
using Lxsh.Project.Common.ImageHelper;
using Lxsh.Project.Common.Web.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Lxsh.Project.Web.Controllers
{
    [AllowAnonymousAttribute]
    public class LoginController : Controller
    {
        public LoginController(IBase_SysLogService _base_SysLogBusiness,
            IUserDepDepartmentService _UserDepDepartmentService)
        {
            this._base_SysLogBusiness = _base_SysLogBusiness;
            this._UserDepDepartmentService = _UserDepDepartmentService;
        }

        public IBase_SysLogService _base_SysLogBusiness { get; set; }
        public IUserDepDepartmentService _UserDepDepartmentService { get; set; }
        public ActionResult index()
        {
          
           return View();
        }                          
        public ActionResult SubmitLogin(string userName, string password, string verify)
        {
             if (verify== "noVerify") //不检查验证码
             {
                 this.HttpContext.Session["CheckCode"] = verify;
             }      
            UserManage.LoginResult result = this.HttpContext.UserLogin(userName, password, verify);
            if (result == UserManage.LoginResult.Success)
            {
                return Content(new AjaxResult() { Result = DoResult.Success, DebugMessage = result.GetRemark() }.ToJson());   
            }
            else
            {     
                return Content(new AjaxResult() { Result = DoResult.Failed, DebugMessage = result.GetRemark() }.ToJson());
            }
        }

        /// <summary>
        /// 验证码 FileContentResult
        /// </summary>
        /// <returns></returns>
        public ActionResult VerifyCode()
        {
            string code = "";
            Bitmap bitmap = VerifyCodeHelper.CreateVerifyCode(out code);
            base.HttpContext.Session["CheckCode"] = code;
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Gif);
            return File(stream.ToArray(), "image/gif");//返回FileContentResult图片
        }
        /// <summary>
        /// 验证码  直接写入Response
        /// </summary>
        public void Verify()
        {
            string code = "";
            Bitmap bitmap = VerifyCodeHelper.CreateVerifyCode(out code);
            base.HttpContext.Session["CheckCode"] = code;
            bitmap.Save(base.Response.OutputStream, ImageFormat.Gif);
            base.Response.ContentType = "image/gif";
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>   
        public ActionResult Logout()
        {
            this.HttpContext.UserLogout();
            return RedirectToAction("Index", "Home"); ;
        }
    }
}