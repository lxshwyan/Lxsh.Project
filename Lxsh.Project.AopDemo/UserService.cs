using System;
using System.Collections.Generic;
using System.Text;

namespace Lxsh.Project.AopDemo
{

    public interface   IUserService
    {
        void AddUser(string Name, int age);
    }
    public  class UserService: IUserService
    {
        public void AddUser(string Name,int age)
        {
            Console.WriteLine("添加用户完成!"+Environment.NewLine+"fsdfdsf");
        
        }
    }

}
