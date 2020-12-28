using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.WebSocketDemo
{
    class Program
    {
        static string strKey = "bb635dd47e5861f717472df95652077356a8f38dea6347851c191f66b7cf9dc8";
        static void Main(string[] args)
        {
        


            string App_Key = ConfigurationSettings.AppSettings["App_Key"];
            string App_Secret = ConfigurationSettings.AppSettings["App_Secret"];
            string wsUrl = ConfigurationSettings.AppSettings["wsUrl"];
            string timestamp = GetTimeStamp();
            string sign = Encrypt($"{timestamp}#{App_Secret}");
            WebSocketHepler.url = $"{wsUrl}/{App_Key}/{timestamp}/{sign}";
            WebSocketHepler.Instance.OnMessage += Instance_OnMessage;
           // PostGuest();

        }

        #region 人脸识别
        private static void Instance_OnMessage(string obj)
        {
            //{ "code":30000,"message":"push record","data":{ "id":19,"userId":3,"name":"谷雨露","type":1,"avatar":"5eb5226221232f0001e661cd","direction":0,"verifyScore":0.939,"receptionUserId":0,"receptionUserName":"","groups":[{"id":1,"name":"默认组","type":1}],"deviceName":"测试","sn":"SPSP-751a16322a906adf2b232699191c8742","signDate":"2020-05-11","signTime":1589167204,"signAvatar":"5eb8c46421232f0001e6620c","signBgAvatar":"5eb8c46421232f0001e6620d","mobile":"15372591753","icNumber":"","idNumber":"","jobNumber":"","remark":"","entryMode":1,"signTimeZone":"+08:00","docPhoto":"","latitude":0.0,"longitude":0.0,"address":"","location":"办公室","abnormalType":0,"userIcNumber":"","userIdNumber":""}};
            //  Console.WriteLine(obj);
           // obj = ReadTxtStr();
            Result result = JsonConvert.DeserializeObject<Result>(obj);
            if (result == null)
            {
                Console.WriteLine("第一步消息解析失败");
                return;
            }
            switch (result.Code)
            {
                case 10000:
                    Console.WriteLine("打印心跳消息");
                    break;
                case 20000:
                    Console.WriteLine("连接成功");
                    break;
                case 20002:
                    Console.WriteLine("连接失败");
                    break;
                case 30000:
                    Console.WriteLine("准备解析识别记录");
                    RecognitionRecord recognitionRecord = result.Data;
                    Console.WriteLine($"识别人用户id  {recognitionRecord.userId}");
                    Console.WriteLine($"识别人用户姓名  {recognitionRecord.name}");
                    Console.WriteLine($"识别人用户jobNumber  {recognitionRecord.jobNumber}");
                    Console.WriteLine($"识别人用户IcNumber  {recognitionRecord.icNumber}");
                
                    Console.WriteLine($"识别人用户IdNumber  {recognitionRecord.idNumber}");
                    if (!string.IsNullOrWhiteSpace(recognitionRecord.idNumber))
                    {
                        Console.WriteLine($"识别人用户IdNumber 解密 {DecryptDes(recognitionRecord.idNumber, strKey)}");
                    }
                    Console.WriteLine($"识别人用户Type  {recognitionRecord.type}");
                    Console.WriteLine($"识别人进出方向  {recognitionRecord.direction}");
                    Console.WriteLine($"验证设备名称  {recognitionRecord.deviceName}");
                    Console.WriteLine($"验证设备ID  {recognitionRecord.sn}");
                    Console.WriteLine($"验证分数 {recognitionRecord.verifyScore}");
                    // http://115.231.57.18:8888/v1/image/1/5eb8b16121232f0001e661e5   2是抓拍，1是上传的照片
                    Console.WriteLine($"识别照片 {recognitionRecord.avatar}");//http://115.231.57.18:8888/v1/image/2/5eb8b16121232f0001e661e5
                    break;
                default:
                    break;
            }

            Console.WriteLine(obj);
        }
        /// <summary>
        /// MD5加密,和动网上的16/32位MD5加密结果相同
        /// </summary>
        /// <param name="strSource">待加密字串</param>
        /// <param name="length">16或32值之一,其它则采用.net默认MD5加密算法</param>
        /// <returns>加密后的字串</returns>
        public static string Encrypt(string source, int length = 32)
        {
            HashAlgorithm provider = CryptoConfig.CreateFromName("MD5") as HashAlgorithm;
            if (string.IsNullOrEmpty(source)) return string.Empty;

            byte[] bytes = Encoding.ASCII.GetBytes(source);
            byte[] hashValue = provider.ComputeHash(bytes);
            StringBuilder sb = new StringBuilder();
            switch (length)
            {
                case 16://16位密文是32位密文的9到24位字符
                    for (int i = 4; i < 12; i++)
                        sb.Append(hashValue[i].ToString("x2"));
                    break;
                case 32:
                    for (int i = 0; i < 16; i++)
                    {
                        sb.Append(hashValue[i].ToString("x2"));
                    }
                    break;
                default:
                    for (int i = 0; i < hashValue.Length; i++)
                    {
                        sb.Append(hashValue[i].ToString("x2"));
                    }
                    break;
            }
            return sb.ToString();
        }

        public static string DecryptDes(string content, string desKey)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider { Key = Encoding.UTF8.GetBytes(desKey.Substring(0, 8)), Mode = CipherMode.ECB })
            {
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(content)))
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            return sr.ReadLine();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 获取当前时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }
        public static string ReadTxtStr()
        {
            string TextStr;
            string file = AppDomain.CurrentDomain.BaseDirectory + "TestResult.txt";
            using (StreamReader sr = new StreamReader(file, System.Text.Encoding.Default))
            {
                TextStr = sr.ReadToEnd().ToString();
                sr.Close();
            }
            return TextStr;
        }
        #endregion

        #region 访客添加
        public static void PostGuest()
        {
            string App_Key = ConfigurationSettings.AppSettings["App_Key"];
            string App_Secret = ConfigurationSettings.AppSettings["App_Secret"];
            string Url = ConfigurationSettings.AppSettings["Url"];
            string timestamp = GetTimeStamp();
            string sign = Encrypt($"{timestamp}#{App_Secret}");
            string fileName = AppDomain.CurrentDomain.BaseDirectory + "image\\test.jpg";
            byte[] fileData = FileToStream(fileName);
            string guestUrl = $"{Url}/guest";
            UpLoadFile upLoadFile = new UpLoadFile() { Content_Type = "image/jpeg", Data = fileData, FileName = "test.jpg", Name = "avatarFile" };
            Dictionary<string, string> input = new Dictionary<string, string>();
            input.Add("app_key", App_Key);
            input.Add("sign", sign);
            input.Add("timestamp", timestamp);
            input.Add("name", "吕召瑞");
            input.Add("mobile", "13546894653");
            input.Add("remark", "demo测试");
            input.Add("groups", "2");
            input.Add("force", "0");
            input.Add("idNumber", "421381198809034012");
            input.Add("receptionUserId", "3");
            input.Add("dateTimeFrom", "2018-07-20 12:30:45");
            input.Add("dateTimeTo", "2020-07-20 12:30:45");
            input.Add("gender", "2");
           WebAPIHelper.PostResponse(guestUrl, upLoadFile, input, Encoding.UTF8);// 测试新增访客
            input = new Dictionary<string, string>();
            input.Add("app_key", App_Key);
            input.Add("sign", sign);
            input.Add("timestamp", timestamp);
            input.Add("name", "访客测试组2");
            input.Add("type", "2");
            upLoadFile = null;
             guestUrl = $"{Url}//group";
           WebAPIHelper.PostResponse(guestUrl, upLoadFile, input, Encoding.UTF8);
        }

        public static byte[] FileToStream(string fileName)
        {
            // 打开文件
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);

            // 读取文件的 byte[]
            byte[] bytes = new byte[fileStream.Length];

            fileStream.Read(bytes, 0, bytes.Length);

            fileStream.Close();
            return bytes;
        }
        #endregion

      
    }
}
