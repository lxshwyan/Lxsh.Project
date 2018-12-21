using Lxsh.Project.Model;
using Newtonsoft.Json;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.SqlDemo
{
   public class QuerySql
    {
        public static void init()
        {
            Easy();
        }
        public static void Easy()
        {
            var db = CreateDBInstance.GetInstance();
            var dbTime = db.GetDate();
            dynamic getALL = db.Queryable<Base_User>().ToList();
             getALL = db.Queryable<Base_User>().Select<object>("*").ToList();
             getALL = db.Queryable<Base_User>().Select<object>("UserName,Password").ToList();   //不要加引号  
            var getRandomList = db.Queryable<Base_User>().OrderBy(it => SqlFunc.GetRandom()).ToList();
            var getAllOrder = db.Queryable<Base_User>().OrderBy(it => it.Id).OrderBy(it => it.UserId, OrderByType.Desc).ToList().Take(10);
            //var getId = db.Queryable<Student>().Select(it => it.Id).ToList();
            //var getNew = db.Queryable<Student>().Where(it => it.Id == 1).Select(it => new { id = SqlFunc.IIF(it.Id == 0, 1, it.Id), it.Name, it.SchoolId }).ToList();
            //var getAllNoLock = db.Queryable<Student>().With(SqlWith.NoLock).ToList();
            //var getByPrimaryKey = db.Queryable<Student>().InSingle(2);
            //var getSingleOrDefault = db.Queryable<Student>().Where(it => it.Id == 1).Single();
            //var getFirstOrDefault = db.Queryable<Student>().First();
            var getByWhere = db.Queryable<Base_User>().Where(it => it.Id == 17 || it.UserName == "admin").ToList();
            //var getByWhere2 = db.Queryable<Student>().Where(it => it.Id == DateTime.Now.Year).ToList();
            //var getByFuns = db.Queryable<Student>().Where(it => SqlFunc.IsNullOrEmpty(it.Name)).ToList();
            //var sum = db.Queryable<Student>().Select(it => it.SchoolId).ToList();
            //var sum2 = db.Queryable<Student, School>((st, sc) => st.SchoolId == sc.Id).Sum((st, sc) => sc.Id);
            //var isAny = db.Queryable<Student>().Where(it => it.Id == -1).Any();
            //var isAny2 = db.Queryable<Student>().Any(it => it.Id == -1);
            //var count = db.Queryable<Student>().Count(it => it.Id > 0);
            //var date = db.Queryable<Student>().Where(it => it.CreateTime.Value.Date == DateTime.Now.Date).ToList();
            //var getListByRename = db.Queryable<School>().AS("Student").ToList();
            //var in1 = db.Queryable<Student>().In(it => it.Id, new int[] { 1, 2, 3 }).ToList();
            //var in2 = db.Queryable<Student>().In(new int[] { 1, 2, 3 }).ToList();
            //int[] array = new int[] { 1, 2 };
            //var in3 = db.Queryable<Student>().Where(it => SqlFunc.ContainsArray(array, it.Id)).ToList();
            //var group = db.Queryable<Student>().GroupBy(it => it.Id)
            //    .Having(it => SqlFunc.AggregateCount(it.Id) > 10)
            //    .Select(it => new { id = SqlFunc.AggregateCount(it.Id) }).ToList();

            //var between = db.Queryable<Student>().Where(it => SqlFunc.Between(it.Id, 1, 20)).ToList();

            //var getTodayList = db.Queryable<Student>().Where(it => SqlFunc.DateIsSame(it.CreateTime, DateTime.Now)).ToList();

            //var joinSql = db.Queryable("student", "s").OrderBy("id").Select("id,name").ToPageList(1, 2);

            //var getDay1List = db.Queryable<Student>().Where(it => it.CreateTime.Value.Hour == 1).ToList();
            //var getDateAdd = db.Queryable<Student>().Where(it => it.CreateTime.Value.AddDays(1) == DateTime.Now).ToList();
            //var getDateIsSame = db.Queryable<Student>().Where(it => SqlFunc.DateIsSame(DateTime.Now, DateTime.Now, DateType.Hour)).ToList();

            //var getSqlList = db.Queryable<Student>().AS("(select * from student) t").ToList();


            //var getUnionAllList = db.UnionAll(db.Queryable<Student>().Where(it => it.Id == 1), db.Queryable<Student>().Where(it => it.Id == 2)).ToList();

            //var getUnionAllList2 = db.UnionAll(db.Queryable<Student>(), db.Queryable<Student>()).ToList();

            //var test1 = db.Queryable<Student, School>((st, sc) => st.SchoolId == sc.Id).Where(st => st.CreateTime > SqlFunc.GetDate()).Select((st, sc) => SqlFunc.ToInt64(sc.Id)).ToList();
            //var test2 = db.Queryable<Student, School>((st, sc) => st.SchoolId == sc.Id)
            //          .Where(st =>
            //            SqlFunc.IF(st.Id > 1)
            //                 .Return(st.Id)
            //                 .ElseIF(st.Id == 1)
            //                 .Return(st.SchoolId).End(st.Id) == 1).Select(st => st).ToList();
            //var test3 = db.Queryable<DataTestInfo2>().Select(it => it.Bool1).ToSql();
            //var test4 = db.Queryable<DataTestInfo2>().Select(it => new { b = it.Bool1 }).ToSql();
            //DateTime? result = DateTime.Now;
            //var test5 = db.Queryable<Student>().Where(it => it.CreateTime > result.Value.Date).ToList()  ;
            string strJson = JsonConvert.SerializeObject(getByWhere);
            Console.WriteLine(strJson);
        }
    }
}
