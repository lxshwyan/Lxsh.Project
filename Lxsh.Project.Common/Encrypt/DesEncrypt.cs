using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.Common.Encrypt
{
    /// <summary>
    /// 可逆对称加密  密钥长度8
    /// </summary>
    public class DesEncrypt
    {
        //8位长度
        private static string KEY = "ruanmou1";
        private static byte[] key = ASCIIEncoding.ASCII.GetBytes(KEY.Substring(0, 8));
        private static byte[] iv = ASCIIEncoding.ASCII.GetBytes(KEY.Insert(0, "w").Substring(0, 8));

        /// <summary>
        /// DES 加密
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static string Encrypt(string strValue)
        {
            DESCryptoServiceProvider dsp = new DESCryptoServiceProvider();
            MemoryStream memStream = new MemoryStream();
            using (memStream)
            {
                CryptoStream crypStream = new CryptoStream(memStream, dsp.CreateEncryptor(key, iv), CryptoStreamMode.Write);
                StreamWriter sWriter = new StreamWriter(crypStream);
                sWriter.Write(strValue);
                sWriter.Flush();
                crypStream.FlushFinalBlock();
                memStream.Flush();
                return Convert.ToBase64String(memStream.GetBuffer(), 0, (int)memStream.Length);
            }
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="EncValue"></param>
        /// <returns></returns>
        public static string Decrypt(string EncValue)
        {
            DESCryptoServiceProvider dsp = new DESCryptoServiceProvider();
            byte[] buffer = Convert.FromBase64String(EncValue);
            MemoryStream memStream = new MemoryStream();
            using (memStream)
            {
                CryptoStream crypStream = new CryptoStream(memStream, dsp.CreateDecryptor(key, iv), CryptoStreamMode.Write);
                crypStream.Write(buffer, 0, buffer.Length);
                crypStream.FlushFinalBlock();
                return ASCIIEncoding.UTF8.GetString(memStream.ToArray());
            }
        }
    }
}
