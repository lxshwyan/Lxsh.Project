using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lxsh.Project.NetCore.Web
{
    public static class LuckyCoreFileExtensions
    {
        public static IApplicationBuilder LuckyCoreFile(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException("app");
            }

            return UseMiddlewareExtensions.UseMiddleware<LuckyCoreFileMiddleware>(app, Array.Empty<object>());
        }
    }
}
