using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;

namespace Lxsh.Project.CoreAPI.Extensions
{
    [ApiController]
    public class ApiControllerBase
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        protected string UserInfo
        {
            get
            {
               // var routeData = HttpContext.GetRouteData();
                //var data = new JwtSecurityTokenHandler().ReadJwtToken();
                return "";
            }
        }
    }
}
