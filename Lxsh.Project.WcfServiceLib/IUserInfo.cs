using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Lxsh.Project.WcfServiceLib
{
   
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“ILxsh”。
    [ServiceContract]
    public interface IUserInfo
    {
        [OperationContract]
        void DoWork();
        [OperationContract]
        List<Student> GetString();
    }
    public class Student
    {
        public string  Name { get; set; }
        public int Age { get; set; }
    }
}
