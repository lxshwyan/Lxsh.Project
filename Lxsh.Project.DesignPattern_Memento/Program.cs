using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*   
备忘录模式中主要有三类角色： 
发起人角色：记录当前时刻的内部状态，负责创建和恢复备忘录数据。
备忘录角色：负责存储发起人对象的内部状态，在进行恢复时提供给发起人需要的状态。
管理者角色：负责保存备忘录对象。
*/
namespace Lxsh.Project.DesignPattern_Memento
{
    class Program
    {
        static void Main(string[] args)
        {
            var persons = new List<ContactPerson>
        {
                new ContactPerson { Name= "Learning Hard", MobileNum = "123445"},
                new ContactPerson { Name = "Tony", MobileNum = "234565"},
                new ContactPerson { Name = "Jock", MobileNum = "231455"}
            };
            var mobileOwner = new MobileOwner(persons);
            mobileOwner.Show();

            // 创建备忘录并保存备忘录对象
            var caretaker = new Caretaker { ContactM = mobileOwner.CreateMemento() };

            // 更改发起人联系人列表
            Console.WriteLine("----移除最后一个联系人--------");
            mobileOwner.ContactPersons.RemoveAt(2);
            mobileOwner.Show();

            // 恢复到原始状态
            Console.WriteLine("-------恢复联系人列表------");
            mobileOwner.RestoreMemento(caretaker.ContactM);
            mobileOwner.Show();

            Console.Read();


        }
    }
}
