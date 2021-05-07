using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Lxsh.Project.NetCoreWebApi.Extensions
{
    public static partial class Extensions
    {
        public static void CopyValueTo(this object t1, object t2)
        {
            foreach (var i in t1.GetType().GetProperties())
            {
                foreach (var j in t2.GetType().GetProperties())
                {
                    if (i.Name == j.Name)
                    {
                        var val = i.GetValue(t1, null);
                        j.SetValue(t2, val, null);
                    }
                }
            }
        }
        public static T CopyValueTo<T>(this object t1) where T : new()
        {
            T t2 = new T();
            foreach (var i in t1.GetType().GetProperties())
            {
                foreach (var j in typeof(T).GetProperties())
                {
                    if (i.Name == j.Name)
                    {
                        var val = i.GetValue(t1, null);
                        j.SetValue(t2, val, null);
                    }
                }
            }
            return t2;
        }


        public static string ObjectToJSON(this object obj)
        {
            return JsonConvert.SerializeObject(obj, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
        }

        /// <summary>
        /// 判断是否为Null或者空
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this object obj)
        {
            if (obj == null)
                return true;
            else
            {
                string objStr = obj.ToString();
                return string.IsNullOrEmpty(objStr);
            }
        }
        #region 判断字符串是否是数字的  


        /// <summary>  
        /// 判断是否是数字  
        /// </summary>  
        /// <param name="str">字符串</param>  
        /// <returns>bool</returns>  
        public static bool IsNumeric(this object str)
        {
            if (str == null || str.ToString().Length == 0)
                return false;
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            byte[] bytestr = ascii.GetBytes(str.ToString());
            foreach (byte c in bytestr)
            {
                if (c < 48 || c > 57)
                {
                    return false;
                }
            }
            return true;
        }

        #endregion 判断字符串是否是数字的
    }
}
