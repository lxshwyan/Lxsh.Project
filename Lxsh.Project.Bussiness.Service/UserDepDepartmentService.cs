using Lxsh.Project.Bussiness.Interface;
using Lxsh.Project.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.Bussiness.Service
{
    public class UserDepDepartmentService : BaseService, IUserDepDepartmentService
    {
       
        #region Identity
        protected SqlSugarClient dbSqlClent = CreateDBInstance.GetInstance();
        /// <summary>
        /// 
        /// </summary>
        public UserDepDepartmentService()
        {
            base.baseDbSqlClent = dbSqlClent;
        }
        #endregion Identity
        public void getUserDepartmentInfoByUserID(string userID)
        {
            var list5 = dbSqlClent.Queryable<Base_User, Base_Department>((st, sc) => st.DepartmentID == sc.DepartmentID&&st.UserId== userID).Select((st, sc) => new { st, sc.DepartmentName })   
                .ToList();
        }
    }
}
