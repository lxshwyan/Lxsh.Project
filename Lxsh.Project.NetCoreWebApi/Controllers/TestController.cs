using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lxsh.Project.NetCoreWebApi.Controllers
{
    
    /// <summary>
    /// 测试说明控制器
    /// </summary>
    public class TestController : BaseController
    {
     
        [HttpGet]
        public string GetUserID()
        {
            var name = Request.HttpContext.User.FindFirst(c => c.Type == "name");
            return name.ToString();
        }
    }
}
