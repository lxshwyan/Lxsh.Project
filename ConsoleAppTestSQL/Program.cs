using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ConsoleAppTestSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            #region MyRegion
            try
            {
                GetAll();
                Console.WriteLine("查询完成");

                IFreeSql FreeSqlConnect = new FreeSql.FreeSqlBuilder().UseConnectionString(FreeSql.DataType.SqlServer, System.Configuration.ConfigurationManager.AppSettings["strConn"])
                  // .UseAutoSyncStructure(true) //自动同步实体结构到数据库
                  .Build();
                DataTable row = FreeSqlConnect.Ado.ExecuteDataTable(" SELECT * FROM ABDoorPoliceInfo");
                Console.WriteLine(row.Rows.Count);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
            #endregion

            using (DisposTest disposTest = new DisposTest())
            {
                disposTest.Call();
            }

        }


        /// <summary>
        /// 获取所有列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAll()
        {
            string str = System.Configuration.ConfigurationManager.AppSettings["strConn"];
            DbHelperSQL db = new DbHelperSQL(str);
            string strSql = " SELECT * FROM ABDoorPoliceInfo  ";
            DataSet ds = db.Query(strSql);
            if (ds == null || ds.Tables.Count < 1)
            {
                Console.WriteLine(ds.Tables.Count);
                return null;
            }
            Console.WriteLine(ds.Tables.Count);
            return ds.Tables[0];
        }

       
    }
}
