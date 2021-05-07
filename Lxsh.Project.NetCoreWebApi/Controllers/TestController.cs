using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <summary>
        /// 获取姓名
        /// </summary>
        /// <param name="id"></param>
        /// <param name="201">返回值为空</param>
        /// <param name="400">如果ID为空</param>
        /// <returns></returns>
        [HttpGet]
        public string GetName(int id)
        {
            return id.ToString();
        }
    }
}
