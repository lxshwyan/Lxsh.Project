using System.Web;
using System.Web.Mvc;

namespace Lxsh.Project.SSOLoginCenter
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
