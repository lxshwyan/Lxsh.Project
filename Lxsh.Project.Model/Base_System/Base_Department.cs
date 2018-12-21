
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lxsh.Project.Model
{
  public  class Base_Department
    {
        //  [ID]
        //,[DepartmentID]
        //,[DepartmentName]
        //,[ParentID]
        //,[ForGate]
        //,[ForAttend]
        public int ID { get; set; }
        public string DepartmentID { get; set; }

        public string ParentID { get; set; }
        public string DepartmentName { get; set; }
        public string ForGate { get; set; }
        public string ForAttend { get; set; }

    }
}