
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Configuration;

using Lxsh.Project.Common.Log;

namespace Lxsh.Project.Bussiness.Service
{
    public class CreateDBInstance
    {
        private static Logger logger = Logger.CreateLogger(typeof(CreateDBInstance));
        public static SqlSugarClient GetInstance()
        {

            string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            DbType dbType = DbType.SqlServer;
            if (string.IsNullOrEmpty(ConnectionString))
            {
                throw new Exception("未配置数据库连接字符串！");
            }
            string nDbType = ConfigurationManager.AppSettings["DbType"];

            if (string.IsNullOrEmpty(ConnectionString))
            {
                switch (nDbType)
                {
                    case "0": dbType = DbType.MySql; break;
                    case "1": dbType = DbType.SqlServer; break;
                    case "2": dbType = DbType.Sqlite; break;
                    case "3": dbType = DbType.Oracle; break;
                    case "4": dbType = DbType.PostgreSQL; break;
                    default: throw new Exception("未配置数据库！");
                }
            }
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig() { ConnectionString = ConnectionString, DbType = dbType, IsAutoCloseConnection = true });
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                string strInfo = sql + "\r\n" + db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value));
                logger.Info(strInfo);
                Console.WriteLine(strInfo);
                Console.WriteLine();
            };
            db.Aop.OnLogExecuted = (sql, pars) =>
            {

            };
            db.Aop.OnLogExecuting = (sql, pars) =>
            {

            };
            db.Aop.OnError = (exp) =>
            {
                logger.Info(exp.Message);
                Console.WriteLine(exp.Message);
                Console.WriteLine();
            };
          //  db.Aop.OnExecutingChangeSql = (sql, pars) =>
          //  {
          //      return new KeyValuePair<string, SugarParameter[]>(sql, pars);
          //  };
          //  db.QueryFilter .Add(new SqlFilterItem()
          // {
          //     FilterValue = filterDb =>
          //     {
          //         return new SqlFilterResult() { Sql = " isDelete=0" };
          //     },
          //     IsJoinQuery = false
          // }).Add(new SqlFilterItem()
          // {
          //     FilterValue = filterDb =>
          //     {
          //         return new SqlFilterResult() { Sql = " f.isDelete=0" };
          //     },
          //     IsJoinQuery = true
          // })
          //.Add(new SqlFilterItem()
          //{
          //    FilterName = "query1",
          //    FilterValue = filterDb =>
          //    {
          //        return new SqlFilterResult() { Sql = " id>@id", Parameters = new { id = 1 } };
          //    },
          //    IsJoinQuery = false
          //});

          //  //Processing prior to execution of SQL
          //  db.Ado.ProcessingEventStartingSQL = (sql, par) =>
          //  {
          //      if (sql.Contains("{0}"))
          //      {
          //          sql = string.Format(sql, "1");
          //      }
          //      return new KeyValuePair<string, SugarParameter[]>(sql, par);
          //  };
            return db;
        }
    }
}