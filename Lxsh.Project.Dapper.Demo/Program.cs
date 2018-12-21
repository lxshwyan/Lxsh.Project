using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Lxsh.Project.Dapper.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
        }
        static void Insert()
        {
            IDbConnection  connection=  new SqlConnection("Data Source = '.'; Initial Catalog = 'UniDataXM'; User ID = 'sa'; Password = '123456'; MultipleActiveResultSets = 'true'");
            var result = connection.Execute("Insert into Users values (@UserName, @Email, @Address)",
                                    new { UserName = "jack", Email = "380234234@qq.com", Address = "上海" });

        }
    }
}
