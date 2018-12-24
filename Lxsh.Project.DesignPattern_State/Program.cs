using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 5. 模式总结

　　5.1 优点

　　　　5.1.1 状态模式将与特定状态相关的行为局部化，并且将不同状态的行为分割开来。

　　　　5.1.2 所有状态相关的代码都存在于某个ConcereteState中，所以通过定义新的子类很容易地增加新的状态和转换。

　　　　5.1.3 状态模式通过把各种状态转移逻辑分不到State的子类之间，来减少相互间的依赖。

　　5.2 缺点

　　　　5.2.1 导致较多的ConcreteState子类

　　5.3 适用场景

　　　　5.3.1 当一个对象的行为取决于它的状态，并且它必须在运行时刻根据状态改变它的行为时，就可以考虑使用状态模式来。

　　　　5.3.2 一个操作中含有庞大的分支结构，并且这些分支决定于对象的状态。
*/
namespace Lxsh.Project.DesignPattern_State
{
    class Program
    {
        static void Main(string[] args)
        {
            // 设置Context的初始状态为ConcreteStateA
            Context context = new Context(new ConcreateStateA());

            // 不断地进行请求，同时更改状态
            for (int i = 0; i < 1000; i++)
            {
                context.Request();
            }
         

            Console.Read();
        }
    }
}
