using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace SFBR.Photograph.Utils
{
    public delegate void PostFileDelegate(int index, long size);
    public class HttpHelper
    {
        private static void PostData(string strBoundary, Stream reqStream, string key, string value)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("--" + strBoundary + "\r\n");
            sb.Append("Content-Disposition: form-data; name=\"" + key + "\"\r\n");
            sb.Append("\r\n");
            sb.Append(value);
            sb.Append("\r\n");
            //避免中文出现乱码,这里使用utf-8编码
            var sbBuffer = Encoding.UTF8.GetBytes(sb.ToString());
            reqStream.Write(sbBuffer, 0, sbBuffer.Length);
        }
        private static void PostFile(string strBoundary, Stream reqStream, string key, string path, int index, PostFileDelegate process)
        {
            var name = Regex.Match(path, "[^\\.]+$").Captures[0].Value;//获取后缀名
            StringBuilder sb = new StringBuilder();
            sb.Append("--" + strBoundary + "\r\n");
            //注意如果name跟filename有相同的话,上传的文件只会有一个
            sb.Append("Content-Disposition: form-data; name=\"" + key + "\"; filename=\"" + key + "." + name + "\"\r\n");
            sb.Append("Content-Type:application/octet-stream\r\n");
            sb.Append("\r\n");
            var sbBuffer = Encoding.Default.GetBytes(sb.ToString());
            reqStream.Write(sbBuffer, 0, sbBuffer.Length);
            long offset = 0;
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var len = Math.Min(fs.Length, 1024 * 500);
                var buffer = new byte[len];
                int size = fs.Read(buffer, 0, buffer.Length);
                while (size > 0)
                {
                    reqStream.Write(buffer, 0, size);
                    offset += size;
                    if (process != null)
                    {
                        process(index, offset);
                    }
                    size = fs.Read(buffer, 0, size);
                }
            }
            byte[] boundaryBytes = Encoding.Default.GetBytes("--" + strBoundary + "\r\n");
            reqStream.Write(boundaryBytes, 0, boundaryBytes.Length);
        }
        private static void PostEnd(string strBoundary, Stream reqStream)
        {
            var bytes = Encoding.Default.GetBytes("--" + strBoundary + "--");
            reqStream.Write(bytes, 0, bytes.Length);
        }
        public static Stream PostFiles(string url, Dictionary<string, string> @params, Dictionary<string, string> files, PostFileDelegate process)
        {
            HttpWebRequest req = HttpWebRequest.Create(url) as HttpWebRequest;
            //req.Referer = referer;
            req.Accept = "Accept:text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            req.Headers["Accept-Language"] = "zh-CN,zh;q=0.";
            req.Headers["Accept-Charset"] = "GBK,utf-8;q=0.7,*;q=0.3";
            req.UserAgent = "User-Agent:Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.835.202 Safari/535.1";
            req.KeepAlive = true;
            req.ContentType = "application/x-www-form-urlencoded";
            req.Host = GetHost(url);
            Encoding encoding = Encoding.UTF8;
            req.Method = "POST";
            string strBoundary = "--123";//可以随便填写
            req.ContentType = "multipart/form-data; boundary=" + strBoundary;
            using (var reqStream = req.GetRequestStream())
            {
                if (@params != null)
                {
                    foreach (var i in @params)
                    {
                        PostData(strBoundary, reqStream, i.Key, i.Value);
                    }
                }
                if (files != null)
                {
                    int n = 0;
                    foreach (var i in files)
                    {
                        PostFile(strBoundary, reqStream, i.Key, i.Value, n++, process);
                    }
                }
                PostEnd(strBoundary, reqStream);
            }

            try
            {
                HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
                return resp.GetResponseStream();
            }
            catch (Exception ex)
            {
                var buffer = Encoding.Default.GetBytes(ex.Message + "[" + url + "]");
                MemoryStream ms = new MemoryStream();
                ms.Write(buffer, 0, buffer.Length);
                ms.Position = 0;
                return ms;
            }
        }

        public static string GetString(string url, Dictionary<string, string> @params, Dictionary<string, string> files, PostFileDelegate process)
        {
            using (StreamReader sr = new StreamReader(PostFiles(url, @params, files, process), Encoding.Default))
            {
                return sr.ReadToEnd();
            }
        }
        private static string GetHost(string url)
        {
            Regex regex = new Regex("//([^/]+)");
            return regex.Match(url).Groups[1].Value;
        }
        public static string Get(string Url, string Referer, Encoding Encoder, ref string CookieStr)
        {
            string result = "";

            WebClient myClient = new WebClient();
            myClient.Headers.Add("Accept: */*");
            myClient.Headers.Add("User-Agent: Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; Trident/4.0; .NET4.0E; .NET4.0C; InfoPath.2; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; SE 2.X MetaSr 1.0)");
            myClient.Headers.Add("Accept-Language: zh-cn");
            myClient.Headers.Add("Content-Type: multipart/form-data");
            myClient.Headers.Add("Accept-Encoding: gzip, deflate");
            myClient.Headers.Add("Cache-Control: no-cache");
            if (!string.IsNullOrEmpty(CookieStr))
            {
                myClient.Headers.Add(CookieStr);
            }
            myClient.Encoding = Encoder;
            result = myClient.DownloadString(Url);
            var cookie = myClient.ResponseHeaders["Set-Cookie"];
            if (!string.IsNullOrEmpty(cookie))
            {
                CookieStr = GetCookie(cookie);
            }
            myClient.Dispose();
            return result;
        }
        public static string Post(string Url, string Referer, Encoding Encoder, ref string CookieStr, NameValueCollection data)
        {
            WebClient myClient = new WebClient();
            myClient.Headers.Add("Accept: */*");
            myClient.Headers.Add("User-Agent: Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; Trident/4.0; .NET4.0E; .NET4.0C; InfoPath.2; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; SE 2.X MetaSr 1.0)");
            myClient.Headers.Add("Accept-Language: zh-cn");
            myClient.Headers.Add("Accept-Encoding: gzip, deflate");
            myClient.Headers.Add("Cache-Control: no-cache");
            if (!string.IsNullOrEmpty(CookieStr))
            {
                myClient.Headers.Add(CookieStr);
            }
            myClient.Encoding = Encoder;
            var result = myClient.UploadValues(Url, data);
            var cookie = myClient.ResponseHeaders["Set-Cookie"];
            if (!string.IsNullOrEmpty(cookie))
            {
                CookieStr = GetCookie(cookie);
            }
            myClient.Dispose();
            return Encoding.Default.GetString(result);
        }
        private static string GetCookie(string CookieStr)
        {
            string result = "";

            string[] myArray = CookieStr.Split(',');
            if (myArray.Count() > 0)
            {
                result = "Cookie: ";
                foreach (var str in myArray)
                {
                    string[] CookieArray = str.Split(';');
                    result += CookieArray[0].Trim();
                    result += "; ";
                }
                result = result.Substring(0, result.Length - 2);
            }
            return result;
        }
    }
}
