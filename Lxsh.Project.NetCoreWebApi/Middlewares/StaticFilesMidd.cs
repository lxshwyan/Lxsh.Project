using Microsoft.AspNetCore.Builder;
using System;
using System.IO;

namespace Lxsh.Project.NetCoreWebApi.Middlewares
{
    public static class StaticFilesMidd
    {
        public static void UseStaticFilesMidd(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));
            var fileProvider = Path.Combine(Directory.GetCurrentDirectory(), @"uploads");
            if (!System.IO.Directory.Exists(fileProvider))
            {
                System.IO.Directory.CreateDirectory(fileProvider);
            }
            _ = app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(fileProvider),
                RequestPath = new Microsoft.AspNetCore.Http.PathString("/uploads")
            });

        }
    }

}
