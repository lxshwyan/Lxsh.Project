using System;
using System.Collections.Generic;
using System.Text;

namespace Lxsh.Project.EventBusDemo
{
   public class ReflectionTest
    {
        public void DisPlayType<T>()
        {
            Console.WriteLine(typeof(T).FullName);
            Console.WriteLine(typeof(T).ToString());
        }
        //泛型类MyGenericClass有个静态函数DisplayNestedType
        public class MyGenericClass<T>
        {
            public static void DisplayNestedType()
            {
                Console.WriteLine(typeof(T).FullName);
                Console.WriteLine(typeof(T).ToString());
            }
        }
    }
}
