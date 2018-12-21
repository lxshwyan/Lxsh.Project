using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.Model
{
    public enum UserState
    {
        Normal = 0,
        Frozen = 1,
        Deleted = 2
    }

    public enum UserType
    {
        User = 1,
        Admin = 2,
        SuperAdmin = 4
    }

    public enum CategoryState
    {
        Normal = 0,
        Frozen = 1,
        Deleted = 2
    }
}
