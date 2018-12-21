using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.Bussiness.Interface
{
    public  interface IUserDepDepartmentService: IBaseService
    {   
        void getUserDepartmentInfoByUserID(string userID);
    }
}
