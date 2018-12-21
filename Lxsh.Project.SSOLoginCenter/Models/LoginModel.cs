﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lxsh.Project.SSOLoginCenter.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public string verify { get; set; } = "noVerify";
    }
}