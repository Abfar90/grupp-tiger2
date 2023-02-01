using System.Configuration;
using Dapper;
using grupp_tiger2.Classes;
using Npgsql;
namespace grupp_tiger2.Data
{
    internal class PostgresDataAccess
    {
        private static List<NpgsqlParameter> parameters;
        static string connectionString = ConfigurationManager.ConnectionStrings["postgres"].ConnectionString;

        public static List<bank_user> LoadBankUsers()
        {
            // This is the connection string from the app.config file, and "postgres" is the name of the connection string
            string connectionString = ConfigurationManager.ConnectionStrings["postgres"].ConnectionString;

            // using is a C# feature that automatically closes the connection when the code inside the block is done
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                // Opens the connection to the database
                conn.Open();

                // This is the Dapper method that executes the query and returns a list of bank_user objects
                var output = conn.Query<bank_user>("select * from bank_user", new DynamicParameters());
                return output.ToList();

            }
        }

        public static List<account> LoadBankAccounts()
        {
            // This is the connection string from the app.config file, and "postgres" is the name of the connection string
            string connectionString = ConfigurationManager.ConnectionStrings["postgres"].ConnectionString;

            // using is a C# feature that automatically closes the connection when the code inside the block is done
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                // Opens the connection to the database
                conn.Open();

                // This is the Dapper method that executes the query and returns a list of bank_user objects
                var output = conn.Query<account>("select * from account", new DynamicParameters());
                return output.ToList();

            }
        }

        public static List<bank_user> LoadBankRoles()
        {
            // This is the connection string from the app.config file, and "postgres" is the name of the connection string
            string connectionString = ConfigurationManager.ConnectionStrings["postgres"].ConnectionString;

            // using is a C# feature that automatically closes the connection when the code inside the block is done
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                // Opens the connection to the database
                conn.Open();

                // This is the Dapper method that executes the query and returns a list of bank_user objects
                var output = conn.Query<bank_user>("select * from bank_user", new DynamicParameters());
                return output.ToList();

            }
        }

        public static void CreateUser(bank_user user)
        {
            
            string connectionString = ConfigurationManager.ConnectionStrings["postgres"].ConnectionString;

            parameters.Add(new NpgsqlParameter("@first_name", user.first_name));
            parameters.Add(new NpgsqlParameter("@last_name", user.last_name));
            parameters.Add(new NpgsqlParameter("@pin_code", user.pin_code));
            parameters.Add(new NpgsqlParameter("@role_id", user.roleID));
            parameters.Add(new NpgsqlParameter("@branch_id", user.branchID));
            parameters.Add(new NpgsqlParameter("@username", user.username));
            


            using (var conn = new NpgsqlConnection(connectionString))
            {
                
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;

                    foreach (NpgsqlParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }

                    cmd.CommandText = "INSERT INTO \"public\".\"bank_user\" (\"first_name\", \"last_name\", \"pin_code\", \"role_id\", \"branch_id\", \"username\") VALUES (@first_name, @last_name, @pin_code, @roleID, @branchID, @userName;";

                    cmd.ExecuteNonQuery();

                }

                
            }
        }
    }
}