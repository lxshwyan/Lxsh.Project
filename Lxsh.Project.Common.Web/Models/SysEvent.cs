using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lxsh.Project.Common.Web.Models
{
    /// <summary>
    /// 保存事件的几个属性
    /// </summary>
    public class SysEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TypeName { get; set; }
    }
}