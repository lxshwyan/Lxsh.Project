
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
Client（客户）：负责创建一个具体的命令（Concrete Command） 
Invoker（调用者）：调用者持有一个命令对象，并在某个时刻调用命令对象的 execute() 方法。 
Command（命令接口）：包含命令对象的 execute() 方法和 undo() 方法。 
ConcreteCommand（具体命令）：实现命令接口。包括两个操作，执行命令和撤销命令。 
Receiver（接收者）：接受命令并执行。
--------------------- 
*/
namespace Lxsh.Project.DesignPattern_Command
{
    class Program
    {
        static void Main(string[] args)
        {
            MiPhone miPhone = new MiPhone();

            LightCommand lightOnCommand = new LightCommand();
            TvCommand tvOnCommand = new TvCommand();
          

            miPhone.setCommand(lightOnCommand);
            miPhone.setCommand(tvOnCommand);

            //开灯
            miPhone.onButtonWasPushed();
            //开电视
            miPhone.onButtonWasPushed();

        }
    }
}
