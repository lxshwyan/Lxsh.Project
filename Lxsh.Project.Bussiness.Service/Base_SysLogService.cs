using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lxsh.Project.Model;
using SqlSugar;
using Lxsh.Project.Bussiness.Interface;
using System.Linq.Expressions;
using Lxsh.Project.Common;
using Lxsh.Project.Common.ExtendExpression;

namespace Lxsh.Project.Bussiness.Service
{
   public  class Base_SysLogService : BaseService, IBase_SysLogService
    {
        #region Identity
        protected SqlSugarClient dbSqlClent = CreateDBInstance.GetInstance();
        /// <summary>
        /// 
        /// </summary>
        public Base_SysLogService()
        {
            base.baseDbSqlClent = dbSqlClent;
        }
        #endregion Identity
        /// <summary>
        /// 获取系统日志分页信息
        /// </summary>
        /// <param name="logContent"></param>
        /// <param name="logType"></param>
        /// <param name="opUserName"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public List<Base_SysLog> GetLogList(string logContent, string logType, string opUserName, DateTime? startTime, DateTime? endTime, Pagination<Base_SysLog> pagination)
        {
            var whereExp = LinqHelper.True<Base_SysLog>();
            if (!logContent.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.LogContent.Contains(logContent));
            if (!logType.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.LogType == logType);
            if (!opUserName.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.OpUserName.Contains(opUserName));
            if (!startTime.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.OpTime >= startTime);
            if (!endTime.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.OpTime <= endTime);
            //  List<Base_SysLog> List = dbSqlClent.Queryable<Base_SysLog>()
            //                    .Where(whereExp).ToDataTablePage(pagination.page, pagination.rows); 
            //    pagination.records = source.Count();
            //  source = source.OrderBy($"{pagination.sidx} {pagination.sord}");
            var total = 0;      
            var List = dbSqlClent.Queryable<Base_SysLog>()
                              .Where(whereExp).OrderBy(st => pagination.sidx, (SqlSugar.OrderByType)System.Enum.Parse(typeof(SqlSugar.OrderByType), pagination.sord.ToString()))
                              .ToPageList(pagination.PageIndex, pagination.PageRows, ref total);   
              pagination.records = total;           
            return List;
        }
    }
}
