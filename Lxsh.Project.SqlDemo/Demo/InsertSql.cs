using Lxsh.Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.SqlDemo
{
    public class InsertSql
    {
        public static void init()
        {
            var db = CreateDBInstance.GetInstance();
            //   db.IgnoreColumns.Add("Id", "Base_User");
            #region 初始化数据

            for (int i = 0; i < 100000; i++)
            {
                Random rd = new Random();
                int nlogType=rd.Next(0, 3);
                Base_SysLog dBaseUser = new Base_SysLog() { Id =i.ToString(), LogContent="李小双测试内容"+i,
                    LogType =System.Enum.Parse(typeof(EnumType.LogType), nlogType.ToString()).ToString(), OpTime=DateTime.Now, OpUserName="admin"};

                // Insert reutrn Insert Count
                int count = db.Insertable(dBaseUser).ExecuteCommand();
            }

            #endregion


            string strUserName = Guid.NewGuid().ToString();
            var insertObj = new Base_User() {Id=1, UserId = "admin" , UserName = "李小双" + strUserName, Password = "123456", RealName = "李小双", Sex = 0, Birthday = DateTime.Now, DepartmentID = "1" };
                
            // Insert reutrn Insert Count
           var t2 = db.Insertable(insertObj).ExecuteCommand(); 
            insertObj.UserId = "admin" + Guid.NewGuid().ToString();

           ////Insert reutrn Identity Value
            var t3 = db.Insertable(insertObj).ExecuteReturnIdentity();   
            insertObj.UserId = "admin" + Guid.NewGuid().ToString(); 
            ////Insert reutrn Identity Value
            var t31 = db.Insertable(insertObj).ExecuteReturnEntity();

            insertObj.UserId = "admin" + Guid.NewGuid().ToString();
            ////Only  insert  Name and SchoolId
            var t4 = db.Insertable(insertObj).InsertColumns(it => new { it.UserName, it.Password,it.DepartmentID }).ExecuteReturnIdentity();     

            ////Ignore   TestId
            //var t6 = db.Insertable(insertObj).IgnoreColumns(it => it == "Name" || it == "TestId").ExecuteReturnIdentity();


            ////Use Lock
            //var t8 = db.Insertable(insertObj).With(SqlWith.UpdLock).ExecuteCommand();


            //var insertObj2 = new Student() { Name = null, CreateTime = Convert.ToDateTime("2010-1-1") };
            //var t9 = db.Insertable(insertObj2).Where(true/* Is insert null */, false/*off identity*/).ExecuteCommand();

            ////Insert List<T>
            //var insertObjs = new List<Student>();
            //for (int i = 0; i < 1000; i++)
            //{
            //    insertObjs.Add(new Student() { Name = "name" + i });
            //}
            //var t10 = db.Insertable(insertObjs.ToArray()).InsertColumns(it => new { it.Name }).ExecuteCommand();

            //var t11 = db.Insertable(insertObjs.ToArray()).ExecuteCommand();


            //var t12 = db.Insertable(insertObj).IgnoreColumns(it => it == "Name" || it == "TestId").ExecuteReturnIdentityAsync();
            //t12.Wait();


            var dt = new Dictionary<string, object>();
            dt.Add("UserName", "1000");
            dt.Add("Birthday", null);
           var t66 = db.Insertable(dt).AS("Base_User").ExecuteReturnIdentity();
        }   
    }
}
