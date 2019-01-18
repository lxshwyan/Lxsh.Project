using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dapper;

namespace Lxsh.Project.Dapper.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            test(new string[]{"fsf","fsfs"});
           // new Program().GetPid(8888);
           // Test test = new Test() { MyProperty = "111" };

            //  Test test1 = new Test() { MyProperty = "111" };

        }

        static void test(params string[] s)
        {
            foreach (var VARIABLE in s)
            {
                Console.WriteLine(VARIABLE);
            }
        }

        static void Insert()
        {
            IDbConnection  connection=  new SqlConnection("Data Source = '.'; Initial Catalog = 'UniDataXM'; User ID = 'sa'; Password = '123456'; MultipleActiveResultSets = 'true'");
            var result = connection.Execute("Insert into Users values (@UserName, @Email, @Address)",
                                    new { UserName = "jack", Email = "380234234@qq.com", Address = "上海" });  
        }
        public async Task<int> GetVAsync(int t)
        {
            Console.WriteLine("调用开始");
             await Task.Run(() => { System.Threading.Thread.Sleep(10000); return t; });  
            Console.WriteLine("调用完成");

            return 1;
        }
        public string GetPid(int nPort)
        {    
                string pid = "-1";
                Process pro = new Process();
            List<int> ports = new List<int>();
            pro.StartInfo.FileName = "cmd.exe";  
                pro.StartInfo.UseShellExecute = false;  
                pro.StartInfo.RedirectStandardInput = true; 
                pro.StartInfo.RedirectStandardOutput = true; 
                pro.StartInfo.RedirectStandardError = true;  
                pro.StartInfo.CreateNoWindow = true; 
                pro.Start();     
                pro.StandardInput.WriteLine("netstat -ano");
                pro.StandardInput.WriteLine("exit");
                Regex reg = new Regex("\\s+", RegexOptions.Compiled); 
                string line = null; 
                while ((line = pro.StandardOutput.ReadLine()) != null)  
                {      
                   if (line.StartsWith("UDP", StringComparison.OrdinalIgnoreCase))  
                    {
                       line = reg.Replace(line, ","); 
                        string[] arr = line.Split(',');  
                        string soc = arr[1];   
                         int pos = soc.LastIndexOf(':');   
                         int pot = int.Parse(soc.Substring(pos + 1));
                         ports.Add(pot);
                            if (nPort== pot)
                            {
                              pid = arr[4];
                              break;
                            }
                        }

                    }  
                 pro.Close();
                 Console.WriteLine(pid);
                 return pid;
        }

        
    }
}
