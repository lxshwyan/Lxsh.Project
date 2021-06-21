using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lxsh.Project.NetCoreWebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft;
using Newtonsoft.Json;

namespace ToolGood.Words.Sample.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MinganController : ControllerBase
    {
        [HttpGet]
        public JsonResult Check([FromQuery]MinganCheckInput input)
        {
            return new JsonResult(input);
        }
        
        [HttpGet]
        public string Replace([FromQuery]MinganReplaceInput input)
        {
            return JsonConvert.SerializeObject(input);
        }
    }
}