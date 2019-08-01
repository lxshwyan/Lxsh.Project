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

 * 我们知道责任链中有两个行为：一个是承担责任，一个是把责任推给下家。

纯的责任链模式：要求两个行为中只执行一个，要么承担责任，要么把责任推给下家。不能存在既承担部分责任，又把责任推给下家的情况。

不纯的责任链模式：就是即承担部分责任，又把责任推给下家。当然也有可能出现没有对象承担责任的情况。

在抽象处理者(Handler)角色中，我们采用的是纯的责任链模式，但是这种情况在现实生活中很难找到。
像场景一聚餐费用 与场景二拦截器应用现实生活中均是不纯的责任链模式。
 */
namespace Lxsh.Project.DesignPattern_Chain
{
    class Program
    {
        static void Main(string[] args)
        {    
            HandlerFactory.getABCHandler().Request(new ContentMsg()
            {
                Name = "lxsh",
                Day = 3,
                Msg = "流程测试"
            });
           
        }
    }
}
