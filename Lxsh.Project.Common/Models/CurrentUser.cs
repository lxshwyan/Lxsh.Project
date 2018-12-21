
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace Lxsh.Project.Common
{
  public  class CurrentUser
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public string Account { get; set; }
        public string Password  { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }
        public DateTime LoginTime { get; set; }
    }
}