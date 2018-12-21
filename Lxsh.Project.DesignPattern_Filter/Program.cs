using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.DesignPattern_Filter
{
    class Program
    {
        static void Main(string[] args)
        {
            var students = new List<Student>();

            students.Add(new Student() { Age = 25,Name = "lxsh", ClassID = 1 });
            students.Add(new Student() { Age = 10,Name = "wyan", ClassID = 2 });

            List<IFilter> filters= new List<IFilter>()
            {  
                 new AgeFilter(),
                 new ClassFilter()
            };
              
            // AndFilter andFilter = new AndFilter(filters);

            // var list = andFilter.Filter(students);

            OrFilter orFilter = new OrFilter(filters);
            var list = orFilter.Filter(students);
            foreach (var item in list)
            {
                Console.WriteLine(item.Name);
            }
         
        }
    }
}
