using Lxsh.Project.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Lxsh.Project.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {    
            if (this.HttpContext.Session["CurrentUrl"] != null)
            {    
                string url = this.HttpContext.Session["CurrentUrl"].ToString();
                this.HttpContext.Session["CurrentUrl"] = null;
                return Redirect(url);
            }
            return View(((CurrentUser)HttpContext.Session["CurrentUser"]));
         
        }
       
    }
}