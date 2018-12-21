using Lxsh.Project.Bussiness.Interface;
using Lxsh.Project.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.Bussiness.Service
{
    public class BaseService:IBaseService
    {
        #region Identity
        protected SqlSugarClient baseDbSqlClent { private get; set; }
        #endregion Identity

        #region Insert
        /// <summary>
        ///  单个实体插入
        /// </summary>
        /// <typeparam name="T">新增实体</typeparam>
        /// <param name="t">返回新增实体</param>
        /// <returns></returns>
        public T InsertReturnEntity<T>(T t) where T : class, new()
        {
            return this.baseDbSqlClent.Insertable(t).ExecuteReturnEntity();
        }
        /// <summary>
        /// 单个实体插入
        /// </summary>
        /// <typeparam int="T">返回插入自增列</typeparam>
        /// <param name="t">实体</param>
        /// <returns></returns>
        public int InsertReturnIdentity<T>(T t) where T : class, new()
        {
            return this.baseDbSqlClent.Insertable(t).ExecuteReturnIdentity();
        }
        /// <summary>
        ///  单个实体插入
        /// </summary>
        /// <typeparam int="T">返回插入影响行数</typeparam>
        /// <param name="t">实体</param>
        /// <returns></returns>
        public int InsertReturneCommand<T>(T t) where T : class, new()
        {
            return this.baseDbSqlClent.Insertable(t).ExecuteCommand();
        }
        /// <summary>
        ///  批量实体插入
        /// </summary>
        /// <typeparam name="T">返回插入影响行数</typeparam>
        /// <param name="tArray">数组（list<T>.ToArray()）</param>
        /// <returns></returns>
        public int InsertReturneCommand<T>(T[] tArray) where T : class, new()
        {
            return this.baseDbSqlClent.Insertable(tArray).ExecuteCommand();
        }
        #endregion

        #region Delete     
        /// <summary>
        /// 删除单个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public int DeleteReturnCommand<T>(T t) where T : class, new()
        {
            return baseDbSqlClent.Deleteable<T>().Where(t).ExecuteCommand();
        }
        /// <summary>
        /// 按条件批量实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="funcWhere"></param>
        /// <returns></returns>
        public int DeleteByWhereReturnCommand<T>(Expression<Func<T, bool>> funcWhere) where T : class, new()
        {
            return baseDbSqlClent.Deleteable<T>().Where(funcWhere).ExecuteCommand();
        }
        #endregion

        #region Update

        /// <summary>
        ///  单个实体修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">实体</param>
        /// <returns></returns>
        public int UpdateReturneCommand<T>(T t) where T : class, new()
        {
            return this.baseDbSqlClent.Updateable(t).ExecuteCommand();
        }
        /// <summary>
        ///  批量实体修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tArray">数组（list<T>.ToArray()）</param>
        /// <returns></returns>
        public int UpdateReturneCommand<T>(T[] tArray) where T : class, new()
        {
            return this.baseDbSqlClent.Updateable(tArray).ExecuteCommand();
        }
        /// <summary>
        /// 更新实体指定的列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public int UpdateReturneCommand<T>(T t, Expression<Func<T, object>> columns) where T : class, new()
        {
            return this.baseDbSqlClent.Updateable(t).UpdateColumns(columns).ExecuteCommand();
        }
        /// <summary>
        /// 按条件更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="funcWhere"></param>
        /// <returns></returns>
        public int UpdateReturneCommand<T>(T t, Expression<Func<T, bool>> funcWhere) where T : class, new()
        {
            return this.baseDbSqlClent.Updateable(t).Where(funcWhere).ExecuteCommand();
        }
        #endregion

        #region Query（PartitionBy(st => new { st.Name }).Take(2).OrderBy(st => st.Id, OrderByType.Desc).Select(st => st).ToPageList(1, 1000, ref count);）
        /// <summary>
        ///  查找该实体所有集合（延迟加载的，需要及时加载使用toList）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ISugarQueryable<T> Query<T>() where T : class, new()
        {
            return baseDbSqlClent.Queryable<T>();
        }
        /// <summary>
        /// 按条件查找实体集合（延迟加载的，需要及时加载使用toList）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="funcWhere">查询条件</param>
        /// <returns></returns>
        public ISugarQueryable<T> Query<T>(Expression<Func<T, bool>> funcWhere) where T : class, new()
        {
            return baseDbSqlClent.Queryable<T>().Where(funcWhere);
        }
        #endregion

        #region 直接操作sql语句
        /// <summary>
        ///  返回 List集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public List<T> ExcuteSqlQuery<T>(string sql, SqlParameter[] parameters) where T : class
        {

            return this.baseDbSqlClent.Ado.SqlQuery<T>(sql, parameters);
        }

        /// <summary>
        ///  返回 DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable ExcuteSqlQuery<T>(string sql) where T : class
        {
            DataTable dt;
            try
            {
                this.baseDbSqlClent.Ado.BeginTran();
                dt = this.baseDbSqlClent.Ado.GetDataTable(sql);
                this.baseDbSqlClent.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                this.baseDbSqlClent.Ado.RollbackTran();
                throw ex;
            }
            return dt;
        }
        /// <summary>
        ///    返回成功或失败
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool ExecuteSqlCommand<T>(string sql) where T : class
        {
            int nResult = this.baseDbSqlClent.Ado.ExecuteCommand(sql);
            return nResult > 0 ? true : false;
        }
        
        #endregion
        #region Dispose   
        /// <summary>
        /// 释放动作方法
        /// </summary>
        public void Dispose()
        {
           
        }
        #endregion

    }
}
