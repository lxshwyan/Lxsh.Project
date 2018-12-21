using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Entitys
{
    public class JobRequestEntity
    {
        /// <summary>
        /// Job需要执行的Action
        /// </summary>
        public string JobFullClass { get; set; }

        /// <summary>
        /// job的名字
        /// </summary>
        public string JobName { get; set; }

        /// <summary>
        /// job的组名称
        /// </summary>
        public string JobGroupName { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// 是否编辑
        /// </summary>
        public bool IsEdit { get; set; }
    }
}