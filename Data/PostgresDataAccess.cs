using System.ComponentModel.DataAnnotations;
using System.Configuration;
using Dapper;
using grupp_tiger2.Classes;
using Npgsql;
namespace grupp_tiger2.Data
{
    internal class PostgresDataAccess
    {
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

        public static void Transfer(int from_account, int to_account, double amount, int id)
        {
            string connString = ConfigurationManager.ConnectionStrings["postgres"].ConnectionString;

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "BEGIN; " +
                                      "UPDATE account SET balance = balance - @amount WHERE account_id = @from_account AND user_id = @id; " +
                                      "UPDATE account SET balance = balance + @amount WHERE account_id = @to_account; " +
                                      "COMMIT;";

                    cmd.Parameters.AddWithValue("@from_account", from_account);
                    cmd.Parameters.AddWithValue("@to_account", to_account);
                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.Parameters.AddWithValue("@id", id);

                    DateTime timeOfTransaction = DateTime.Now;
                    transaction log = new transaction(id, from_account, to_account, timeOfTransaction.ToString(), amount);
                    logTransfer(log);

                    cmd.ExecuteNonQuery();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Transaction Complete.");
                    Console.ResetColor();
                    Console.WriteLine("Press any key to return to the menu.");
                    Console.ReadKey();
                }
            }
        }

        public static void logTransfer(transaction log)
        {
            string connString = ConfigurationManager.ConnectionStrings["postgres"].ConnectionString;

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "INSERT INTO \"public\".\"bank_transactions\" " +
                        "(\"from_account_id\", \"to_account_id\", \"timestamp\", \"amount\") " +
                        "VALUES (@from_account, @to_account, @timestamp, @amount);";

                    cmd.Parameters.AddWithValue("@id", log.id);
                    cmd.Parameters.AddWithValue("@from_account", log.from_account_id);
                    cmd.Parameters.AddWithValue("@to_account", log.to_account_id);
                    cmd.Parameters.AddWithValue("@timestamp", log.timestamp);
                    cmd.Parameters.AddWithValue("@amount", log.amount);

                    cmd.ExecuteNonQuery();
                    
                }
            }
        }
    }
}