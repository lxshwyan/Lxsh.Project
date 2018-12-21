using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lxsh.Project.Model;
using SqlSugar;
using Lxsh.Project.Bussiness.Interface;
using System.Linq.Expressions;

namespace Lxsh.Project.Bussiness.Service
{
   public  class UserService:BaseService, IUserService
    {
        #region Identity
        protected SqlSugarClient dbSqlClent = CreateDBInstance.GetInstance();
        /// <summary>
        /// 
        /// </summary>
        public UserService()
        {
            base.baseDbSqlClent = dbSqlClent;
        }
        #endregion Identity
        /// <summary>
        ///  根据用户名获取用户信息 
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public Base_User getUserInfoByUserName(string UserName)
        {   
            Base_User getSingle = dbSqlClent.Queryable<Base_User>().Where(model => model.UserId == UserName).Single();  
            return getSingle;
        }
    }
}
