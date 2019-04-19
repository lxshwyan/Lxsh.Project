using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lxsh.Project.MVCEF.Demo.Models
{
    public class Donator
    {
        public int DonatorId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime DonateDate { get; set; }
    }
}