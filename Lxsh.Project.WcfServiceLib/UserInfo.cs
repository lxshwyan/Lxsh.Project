using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Lxsh.Project.WcfServiceLib
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“Lxsh”。
    public class UserInfo : IUserInfo
    {
        public void DoWork()
        {
            Console.WriteLine("DoWork");
        }

        public List<Student> GetString()
        {
            return new List<Student>() { new Student() {  Name="lxsh",Age=12},new Student() { Name = "test", Age = 22 } };
        }
        
         
    }
}
