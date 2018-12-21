using Lxsh.Project.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.SqlDemo
{
   public class DeleteSql
    {
        public static void init()
        {
            var db = CreateDBInstance.GetInstance();
        

            //by entity
            var t1 = db.Deleteable<Base_User>().Where(new Base_User() { UserId  = "admin30d2c146-3b34-4fdf-be18-f9e066103cb4" }).ExecuteCommand();

            //use lock
            var t2 = db.Deleteable<Base_User>().With(SqlWith.RowLock).ExecuteCommand();


            //by primary key
            var t3 = db.Deleteable<Base_User>().In(1).ExecuteCommand();

            //by primary key array
            var t4 = db.Deleteable<Base_User>().In(new int[] { 1, 2 }).ExecuteCommand();
            var t41 = db.Deleteable<Base_User>().In(new int[] { 1, 2 }.Select(it => it)).ExecuteCommand();
            var t42 = db.Deleteable<Base_User>().In(new int[] { 1, 2 }.AsEnumerable()).ExecuteCommand();

            //by expression   id>1 and id==1
            var t5 = db.Deleteable<Base_User>().Where(it => it.Id > 1).Where(it => it.Id == 1).ExecuteCommand();

            var t6 = db.Deleteable<Base_User>().AS("Base_User").Where(it => it.Id > 1).Where(it => it.Id == 1).ExecuteCommandAsync();
            t6.Wait();
        }
    }
}
