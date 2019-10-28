/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lxsh.Project.NetPostWebservice.Demo
*文件名： HttpHelper
*创建人： Lxsh
*创建时间：2019/9/19 18:22:29
*描述
*=======================================================================
*修改标记
*修改时间：2019/9/19 18:22:29
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Lxsh.Project.NetPostWebservice.Demo
{
    public class HttpHelper
    {
        /// <summary>
        /// Post带参请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="contentType">指定参数类型</param>
        /// <param name="strData"></param>
        /// <param name="dicHeader"></param>
        /// <returns></returns>
        public static string PostRequest(string url, DataTypeEnum contentType, string strData, Dictionary<string, string> dicHeader = null)
        {
            string result;
            var webRequest = WebRequest.Create(url);
            if (dicHeader != null)
                foreach (var m in dicHeader)
                {
                    webRequest.Headers.Add(m.Key, m.Value);
                }
            webRequest.Method = MethodTypeEnum.Post.ToString();
            webRequest.Proxy = null;
            if (contentType == DataTypeEnum.Form)
            {
                webRequest.ContentType = "application/x-www-form-urlencoded";
            }
            else
            {
                webRequest.ContentType = "application/" + contentType;
            }
          
                byte[] reqBodyBytes = System.Text.Encoding.UTF8.GetBytes(strData);
                Stream reqStream = webRequest.GetRequestStream();//加入需要发送的参数
                reqStream.Write(reqBodyBytes, 0, reqBodyBytes.Length);
                reqStream.Close(); 
            using (var reader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }

        /// <summary>
        /// Get不带参请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetRequest(string url)
        {
            string result;
            WebRequest webRequest = WebRequest.Create(url);
            webRequest.Method = MethodTypeEnum.Get.ToString();
            webRequest.Proxy = null;
            using (var reader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }

        /// <summary>
        /// Post不带参请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string PostRequest(string url)
        {
            string result;
            WebRequest webRequest = WebRequest.Create(url);
            webRequest.Method = MethodTypeEnum.Post.ToString();
            webRequest.Proxy = null;
            using (var reader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
    }

    /// <summary>
    /// 带参数据类型
    /// </summary>
    public enum DataTypeEnum
    {
        Json,
        Xml,
        Form
    }

    /// <summary>
    /// 带参数据类型
    /// </summary>
    public enum MethodTypeEnum
    {
        Get,
        Post
    }
}