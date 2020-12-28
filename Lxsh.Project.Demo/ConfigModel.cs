using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.Demo
{
   public class ConfigModel
    {
        /// <summary>
        /// 平台进出判断（1进、2出、3进出）
        /// </summary>
        public int PlatInOut { get; set; } = 1;
        /// <summary>
        /// 1人行，2车型
        /// </summary>
        public string PersonOrCar { get; set; }
        /// <summary>
        /// 平台显示名称
        /// </summary>
        public string CTitleName { get; set; }
        /// <summary>
        /// 关押点名称
        /// </summary>
        public string AssertGroupID  { get; set; }
        /// <summary>
        /// 1为1-1对1绑定，2为1-多绑定
        /// </summary>
        public string CheckType { get; set; }
        /// <summary>
        /// ;;接收设备编号,接收多个设备数据时，用英文分号;分割
        /// </summary>
        public string RecDevID { get; set; }
        /// <summary>
        ///
        /// </summary>
        public string PlatType { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsUse { get; set; }
    }
}
