using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lxsh.Project.NetCoreWebApi.Controllers
{
    [Authorize(Policy = "customizePermisson")]
    [ApiController]
    [Route("api/[Controller]/[action]")]
    public class BaseController : Controller
    {
    }
}
