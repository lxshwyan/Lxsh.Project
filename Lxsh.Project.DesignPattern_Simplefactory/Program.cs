using System;
/* 一：简单工厂模式

   创建型的设计模式

   工厂里面：将类的创建放在一个工厂里面。 屏蔽了客户端和创建逻辑。
*/
namespace Lxsh.Project.DesignPattern_Simplefactory
{
    class Program
    {
        static void Main(string[] args)
        {
            IDbConnection dbConnection= ConnectionFactory.CreateConnection(DatabaseType.SqlServer, "Data Source='.';Initial Catalog='UniDataXM';User ID='sa';Password='123456';MultipleActiveResultSets='true'");
            Console.WriteLine("Hello World!");
        }
    }
}
