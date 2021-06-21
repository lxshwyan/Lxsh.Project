using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Lxsh.Project.EventBusDemo
{
  public  class EvOnLoginSuccess : IBusEvent
    {
        public EvOnLoginSuccess(LoginResultDto info)
        {
            this.Info = info;

        }
        public LoginResultDto Info { get; set; }
    }

  
    public class LoginResultDto 
    {
        [Description("用户类别")]
        public string UserType { get; set; }

        [Description("用户主键")]
        public string Id { get; set; }

        [Description("是否成功登录")]
        public bool Success { get; set; } = false;
    }
}
