
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Lxsh.Project.Bussiness.Interface
{
    public interface IBaseService :IDisposable
    {
        #region Insert
        /// <summary>
        ///  单个实体插入
        /// </summary>
        /// <typeparam name="T">新增实体</typeparam>
        /// <param name="t">返回新增实体</param>
        /// <returns></returns>
        T InsertReturnEntity<T>(T t) where T : class, new();

        /// <summary>
        /// 单个实体插入
        /// </summary>
        /// <typeparam int="T">返回插入自增列</typeparam>
        /// <param name="t">实体</param>
        /// <returns></returns>
        int InsertReturnIdentity<T>(T t) where T : class, new();

        /// <summary>
        ///  单个实体插入
        /// </summary>
        /// <typeparam int="T">返回插入影响行数</typeparam>
        /// <param name="t">实体</param>
        /// <returns></returns>
        int InsertReturneCommand<T>(T t) where T : class, new();

        /// <summary>
        ///  批量实体插入
        /// </summary>
        /// <typeparam name="T">返回插入影响行数</typeparam>
        /// <param name="tArray">数组（list<T>.ToArray()）</param>
        /// <returns></returns>
        int InsertReturneCommand<T>(T[] tArray) where T : class, new();

        #endregion

        #region Delete     
        /// <summary>
        /// 删除单个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        int DeleteReturnCommand<T>(T t) where T : class, new();

        /// <summary>
        /// 按条件批量实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="funcWhere"></param>
        /// <returns></returns>
        int DeleteByWhereReturnCommand<T>(Expression<Func<T, bool>> funcWhere) where T : class, new();

        #endregion

        #region Update

        /// <summary>
        ///  单个实体修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">实体</param>
        /// <returns></returns>
        int UpdateReturneCommand<T>(T t) where T : class, new();

        /// <summary>
        ///  批量实体修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tArray">数组（list<T>.ToArray()）</param>
        /// <returns></returns>
        int UpdateReturneCommand<T>(T[] tArray) where T : class, new();

        /// <summary>
        /// 更新实体指定的列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        int UpdateReturneCommand<T>(T t, Expression<Func<T, object>> columns) where T : class, new();

        /// <summary>
        /// 按条件更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="funcWhere"></param>
        /// <returns></returns>
        int UpdateReturneCommand<T>(T t, Expression<Func<T, bool>> funcWhere) where T : class, new();

        #endregion

        #region Query（PartitionBy(st => new { st.Name }).Take(2).OrderBy(st => st.Id, OrderByType.Desc).Select(st => st).ToPageList(1, 1000, ref count);）
        /// <summary>
        ///  查找该实体所有集合（延迟加载的，需要及时加载使用toList）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        ISugarQueryable<T> Query<T>() where T : class, new();

        /// <summary>
        /// 按条件查找实体集合（延迟加载的，需要及时加载使用toList）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="funcWhere">查询条件</param>
        /// <returns></returns>
        ISugarQueryable<T> Query<T>(Expression<Func<T, bool>> funcWhere) where T : class, new();


        #endregion

        #region 直接操作sql语句
        /// <summary>
        ///  返回 List集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        List<T> ExcuteSqlQuery<T>(string sql, SqlParameter[] parameters) where T : class;


        /// <summary>
        ///  返回 DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        DataTable ExcuteSqlQuery<T>(string sql) where T : class;

        /// <summary>
        ///    返回成功或失败
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        bool ExecuteSqlCommand<T>(string sql) where T : class;
       
        #endregion

    }
}