using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lxsh.Project.CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
         var st=   GetIdCardInfo("421381198809034011");
        }

        /// <summary>
        /// 根据身份证获取身份证信息
        /// 18位身份证
        /// 0地区代码(1~6位,其中1、2位数为各省级政府的代码，3、4位数为地、市级政府的代码，5、6位数为县、区级政府代码)
        /// 1出生年月日(7~14位)
        /// 2顺序号(15~17位单数为男性分配码，双数为女性分配码)
        /// 3性别
        //-----------------
        /// 15位身份证
        /// 0地区代码
        /// 1出生年份(7~8位年,9~10位为出生月份，11~12位为出生日期
        /// 2顺序号(13~15位)，并能够判断性别，奇数为男，偶数为女
        /// 3性别
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        public static string[] GetIdCardInfo(string cardId)
        {
            string[] info = new string[4];

            if (string.IsNullOrEmpty(cardId))
            {
                return info;
            }

            try
            {
                System.Text.RegularExpressions.Regex regex = null;
                if (cardId.Length == 18)
                {
                    regex = new Regex(@"^\d{17}(\d|x|X)$");
                    if (regex.IsMatch(cardId))
                    {

                        info.SetValue(cardId.Substring(0, 6), 0);
                        info.SetValue(DateTime.ParseExact(cardId.Substring(6, 8), "yyyyMMdd", CultureInfo.CurrentCulture).ToString("yyyy-MM-dd"), 1);
                        info.SetValue(cardId.Substring(14, 3), 2);
                        info.SetValue(Convert.ToInt32(info[2]) % 2 != 0 ? "男" : "女", 3);
                    }
                }
                else if (cardId.Length == 15)
                {
                    regex = new Regex(@"^\d{15}");
                    if (regex.IsMatch(cardId))
                    {
                        info.SetValue(cardId.Substring(0, 6), 0);
                        info.SetValue(DateTime.ParseExact(cardId.Substring(6, 6), "yyyyMMdd", CultureInfo.CurrentCulture).ToString("yyyy-MM-dd"), 1);
                        info.SetValue(cardId.Substring(12, 3), 2);
                        info.SetValue(Convert.ToInt32(info[2]) % 2 != 0 ? "男" : "女", 3);
                    }
                }
            }
            catch (Exception ex)
            {
                info[0] = ex.Message;
            }

            return info;
        }
    }
}
