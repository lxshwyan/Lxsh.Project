
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lxsh.Project.Common.RabbitMQ
{
    public static class SerializeExtension
    {
        public static string ToJson(this object obj, bool ignoreNull = false)
        {
            string result;
            if (obj==null)
            {
                result = null;
            }
            else
            {
                result = JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings
                {
                    DateFormatString = "yyyy-MM-dd HH:mm:ss",
                    NullValueHandling = (ignoreNull ? NullValueHandling.Ignore : NullValueHandling.Include)
                });
            }
            return result;
        }

        public static T FromJson<T>(this string jsonStr)
        {
            return string.IsNullOrEmpty(jsonStr) ? default(T) : JsonConvert.DeserializeObject<T>(jsonStr);
        }

        public static byte[] SerializeUtf8(this string str)
        {
            return (str == null) ? null : Encoding.UTF8.GetBytes(str);
        }

        public static string DeserializeUtf8(this byte[] stream)
        {
            return (stream == null) ? null : Encoding.UTF8.GetString(stream);
        }
    }
}