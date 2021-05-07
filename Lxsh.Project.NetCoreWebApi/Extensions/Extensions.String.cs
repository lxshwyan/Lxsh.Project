using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;

namespace Lxsh.Project.NetCoreWebApi.Extensions
{
    public static partial class Extensions
    {
        [DebuggerStepThrough] //该特性是用在方法前面的，在想要跳过的方法前面加上 
        public static T FromJson<T>(this string jsonStr)
        {
            return string.IsNullOrEmpty(jsonStr) ? default(T) : JsonConvert.DeserializeObject<T>(jsonStr);
        }
        /// <summary>
        /// 指示指定的字符串是 null、空或者仅由空白字符组成。
        /// </summary>
        [DebuggerStepThrough] //该特性是用在方法前面的，在想要跳过的方法前面加上 
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
        /// <summary>
        /// 将Json字符串转为DataTable
        /// </summary>
        /// <param name="jsonStr">Json字符串</param>
        /// <returns></returns>
        public static DataTable ToDataTable(this string jsonStr)
        {
            return jsonStr == null ? null : JsonConvert.DeserializeObject<DataTable>(jsonStr);
        }

        /// <summary>
        /// 字符串是否符合GUID格式
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static bool IsGuid(this string guid)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(guid, "^[a-zA-Z0-9]{8}-(?:[a-zA-Z0-9]{4}-){3}[a-zA-Z0-9]{12}$");
        }
        /// <summary>
        /// 转int
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ToInt(this string str)
        {
            return int.TryParse(str, out int ret) ? ret : 0;
        }
        /// <summary>
        /// 编码加1
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static int NewCode(this string code)
        {
            if (code.Length - 4 >= 4)
            {
                return code.Substring(code.Length - 4, 4).ToInt() + 1;
            }
            else
            {
                return code.ToInt() + 1;
            }
        }
    }
}
