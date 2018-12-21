
using Lxsh.Project.Common;
using Lxsh.Project.Model;  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.Bussiness.Interface
{
  public  interface IBase_SysLogService : IBaseService
    {
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
        List<Base_SysLog> GetLogList(string logContent, string logType, string opUserName, DateTime? startTime, DateTime? endTime, Pagination<Base_SysLog> pagination);   

    }
}
