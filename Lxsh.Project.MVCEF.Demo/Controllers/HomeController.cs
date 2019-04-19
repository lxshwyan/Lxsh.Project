using Lxsh.Project.MVCEF.Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityFramework;
using EntityFramework.Extensions;

namespace Lxsh.Project.MVCEF.Demo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            AppDBContext context = new AppDBContext();
            //for (int i = 0; i < 1000; i++)
            //{
            //    context.Donators.Add(new Donator() { DonatorId = 1, Amount = 1, DonateDate = DateTime.Now, Name = "lxsh" });
            //}

            //List<Donator> donator = context.Donators.Where(d => d.DonatorId == 1).ToList();
            //foreach (var VARIABLE in donator)
            //{
            //    VARIABLE.Name = "fsfdsfds";
            //}

            context.Donators.Where(d => d.Name == "test").Update(r =>new Donator(){ Name = "tes1t"});
           

           // context.SaveChanges();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}