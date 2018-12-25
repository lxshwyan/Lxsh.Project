using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * 责任链模式
顾名思义，责任链模式（Chain of Responsibility Pattern）为请求创建了一个接收者对象的链。这种模式给予请求的类型，对请求的发送者和接收者进行解耦。这种类型的设计模式属于行为型模式。

在这种模式中，通常每个接收者都包含对另一个接收者的引用。如果一个对象不能处理该请求，那么它会把相同的请求传给下一个接收者，依此类推。
 */
namespace Lxsh.Project.DesignPattern_Chain
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcreateHandleA concreateHandleA = new ConcreateHandleA();
            ConcreateHandleB concreateHandleB = new ConcreateHandleB();
            ConcreateHandleC concreateHandleC = new ConcreateHandleC();

            concreateHandleA.Hander = concreateHandleB;
            concreateHandleB.Hander = concreateHandleC;

            concreateHandleA.Request(new ContentMsg()
            {
                Name = "lxsh",
                Day = 3,
                Msg = "流程测试"
            });

        }
    }
}
