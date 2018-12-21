using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
迭代类好处：

1. 读写分离

2. 封装了元数据，比如底层的array数组

3. 简化的业务逻辑    
*/
namespace Lxsh.Project.DesignPattern_Iterator
{
  public  class Program
    {
     
        static void Main(string[] args)
        {
            LxshAggregation<string> aggregation = new LxshAggregation<string>(); 
            aggregation.Add("lisan");
            aggregation.Add("wangwu");
            aggregation.Add("zhangsi");     
            var enumerator = aggregation.GetEnumerator();
            aggregation.Add("maliu");
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }    
        }
    }
}
