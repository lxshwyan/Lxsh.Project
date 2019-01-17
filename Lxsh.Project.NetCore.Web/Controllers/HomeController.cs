using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lxsh.Project.NetCore.Web.Models;
using Microsoft.Extensions.Options;

namespace Lxsh.Project.NetCore.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IOptionsSnapshot<Appconfig> appConfig)
        {
           var config = appConfig.Get("appconfig");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
