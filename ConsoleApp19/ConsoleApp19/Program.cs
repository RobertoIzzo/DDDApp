using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ConsoleApp19
{
    class Program
    {
        public class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class Setting
        {
            public int id { get; set; }
            public string language { get; set; }
            public string email { get; set; }
            public int customerId { get; set; }
        }

        static void Main(string[] args)
        {

            var connectionString =
                System.Configuration.ConfigurationManager.
                    ConnectionStrings["TestDapper"].ConnectionString;
            string getUsers = @"SELECT TOP 5 * FROM Test.dbo.Users;";
            string getUser = "SELECT * FROM Test.dbo.Users WHERE Id = @Id;";
            string insertUser = "INSERT INTO Test.dbo.Users (Name) Values (@Name);";
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {

                    var users = connection.Query<User>(getUsers).ToList();
                    var orderDetail = connection.QueryFirstOrDefault<User>(getUser, new { Id = 38 });
                    var affectedRows = connection.Execute(insertUser, new { Name = "Mark" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
