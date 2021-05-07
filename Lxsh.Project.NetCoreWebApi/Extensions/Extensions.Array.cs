using System;
using System.Collections.Generic;
using System.Text;

namespace Lxsh.Project.NetCoreWebApi.Extensions
{
    public static partial class Extensions
    {
        public static string JoinString<T>(this List<T> list, Func<T, string> func, char charSet)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var i in list)
            {
                sb.Append(func(i));
                sb.Append(charSet);
            }
            return sb.ToString().TrimEnd(charSet);
        }
    }
}
