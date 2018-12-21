/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.DesignPattern_Simplefactory
*文件名： ConnectionFactory
*创建人： Lxsh
*创建时间：2018/12/20 15:05:34
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/20 15:05:34
*修改人：Lxsh
*描述：
************************************************************************/
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;          
using Npgsql;

namespace Lxsh.Project.DesignPattern_Simplefactory
{
    public class ConnectionFactory
    {
        public static IDbConnection CreateConnection(string dbtype, string strConn)
        {
            if (string.IsNullOrEmpty(dbtype))
                throw new ArgumentNullException("获取数据库连接居然不传数据库类型，你在逗我么？");
            if (string.IsNullOrEmpty(strConn))
                throw new ArgumentNullException("获取数据库连接居然不传数据库类型，你在逗我么？");
            var dbType = GetDataBaseType(dbtype);
            return CreateConnection(dbType, strConn);
        }
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <param name="conStr">数据库连接字符串</param>
        /// <returns>数据库连接</returns>
        public static IDbConnection CreateConnection(DatabaseType dbType, string strConn)
        {
            IDbConnection connection = null;
            if (string.IsNullOrEmpty(strConn))
                throw new ArgumentNullException("获取数据库连接居然不传数据库类型，你在逗我么？");

            switch (dbType)
            {
                case DatabaseType.SqlServer:
                    connection = new SqlConnection(strConn);
                    break;
                case DatabaseType.MySQL:
                    connection = new MySqlConnection(strConn);
                    break;
                case DatabaseType.PostgreSQL:
                    connection = new NpgsqlConnection(strConn);
                    break;
                default:
                    throw new ArgumentNullException($"这是我的错，还不支持的{dbType.ToString()}数据库类型");

            }
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            return connection;
        }
        /// <summary>
        /// 转换数据库类型
        /// </summary>
        /// <param name="dbtype">数据库类型字符串</param>
        /// <returns>数据库类型</returns>
        public static DatabaseType GetDataBaseType(string dbtype)
        {
            if (string.IsNullOrEmpty(dbtype))
                throw new ArgumentNullException("获取数据库连接居然不传数据库类型，你在逗我么？");
            DatabaseType returnValue = DatabaseType.SqlServer;
            foreach (DatabaseType dbType in Enum.GetValues(typeof(DatabaseType)))
            {
                if (dbType.ToString().Equals(dbtype, StringComparison.OrdinalIgnoreCase))
                {
                    returnValue = dbType;
                    break;
                }
            }
            return returnValue;
        }

    }
}