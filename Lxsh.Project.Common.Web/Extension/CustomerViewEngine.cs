using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lxsh.Project.Common.Web.Extension
{
    /// <summary>
    /// razor视图引擎扩展
    /// </summary>
    public class CustomerViewEngine : RazorViewEngine
    {
        /// <summary>
        /// 可以分开部署不同语种
        /// </summary>
        /// <param name="engineName"></param>
        public CustomerViewEngine(string engineName)
        {
            base.ViewLocationFormats = new[]
                {
                    "~/Views" + engineName + "/{1}/{0}.cshtml",
                    "~/Views" + engineName + "/Shared/{0}.cshtml"
                };

            base.PartialViewLocationFormats = new[]
                {
                    "~/Views" + engineName + "/{1}/{0}.cshtml",
                    "~/Views" + engineName + "/Shared/{0}.cshtml"
                };

            base.AreaViewLocationFormats = new[]
                {
                    "~Areas/{2}/Views" + engineName + "/{1}/{0}.cshtml",
                    "~Areas/{2}/Views" + engineName + "/Shared/{0}.cshtml"
                };

            base.AreaPartialViewLocationFormats = new[]
                {
                    "~Areas/{2}/Views" + engineName + "/{1}/{0}.cshtml",
                    "~Areas/{2}/Views" + engineName + "/Shared/{0}.cshtml"
                };
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            this.SetEngine(controllerContext);
            return base.FindView(controllerContext, viewName, masterName, useCache);
        }

        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            this.SetEngine(controllerContext);
            return base.FindPartialView(controllerContext, partialViewName, useCache);
        }

        /// <summary>
        /// 根据条件自行设置,目前是chrome浏览器就展示默认的
        /// 不是chrome浏览器的话就展示/Themes/Eleven下的
        /// 可以直接测试是移动端还是pc端
        /// 然后写入cookie
        /// </summary>
        private void SetEngine(ControllerContext controllerContext)
        {
            string engineName = "/Themes/Eleven";
            if (controllerContext.HttpContext.Request.UserAgent.IndexOf("Chrome/65") >= 0)
            {
                engineName = null;
            }

            //if (controllerContext.HttpContext.Request.IsMobile())//检测是不是移动端
            //{
            //    engineName = null;
            //}

            base.ViewLocationFormats = new[]
               {
                    "~/Views" + engineName + "/{1}/{0}.cshtml",
                    "~/Views" + engineName + "/Shared/{0}.cshtml"
                };

            base.PartialViewLocationFormats = new[]
                {
                    "~/Views" + engineName + "/{1}/{0}.cshtml",
                    "~/Views" + engineName + "/Shared/{0}.cshtml"
                };

            base.AreaViewLocationFormats = new[]
                {
                    "~Areas/{2}/Views" + engineName + "/{1}/{0}.cshtml",
                    "~Areas/{2}/Views" + engineName + "/Shared/{0}.cshtml"
                };

            base.AreaPartialViewLocationFormats = new[]
                {
                    "~Areas/{2}/Views" + engineName + "/{1}/{0}.cshtml",
                    "~Areas/{2}/Views" + engineName + "/Shared/{0}.cshtml"
                };
        }
    }
}