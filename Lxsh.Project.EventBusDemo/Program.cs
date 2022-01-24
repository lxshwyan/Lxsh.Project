using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Lxsh.Project.EventBusDemo
{
   public class Program
    {
        static void Main(string[] args)
        {
           // TestReflction();
            var test1 = new TestEventBus1();
            var test2 = new TestEventBus2();
            Console.WriteLine("1111");
            Task.Run(() =>
            {
                Console.WriteLine("2222");
                test1.OnLoginSuccess(new EvOnLoginSuccess(new LoginResultDto { Id = "321", UserType = "1", Success = true }));
                Console.WriteLine("fsfds");
            }       
            );
           EventBus.Instance.Publish<EvOnLoginSuccess>(new EvOnLoginSuccess(new LoginResultDto { Id = "321", UserType = "1", Success = true }));
        }
      public static void  TestReflction()
        {
            ReflectionTest rt = new ReflectionTest();

            MethodInfo mi = rt.GetType().GetMethod("DisplayType");//先获取到DisplayType<T>的MethodInfo反射对象
            mi.MakeGenericMethod(new Type[] { typeof(string) }).Invoke(rt, null);//然后使用MethodInfo反射对象调用ReflectionTest类的DisplayType<T>方法，这时要使用MethodInfo的MakeGenericMethod函数指定函数DisplayType<T>的泛型类型T

            Type myGenericClassType = rt.GetType().GetNestedType("MyGenericClass`1");//这里获取MyGenericClass<T>的Type对象，注意GetNestedType方法的参数要用MyGenericClass`1这种格式才能获得MyGenericClass<T>的Type对象
            myGenericClassType.MakeGenericType(new Type[] { typeof(float) }).GetMethod("DisplayNestedType", BindingFlags.Static | BindingFlags.Public).Invoke(null, null);
            //然后用Type对象的MakeGenericType函数为泛型类MyGenericClass<T>指定泛型T的类型，比如上面我们就用MakeGenericType函数将MyGenericClass<T>指定为了MyGenericClass<float>，然后继续用反射调用MyGenericClass<T>的DisplayNestedType静态方法

            Console.ReadLine();

        }
    }
}
