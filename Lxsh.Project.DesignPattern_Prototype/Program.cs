using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.DesignPattern_Prototype
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Student student = new Student() {
                Name = "李小双",
                Age = 18,
                studentclass = new StudentClass() {
                    ClassID = 1,
                    ClassName = "一班"
                }, 
              };

            #region 浅拷贝2次 实现深拷贝
            Student student2 = (Student)student.Clone();       //浅拷贝
            student2.studentclass = (StudentClass)student.studentclass.Clone();   //浅拷贝2次 实现深拷贝
            student2.studentclass.ClassName = "二班";
            Console.WriteLine(student.studentclass.ClassName);
            Console.WriteLine(student2.studentclass.ClassName);
            #endregion

            #region 序列化深拷贝
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, student);
            ms.Seek(0, System.IO.SeekOrigin.Begin);
            var student3 = (Student)bf.Deserialize(ms);
            ms.Close();
            #endregion

        }
    }
}
