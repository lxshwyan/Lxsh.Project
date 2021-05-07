using Castle.DynamicProxy;
using System;

namespace Lxsh.Project.AopDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //先实例化一个代理类生成器
            ProxyGenerator generator = new ProxyGenerator();
            var u = generator.CreateInterfaceProxyWithTarget<IUserService>(new UserService(), new LogIntercept());
            u.AddUser("LXSH",18);
            Console.ReadLine();

        }
    }
}
