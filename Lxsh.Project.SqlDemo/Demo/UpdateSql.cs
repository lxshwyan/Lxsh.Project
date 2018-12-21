using Lxsh.Project.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.SqlDemo
{
    public class UpdateSql
    {
        public static void init()
        {
            var db = CreateDBInstance.GetInstance();

            var updateObj = new Base_User() { Id = 1, UserId = "lxsh", DepartmentID = "0",  Birthday = Convert.ToDateTime("2017-05-21 09:56:12.610") };
            var updateObjs = new List<Base_User>() { updateObj, new Base_User() { Id = 2, UserId = "sun", DepartmentID = "0" } }.ToArray();
            //update reutrn Update Count
            var t1 = db.Updateable(updateObj).ExecuteCommand();

            //Only  update  Name 
            updateObj = new Base_User() { Id = 1, UserId = "admin",Sex=1, RealName="李飞", DepartmentID = "10", Birthday = Convert.ToDateTime("2018-05-21 09:56:12.610") };   
            var t3 = db.Updateable(updateObj).UpdateColumns(it => new { it.Sex ,it.RealName}).ExecuteCommand(); 
            var t3_1 = db.Updateable(updateObj).UpdateColumns(it => it == "DepartmentID").ExecuteCommand();


            //Ignore  Name and TestId
            var t4 = db.Updateable(updateObj).IgnoreColumns(it => new { it.UserName, it.Id }).ExecuteCommand();

            //Ignore  Name and TestId
            var t5 = db.Updateable(updateObj).IgnoreColumns(it => it == "UserName" || it == "Id").With(SqlWith.UpdLock).ExecuteCommand();


            ////Use Lock
            //var t6 = db.Updateable(updateObj).With(SqlWith.UpdLock).ExecuteCommand();

            ////update List<T>
            //var t7 = db.Updateable(updateObjs).ExecuteCommand();

            ////Re Set Value
            //var t8 = db.Updateable(updateObj)
            //    .ReSetValue(it => it.Name == (it.Name + 1)).ExecuteCommand();

            ////Where By Expression
            //var t9 = db.Updateable(updateObj).Where(it => it.Id == 1).ExecuteCommand();

            ////Update By Expression  Where By Expression
            //var t10 = db.Updateable<Student>()
            //    .UpdateColumns(it => new Student() { Name = "a", CreateTime = DateTime.Now })
            //    .Where(it => it.Id == 11).ExecuteCommand();

            ////Rename 
            //db.Updateable<School>().AS("Student").UpdateColumns(it => new School() { Name = "jack" }).Where(it => it.Id == 1).ExecuteCommand();
            ////Update Student set Name='jack' Where Id=1

            ////Column is null no update
            //db.Updateable(updateObj).Where(true).ExecuteCommand();

            ////sql
            //db.Updateable(updateObj).Where("id=@x", new { x = "1" }).ExecuteCommand();
            //db.Updateable(updateObj).Where("id", "=", 1).ExecuteCommand();
            //var t12 = db.Updateable<School>().AS("Student").UpdateColumns(it => new School() { Name = "jack" }).Where(it => it.Id == 1).ExecuteCommandAsync();
            //t12.Wait();

            ////update one columns
            //var count = db.Updateable<Student>().UpdateColumns(it => it.SchoolId == it.SchoolId).Where(it => it.Id == it.Id + 1).ExecuteCommand();


            ////update one columns
            //var count2 = db.Updateable<Student>().UpdateColumns(it => it.SchoolId == it.SchoolId + 1).Where(it => it.Id == it.Id + 1).ExecuteCommand();

            //var dt = new Dictionary<string, object>();
            //dt.Add("id", 1);
            //dt.Add("name", null);
            //dt.Add("createTime", DateTime.Now);
            //var t66 = db.Updateable(dt).AS("student").With(SqlWith.UpdLock).ExecuteCommand();
        }
       
    }
}
