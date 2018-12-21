using Lxsh.Project.Model;  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.Bussiness.Interface
{
  public  interface IUserService: IBaseService
    {        
        /// <summary>
        ///  根据用户名称获取用户信息
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        Base_User getUserInfoByUserName(string UserName);
      
    }
}
