using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lxsh.Project.NetCoreWebApi.Policy
{
    public class PermissionRequirement: IAuthorizationRequirement
    {
        public string _permissionName { get; }

        public PermissionRequirement(string PermissionName)
        {
            _permissionName = PermissionName;
        }
    }
}
