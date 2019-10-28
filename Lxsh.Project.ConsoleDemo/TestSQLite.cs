/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lxsh.Project.ConsoleDemo.Properties
*文件名： TestSQLite
*创建人： Lxsh
*创建时间：2019/10/17 11:19:19
*描述
*=======================================================================
*修改标记
*修改时间：2019/10/17 11:19:19
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lxsh.Project.ConsoleDemo.Properties
{
   public  class TestSQLite
    {

        public static TestSQLite _instance;
        private static object objLock = new object();

        static TestSQLite()
        {
            if (_instance == null)
            {
                lock (objLock)
                {
                    if (_instance == null)
                        _instance = new TestSQLite();
                }
            }
        }
        public void Insert()
        {
            string strGUID = Guid.NewGuid().ToString();
            string userName = "lxsh";
            string passWord = "123456";
            string strSql = $"insert into UserInfo (UserID,UserName,PassWord )values ( {strGUID},{userName},{passWord})";
            DbHelperSQLite.ExecuteSql(strSql);
            // strSql = $"select *  from  UserInfo";
            //  var date=  DbHelperSQLite.Query(strSql);
        }
    }
}