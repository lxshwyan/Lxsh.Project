using Lxsh.Project.Bussiness.Interface;
using Lxsh.Project.Common;
using Lxsh.Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lxsh.Project.Web.Controllers
{
    [AllowAnonymous]
    public class Base_SysLogController : Controller
    {
        public IBase_SysLogService _base_SysLogBusiness { get; set; }
        public IUserDepDepartmentService _UserDepDepartmentService { get; set; }
        public Base_SysLogController(IBase_SysLogService _base_SysLogBusiness, IUserDepDepartmentService _UserDepDepartmentService)
        {
            this._base_SysLogBusiness = _base_SysLogBusiness;
            this._UserDepDepartmentService = _UserDepDepartmentService;
            
        }
     
        // GET: Base_SysLog
        public ActionResult Index()
        {
            return View();
        }
        #region 获取数据

        /// <summary>
        /// 获取日志列表
        /// </summary>
        /// <param name="logContent">日志内容</param>
        /// <param name="logType">日志类型</param>
        /// <param name="opUserName">操作人用户名</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        public ActionResult GetLogList(
            string logContent,
            string logType,
            string opUserName,
            DateTime? startTime,
            DateTime? endTime,
            Pagination<Base_SysLog> pagination)
        {
            this._UserDepDepartmentService.getUserDepartmentInfoByUserID(opUserName);
            var dataList = _base_SysLogBusiness.GetLogList(logContent, logType, opUserName, startTime, endTime, pagination); 
            return Content(pagination.BuildTableResult_DataGrid(dataList).ToJson());
        }

        public ActionResult GetLogTypeList()
        {
            List<object> logTypeList = new List<object>();
            Enum.GetNames(typeof(EnumType.LogType)).ForEach(aName =>
            {
                logTypeList.Add(new { Name = aName, Value = aName });
            });

            return Content(logTypeList.ToJson());
        }

        #endregion
    }
}