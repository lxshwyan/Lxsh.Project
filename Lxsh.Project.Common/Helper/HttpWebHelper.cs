
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Lxsh.Project.Common
{
    public class HttpWebHelper
    {
        private const string DefaultUserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";

        private CookieCollection _cookies = new CookieCollection();

        public WebProxy Proxy
        {
            get;
            set;
        }

        public CookieCollection Cookies
        {
            get
            {
                return this._cookies;
            }
        }

        public void ClearCookies()
        {
            this._cookies = new CookieCollection();
        }

        private HttpWebResponse CreateGetHttpResponse(string url, int? timeout = 300, string userAgent = "", CookieCollection cookies = null, string referer = "", Dictionary<string, string> headers = null, string contentType = "application/x-www-form-urlencoded", bool? keepAlive = true, string accept = "*/*")
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            HttpWebRequest httpWebRequest;
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(HttpWebHelper.CheckValidationResult);
                httpWebRequest = (WebRequest.Create(url) as HttpWebRequest);
                httpWebRequest.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                httpWebRequest = (WebRequest.Create(url) as HttpWebRequest);
            }
            if (this.Proxy != null)
            {
                httpWebRequest.Proxy = this.Proxy;
            }
            httpWebRequest.Method = "GET";
            httpWebRequest.Headers["Pragma"] = "no-cache";
            httpWebRequest.Accept = accept;
            httpWebRequest.Headers["Accept-Language"] = "en-US,en;q=0.5";
            httpWebRequest.ContentType = contentType;
            httpWebRequest.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
            httpWebRequest.Referer = referer;
            if (keepAlive.HasValue)
            {
                httpWebRequest.KeepAlive = keepAlive.Value;
            }
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> current in headers)
                {
                    httpWebRequest.Headers.Add(current.Key, current.Value);
                }
            }
            if (!string.IsNullOrEmpty(userAgent))
            {
                httpWebRequest.UserAgent = userAgent;
            }
            if (timeout.HasValue)
            {
                httpWebRequest.Timeout = timeout.Value * 1000;
            }
            if (cookies != null)
            {
                httpWebRequest.CookieContainer = new CookieContainer();
                httpWebRequest.CookieContainer.Add(cookies);
            }
            else
            {
                httpWebRequest.CookieContainer = new CookieContainer();
                httpWebRequest.CookieContainer.Add(this.Cookies);
            }
            return httpWebRequest.GetResponse() as HttpWebResponse;
        }

        private HttpWebResponse CreatePostHttpResponse(string url, IDictionary<string, string> parameters, Encoding requestEncoding, int? timeout = 300, string userAgent = "", CookieCollection cookies = null, string referer = "", Dictionary<string, string> headers = null, string contentType = "application/x-www-form-urlencoded", bool? keepAlive = true, string accept = "*/*")
        {
            string parameters2 = HttpWebHelper.CreateParameter(parameters);
            return this.CreatePostHttpResponse(url, parameters2, requestEncoding, timeout, userAgent, cookies, referer, headers, contentType, keepAlive, accept);
        }

        private HttpWebResponse CreatePostHttpResponse(string url, string parameters, Encoding requestEncoding, int? timeout = 300, string userAgent = "", CookieCollection cookies = null, string referer = "", Dictionary<string, string> headers = null, string contentType = "application/x-www-form-urlencoded", bool? keepAlive = true, string accept = "*/*")
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            if (requestEncoding == null)
            {
                throw new ArgumentNullException("requestEncoding");
            }
            HttpWebRequest httpWebRequest;
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(HttpWebHelper.CheckValidationResult);
                httpWebRequest = (WebRequest.Create(url) as HttpWebRequest);
                httpWebRequest.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                httpWebRequest = (WebRequest.Create(url) as HttpWebRequest);
            }
            if (this.Proxy != null)
            {
                httpWebRequest.Proxy = this.Proxy;
            }
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("Accept-Language", "zh-CN,en-GB;q=0.5");
            httpWebRequest.Accept = accept;
            httpWebRequest.Referer = referer;
            httpWebRequest.Headers["Accept-Language"] = "en-US,en;q=0.5";
            httpWebRequest.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
            httpWebRequest.ContentType = contentType;
            httpWebRequest.Headers["Pragma"] = "no-cache";
            if (keepAlive.HasValue)
            {
                httpWebRequest.KeepAlive = keepAlive.Value;
            }
            if (cookies != null)
            {
                httpWebRequest.CookieContainer = new CookieContainer();
                httpWebRequest.CookieContainer.Add(cookies);
            }
            else
            {
                httpWebRequest.CookieContainer = new CookieContainer();
                httpWebRequest.CookieContainer.Add(this.Cookies);
            }
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> current in headers)
                {
                    httpWebRequest.Headers.Add(current.Key, current.Value);
                }
            }
            if (!string.IsNullOrEmpty(userAgent))
            {
                httpWebRequest.UserAgent = userAgent;
            }
            else
            {
                httpWebRequest.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
            }
            if (timeout.HasValue)
            {
                httpWebRequest.Timeout = timeout.Value * 1000;
            }
            httpWebRequest.Expect = string.Empty;
            if (!string.IsNullOrEmpty(parameters))
            {
                byte[] bytes = requestEncoding.GetBytes(parameters);
                using (Stream requestStream = httpWebRequest.GetRequestStream())
                {
                    requestStream.Write(bytes, 0, bytes.Length);
                }
            }
            return httpWebRequest.GetResponse() as HttpWebResponse;
        }

        private HttpWebResponse CreatePostFileHttpResponse(string url, string filePath, int? timeout = 300, string userAgent = "", CookieCollection cookies = null, string referer = "", Dictionary<string, string> headers = null, string contentType = "application/x-www-form-urlencoded", bool? keepAlive = true, string accept = "*/*")
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            HttpWebRequest httpWebRequest;
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(HttpWebHelper.CheckValidationResult);
                httpWebRequest = (WebRequest.Create(url) as HttpWebRequest);
                httpWebRequest.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                httpWebRequest = (WebRequest.Create(url) as HttpWebRequest);
            }
            if (this.Proxy != null)
            {
                httpWebRequest.Proxy = this.Proxy;
            }
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = accept;
            httpWebRequest.Referer = referer;
            httpWebRequest.Headers["Accept-Language"] = "en-US,en;q=0.5";
            httpWebRequest.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
            httpWebRequest.ContentType = contentType;
            httpWebRequest.Headers["Pragma"] = "no-cache";
            if (keepAlive.HasValue)
            {
                httpWebRequest.KeepAlive = keepAlive.Value;
            }
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> current in headers)
                {
                    httpWebRequest.Headers.Add(current.Key, current.Value);
                }
            }
            if (cookies != null)
            {
                httpWebRequest.CookieContainer = new CookieContainer();
                httpWebRequest.CookieContainer.Add(cookies);
            }
            else
            {
                httpWebRequest.CookieContainer = new CookieContainer();
                httpWebRequest.CookieContainer.Add(this.Cookies);
            }
            if (!string.IsNullOrEmpty(userAgent))
            {
                httpWebRequest.UserAgent = userAgent;
            }
            else
            {
                httpWebRequest.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
            }
            if (timeout.HasValue)
            {
                httpWebRequest.Timeout = timeout.Value * 1000;
            }
            httpWebRequest.Expect = string.Empty;
            if (!string.IsNullOrEmpty(filePath))
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    string text = "----------" + DateTime.Now.Ticks.ToString("x");
                    byte[] bytes = Encoding.ASCII.GetBytes("\r\n--" + text + "\r\n");
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("--");
                    stringBuilder.Append(text);
                    stringBuilder.Append("\r\n");
                    stringBuilder.Append("Content-Disposition: form-data; name=\"");
                    stringBuilder.Append("file");
                    stringBuilder.Append("\"; filename=\"");
                    stringBuilder.Append(fileStream.Name);
                    stringBuilder.Append("\"");
                    stringBuilder.Append("\r\n");
                    stringBuilder.Append("Content-Type: ");
                    stringBuilder.Append("application/octet-stream");
                    stringBuilder.Append("\r\n");
                    stringBuilder.Append("\r\n");
                    string s = stringBuilder.ToString();
                    byte[] bytes2 = Encoding.UTF8.GetBytes(s);
                    httpWebRequest.ContentType = "multipart/form-data; boundary=" + text;
                    long contentLength = fileStream.Length + (long)bytes2.Length + (long)bytes.Length;
                    httpWebRequest.ContentLength = contentLength;
                    byte[] array = new byte[fileStream.Length];
                    fileStream.Read(array, 0, array.Length);
                    using (Stream requestStream = httpWebRequest.GetRequestStream())
                    {
                        requestStream.Write(bytes2, 0, bytes2.Length);
                        requestStream.Write(array, 0, array.Length);
                        requestStream.Write(bytes, 0, bytes.Length);
                    }
                }
            }
            return httpWebRequest.GetResponse() as HttpWebResponse;
        }

        private static string CreateParameter(IDictionary<string, string> parameters)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string current in parameters.Keys)
            {
                stringBuilder.AppendFormat("&{0}={1}", current, parameters[current]);
            }
            return stringBuilder.ToString().TrimStart(new char[]
            {
                '&'
            });
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        public string Post(string url, IDictionary<string, string> parameters, Encoding requestEncoding, Encoding responseEncoding, int? timeout = 300, string userAgent = "", CookieCollection cookies = null, string referer = "", Dictionary<string, string> headers = null, string contentType = "application/x-www-form-urlencoded", bool? keepAlive = true, string accept = "*/*")
        {
            HttpWebResponse httpWebResponse = this.CreatePostHttpResponse(url, parameters, requestEncoding, timeout, userAgent, cookies, referer, headers, contentType, keepAlive, "*/*");
            string result;
            try
            {
                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), responseEncoding))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        public T Post<T>(string url, IDictionary<string, string> parameters, Encoding requestEncoding, Encoding responseEncoding, int? timeout = 300, string userAgent = "", CookieCollection cookies = null, string referer = "", Dictionary<string, string> headers = null, string contentType = "application/x-www-form-urlencoded", bool? keepAlive = true, string accept = "*/*")
        {
            return JsonConvert.DeserializeObject<T>(this.Post(url, parameters, requestEncoding, responseEncoding, timeout, userAgent, cookies, referer, headers, contentType, keepAlive, "*/*"));
        }

        public string Post(string url, string parameters, Encoding requestEncoding, Encoding responseEncoding, int? timeout = 300, string userAgent = "", CookieCollection cookies = null, string referer = "", Dictionary<string, string> headers = null, string contentType = "application/x-www-form-urlencoded", bool? keepAlive = true, string accept = "*/*")
        {
            HttpWebResponse httpWebResponse = this.CreatePostHttpResponse(url, parameters, requestEncoding, timeout, userAgent, cookies, referer, headers, contentType, keepAlive, "*/*");
            string result;
            try
            {
                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), responseEncoding))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        public T Post<T>(string url, string parameters, Encoding requestEncoding, Encoding responseEncoding, int? timeout = 300, string userAgent = "", CookieCollection cookies = null, string referer = "", Dictionary<string, string> headers = null, string contentType = "application/x-www-form-urlencoded", bool? keepAlive = true, string accept = "*/*")
        {
            string jsonStr = this.Post(url, parameters, requestEncoding, responseEncoding, timeout, userAgent, cookies, referer, headers, contentType, keepAlive, "*/*");
            return jsonStr.FromJson<T>();
        }

        public string PostFile(string url, string filePath, Encoding responseEncoding, int? timeout = 300, string userAgent = "", CookieCollection cookies = null, string referer = "", Dictionary<string, string> headers = null, string contentType = "application/x-www-form-urlencoded", bool? keepAlive = true, string accept = "*/*")
        {
            HttpWebResponse httpWebResponse = this.CreatePostFileHttpResponse(url, filePath, timeout, userAgent, cookies, referer, headers, contentType, keepAlive, "*/*");
            string result;
            try
            {
                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), responseEncoding))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        public string Get(string url, Encoding responseEncoding, int? timeout = 300, string userAgent = "", CookieCollection cookies = null, string referer = "", Dictionary<string, string> headers = null, string contentType = "application/x-www-form-urlencoded", bool? keepAlive = true, string accept = "*/*")
        {
            HttpWebResponse httpWebResponse = this.CreateGetHttpResponse(url, timeout, userAgent, cookies, referer, headers, contentType, keepAlive, "*/*");
            string result;
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), responseEncoding))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }

        public T Get<T>(string url, Encoding responseEncoding, int? timeout = 300, string userAgent = "", CookieCollection cookies = null, string referer = "", Dictionary<string, string> headers = null, string contentType = "application/x-www-form-urlencoded", bool? keepAlive = true, string accept = "*/*")
        {
            string jsonStr = this.Get(url, responseEncoding, timeout, userAgent, cookies, referer, headers, contentType, keepAlive, "*/*");
            return jsonStr.FromJson<T>();
        }

        public byte[] GetFile(string url, out Dictionary<string, string> header, int? timeout = 300, string userAgent = "", CookieCollection cookies = null, string referer = "", Dictionary<string, string> headers = null, bool? keepAlive = true, string accept = "*/*")
        {
            HttpWebResponse response = this.CreateGetHttpResponse(url, timeout, userAgent, cookies, referer, headers, "application/x-www-form-urlencoded", keepAlive, "*/*");
            header = response.Headers.AllKeys.ToDictionary((string key) => key, (string key) => response.Headers[key]);
            byte[] result;
            try
            {
                Stream responseStream = response.GetResponseStream();
                byte[] array = new byte[response.ContentLength];
                responseStream.Read(array, 0, array.Length);
                responseStream.Close();
                result = array;
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        public Stream GetStream(string url, int? timeout = 300, string userAgent = "", CookieCollection cookies = null, string referer = "", Dictionary<string, string> headers = null, bool? keepAlive = true, string accept = "*/*")
        {
            HttpWebResponse httpWebResponse = this.CreateGetHttpResponse(url, timeout, userAgent, cookies, referer, headers, "application/x-www-form-urlencoded", keepAlive, "*/*");
            return httpWebResponse.GetResponseStream();
        }
    }
}
