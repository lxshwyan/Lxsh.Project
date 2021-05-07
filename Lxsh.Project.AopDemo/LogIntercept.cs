using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lxsh.Project.AopDemo
{
    public class LogIntercept : IInterceptor
    {
        public LogIntercept()
        {

        }

        public void Intercept(IInvocation invocation)
        {
            
            //执行原有方法之前
            Console.WriteLine($"{invocation.Method}=>执行开始=>参数为{string.Join(",", invocation.Arguments)}");

            //执行原有方法
            invocation.Proceed();

            //执行原有方法之后
            Console.WriteLine("执行结束");
        }
    }
}
