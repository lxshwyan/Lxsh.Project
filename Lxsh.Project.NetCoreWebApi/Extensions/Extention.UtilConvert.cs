/*
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 *                                                                                 *
 * Copyright (c) 2019 Company Name                                                 *
 *                                                                                 *
 * Author Lxsh                                                                     *
 *                                                                                 *
 * Time 2020-08-20 17:09:15                                                        *
 *                                                                                 *
 * Describe                                                                        *
 *                                                                                 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 */
using System;

namespace Lxsh.Project.NetCoreWebApi.Extensions
{
    public static partial class UtilConvert
    {
        /// 
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static int ObjToInt(this object thisValue)
        {
            int reval = 0;
            if (thisValue == null) return 0;
            if (thisValue != null && thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }
        public static bool ObjToBool(this object thisValue)
        {
            bool result = false;
            if (thisValue != null && thisValue != DBNull.Value && bool.TryParse(thisValue.ToString(), out result))
            {
                return result;
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static string ObjToString(this object thisValue)
        {
            if (thisValue != null) return thisValue.ToString().Trim();
            return "";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static bool IsNotEmptyOrNull(this object thisValue)
        {
            return ObjToString(thisValue) != "";
        }
        /// <summary>
        /// 获取开始时间
        /// </summary>
        /// <param name="thisValue">需要格式化时间</param>
        /// <returns>返回开始时间  xxxx-xx-xx  00:00:00或null</returns>
        public static DateTime? GetStartTime(this DateTime? thisValue)
        {
            if (!thisValue.HasValue)
            {
                return null;
            }
            return Convert.ToDateTime(Convert.ToDateTime(thisValue).ToString("yyyy-MM-dd 00:00:00"));
        }
        /// <summary>
        /// 获取结束时间
        /// </summary>
        /// <param name="thisValue">需要格式化时间</param>
        /// <returns>返回结束时间  xxxx-xx-xx  23:59:59或null</returns>
        public static DateTime? GetEndTime(this DateTime? thisValue)
        {
            if (!thisValue.HasValue)
            {
                return null;
            }
            return Convert.ToDateTime(Convert.ToDateTime(thisValue).ToString("yyyy-MM-dd 23:59:59"));
        }
        /// <summary>
        /// 获取开始时间
        /// </summary>
        /// <param name="thisValue">需要格式化时间</param>
        /// <returns>返回开始时间  xxxx-xx-xx  00:00:00</returns>
        public static DateTime GetStartTime(this DateTime thisValue)
        {

            return Convert.ToDateTime(Convert.ToDateTime(thisValue).ToString("yyyy-MM-dd 00:00:00"));
        }
        /// <summary>
        /// 获取结束时间
        /// </summary>
        /// <param name="thisValue">需要格式化时间</param>
        /// <returns>返回结束时间  xxxx-xx-xx  23:59:59</returns>
        public static DateTime GetEndTime(this DateTime thisValue)
        {
            return Convert.ToDateTime(Convert.ToDateTime(thisValue).ToString("yyyy-MM-dd 23:59:59"));
        }




        #region 系统时间和unix时间戳转换
        /// <summary>
        /// 将系统时间转换成unix时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string TimeUnix(this string time)
        {
            DateTime dtNow = DateTime.Parse(time);
            DateTimeOffset dto = new DateTimeOffset(dtNow);
            return dto.ToUnixTimeSeconds().ToString();
        }
        /// <summary>
        /// 将unix时间戳转换成系统时间
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime UnixTime(this string time)
        {
            long lTime = long.Parse(time);
            var dto = DateTimeOffset.FromUnixTimeSeconds(lTime);
            return dto.ToLocalTime().DateTime;
        }

        /// <summary>
        /// 将系统时间转换成unix时间戳
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static long TimeUnix(this DateTime dt)
        {
            DateTimeOffset dto = new DateTimeOffset(dt);
            return dto.ToUnixTimeSeconds();
        }

        /// <summary>
        /// 将unix时间戳转换成系统时间
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static DateTime UnixTime(this long d)
        {
            var dto = DateTimeOffset.FromUnixTimeSeconds(d);
            return dto.ToLocalTime().DateTime;
        }
        #endregion
    }
}
