using Lxsh.Project.Common.Web.Filter;
using System.Web;
using System.Web.Mvc;

namespace Lxsh.Project.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LogExceptionFilter());
            filters.Add(new AuthorityFilterAttribute());
        }
    }
}
