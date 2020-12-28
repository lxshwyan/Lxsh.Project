using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lxsh.Project.Demo
{
    public class ReadWriteJsonFile
    {

        public static T ReadConfig<T>(string Name)
        {
            var file = ReadFromSQL<T>(Name);
            if (file == null)
            {
                file = ReadFromFile<T>(Name);
            }
            return file;
        }
        public static bool SaveConfig<T>(string fileName, T TContent)
        {
            bool sqlResult=  SaveFromSQL<T>(fileName, TContent);
            bool fileResult = SaveFromFile<T>(fileName, TContent);
            return sqlResult&& fileResult;

        }
        private static T ReadFromFile<T>(string fileName)
        {
            try
            {
                using (FileStream fileStream = new FileStream(GetPath(fileName), FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
                    {
                        string strConent = streamReader.ReadToEnd();
                        if (!string.IsNullOrWhiteSpace(strConent))
                        {
                            return JsonConvert.DeserializeObject<T>(strConent);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            return default(T);
        }
        private static bool SaveFromFile<T>(string fileName, T TContent)
        {
            try
            {
                if (TContent == null)
                {
                    return false;
                }
                using (var fileStream = new FileStream(GetPath(fileName), FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    using (var streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
                    {
                        streamWriter.Write(JsonConvert.SerializeObject(TContent));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            return true;
        }
        private static T ReadFromSQL<T>(string name)
        {


            return default(T);
        }
        private static bool SaveFromSQL<T>(string filePath, T TContent)
        {

            return true;
        }

        /// <summary>  
        /// 获取网卡的MAC地址  
        /// </summary>  
        /// <returns>返回一个string</returns>  
        private static string GetNetCardMAC()
        {
            try
            {
                string stringMAC = "";
                ManagementClass MC = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection MOC = MC.GetInstances();
                foreach (ManagementObject MO in MOC)
                {
                    if ((bool)MO["IPEnabled"] == true)
                    {
                        stringMAC += MO["MACAddress"].ToString();

                        return stringMAC;
                    }
                }
                return "";
            }
            catch
            {
                return "";
            }
        }
        /// <summary>  
        /// 获取第一分区硬盘编号  
        /// </summary>  
        /// <returns>返回一个字符串类型</returns>  
        private static string GetHardDiskID()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
                string strHardDiskID = null;
                foreach (ManagementObject mo in searcher.Get())
                {
                    strHardDiskID = mo["SerialNumber"].ToString().Trim();
                    break;
                }
                return strHardDiskID;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 获取文件路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetPath(string fileName)
        {

            return AppDomain.CurrentDomain.BaseDirectory + "\\" + fileName + ".json";

        }
    }

}
