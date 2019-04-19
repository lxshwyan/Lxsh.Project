using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Lxsh.Project.MVCEF.Demo.Models
{
    public class AppDBContext: DbContext
    {
        public AppDBContext() : base("name=AppDBContext")
        {    
    
        }     

        public DbSet<Donator> Donators { get; set; }
    }
}