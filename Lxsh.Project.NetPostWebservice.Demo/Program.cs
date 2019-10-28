using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Lxsh.Project.NetPostWebservice.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var m = "m=" + JsonConvert.SerializeObject(new { age = 1, name = "jxp" });
        // var a = HttpHelper.PostRequest("http://localhost/DbService/DbWs.asmx/HelloWorld", DataTypeEnum.Form, m);
          string   a= "<?xml version='1.0' encoding='utf - 8'?><string xmlns='http://tempuri.org/'>Hello World!</string>";
            //var a = HttpHelper.PostRequest("http://localhost/DbService/DbWs.asmx/HelloWorld");
            var xmldoc = new XmlDocument();
            xmldoc.LoadXml(a);
            var nsMgr = new XmlNamespaceManager(xmldoc.NameTable); nsMgr.AddNamespace("ns", "http://tempuri.org/");
           XmlNode errorNode = xmldoc.SelectSingleNode("/ns:string", nsMgr);
          
            var b = errorNode.InnerXml;
            Console.WriteLine(b);
            Console.ReadKey();
        }
    }
}
