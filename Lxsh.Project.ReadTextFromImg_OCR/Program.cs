using Spire.OCR;
using System;
using System.Collections.Generic;
using System.IO;

namespace Lxsh.Project.ReadTextFromImg_OCR
{
    class Program
    {
        static void Main(string[] args)
        {
            List<UserInfo> users = new List<UserInfo>()
            {
               new UserInfo(){  Id=1,Name="测试1"},
               new UserInfo(){  Id=2,Name="测试2"},
               new UserInfo(){  Id=3,Name="测试3"},
            };
            foreach (var item in users)
            {
                string s = string.Format("sfs{0}");
            }
            if (args!=null&& args.Length>0)
            {
                Console.WriteLine(args[0]);

            }
            if (args != null && args.Length > 1)
            {
                Console.WriteLine(args[1]);

            }
            Console.WriteLine("完成");
            Console.ReadLine();
        }
    }
    public class UserInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
