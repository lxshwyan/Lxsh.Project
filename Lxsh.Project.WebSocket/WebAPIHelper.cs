using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.WebSocketDemo
{
   public class WebAPIHelper
    {
        public static string PostResponse(string url, UpLoadFile file, Dictionary<string, string> input, Encoding endoding)
        {
            string boundary = "----WebKitFormBoundary7MA4YWxkTrZu0gW";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.KeepAlive = true;
            request.Expect = "";
            request.ContentType = "multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW";
            MemoryStream stream = new MemoryStream();
            byte[] line = Encoding.ASCII.GetBytes("--" + boundary + "\r\n");
            byte[] enterER = Encoding.ASCII.GetBytes("\r\n");

            ////提交文件
            if (file != null)
            {
                string fformat = "Content-Disposition:form-data; name=\"{0}\";filename=\"{1}\"\r\nContent-Type:{2}\r\n\r\n";
                
                    stream.Write(line, 0, line.Length);        //项目分隔符
                    string s = string.Format(fformat, file.Name, file.FileName, file.Content_Type);
                    byte[] data = Encoding.UTF8.GetBytes(s);
                    stream.Write(data, 0, data.Length);
                    stream.Write(file.Data, 0, file.Data.Length);
                    stream.Write(enterER, 0, enterER.Length);  //添加\r\n
               
            }
            //提交文本字段
            if (input != null)
            {
                stream.Write(line, 0, line.Length);        //项目分隔符
                string format = "--" + boundary + "\r\nContent-Disposition:form-data;name=\"{0}\"\r\n\r\n{1}\r\n";    //自带项目分隔符
                foreach (string key in input.Keys)
                {
                    string s = string.Format(format, key, input[key]);
                    byte[] data = Encoding.UTF8.GetBytes(s);
                    stream.Write(data, 0, data.Length);
                }
            }

            byte[] foot_data = Encoding.UTF8.GetBytes("--" + boundary + "--\r\n");      //项目最后的分隔符字符串需要带上--  
            stream.Write(foot_data, 0, foot_data.Length);
            request.ContentLength = stream.Length;
            Stream requestStream = request.GetRequestStream(); //写入请求数据
            stream.Position = 0L;
            stream.CopyTo(requestStream); 
            stream.Close();
            requestStream.Close();
            try
            {
                HttpWebResponse response;
                try
                {
                    response = (HttpWebResponse)request.GetResponse();

                    try
                    {
                        using (var responseStream = response.GetResponseStream())
                        using (var mstream = new MemoryStream())
                        {
                            responseStream.CopyTo(mstream);
                            string message = endoding.GetString(mstream.ToArray());
                            return message;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                catch (WebException ex)
                {
                    //response = (HttpWebResponse)ex.Response;
                    //if (response.StatusCode == HttpStatusCode.BadRequest)
                    //{
                    //    using (Stream data = response.GetResponseStream())
                    //    {
                    //        using (StreamReader reader = new StreamReader(data))
                    //        {
                    //            string text = reader.ReadToEnd();
                    //            Console.WriteLine(text);
                    //        }
                    //    }
                    //}

                    throw ex;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class UpLoadFile
    {
        public string Name { get; set; }
        public string FileName { get; set; }

        public string Content_Type { get; set; }
        public byte[] Data { get; set; }
    }
}
