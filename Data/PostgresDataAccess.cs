using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using Dapper;
using grupp_tiger2.Classes;
using Npgsql;
using Spectre.Console;
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

        public static List<bank_account> LoadBankAccounts()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["postgres"].ConnectionString;

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                var output = conn.Query<bank_account>("select * from bank_account", new DynamicParameters());
                return output.ToList();

            }
        }

        public static List<bank_user> LoadBankRoles()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["postgres"].ConnectionString;

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                var output = conn.Query<bank_user>("select * from bank_user", new DynamicParameters());
                return output.ToList();
            }
        }

        public static List<bank_transactions> LoadTransactions()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["postgres"].ConnectionString;

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                var output = conn.Query<bank_transactions>("select * from bank_transactions order by id desc limit 10;", new DynamicParameters());
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
                                      ($"UPDATE bank_account SET balance = balance - '{amount}' WHERE account_id = '{from_account}' AND user_id = '{id}'; ") +
                                      ($"UPDATE bank_account SET balance = balance + '{amount}' WHERE account_id = '{to_account}'; ") +
                                      "COMMIT;";

                    DateTime timeOfTransaction = DateTime.Now;
                    bank_transactions log = new bank_transactions(id, from_account, to_account, timeOfTransaction.ToString(), amount);
                    logTransfer(log);

                    AnsiConsole.Progress()
                            .AutoClear(true)
                            .StartAsync(async ctx =>
                            {
                                // Define tasks
                                var task1 = ctx.AddTask("[green]Connecting to the server...[/]");

                                while (!ctx.IsFinished)
                                {
                                    await Task.Delay(80);

                                    task1.Increment(2.8);
                                    
                                }
                                
                            });

                    Thread.Sleep(4000);
                    cmd.ExecuteNonQuery();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nTransaction Complete.\n");
                    Console.ResetColor();
                    Console.WriteLine("Press any key to return to the menu.");
                    Console.ReadKey();

                }
            }
        }

        //Create a function to open a new savings account for a user and add ask for the initial deposit
        public static void CreateSavingsAccount(int id, double initialDeposit)
        {
            string connString = ConfigurationManager.ConnectionStrings["postgres"].ConnectionString;

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO bank_account (user_id, balance, name, interest_rate, currency_id) VALUES (@id, @initialDeposit, 'Savings', 0.01, 5);";

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@initialDeposit", initialDeposit);

                    cmd.ExecuteNonQuery();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nYour SmartSave account with 1% per year are created.");
                    Console.ResetColor();
                    Console.WriteLine("\nPress any key to return to the menu.");
                    Console.ReadKey();
                }
            }
        }

        public static void logTransfer(bank_transactions log)
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
                        ($"VALUES ('{log.from_account_id}', '{log.to_account_id}', '{log.timestamp}', '{log.amount}');");

                    cmd.ExecuteNonQuery();
                    
                }
            }
        }

        public static void CreateUser(bank_user user)
        {
            //parameters.Add(new NpgsqlParameter("@first_name", user.first_name));
            //parameters.Add(new NpgsqlParameter("@last_name", user.last_name));
            //parameters.Add(new NpgsqlParameter("@pin_code", user.pin_code));
            //parameters.Add(new NpgsqlParameter("@role_id", user.roleID));
            //parameters.Add(new NpgsqlParameter("@branch_id", user.branchID));
            //parameters.Add(new NpgsqlParameter("@username", user.username));

            string connectionString = ConfigurationManager.ConnectionStrings["postgres"].ConnectionString;

            using (var conn = new NpgsqlConnection(connectionString))
            {
                
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;


                    //cmd.Parameters.AddWithValue("@firstname", user.first_name);
                    //cmd.Parameters.AddWithValue("@lastname", user.last_name);
                    //cmd.Parameters.AddWithValue("@pincode", user.pin_code);
                    //cmd.Parameters.AddWithValue("@roleid", user.role_id);
                    //cmd.Parameters.AddWithValue("@branchid", user.branch_id);
                    //cmd.Parameters.AddWithValue("@username", user.username);

                    //cmd.CommandText = "INSERT INTO \"public\".\"bank_transactions\" " +
                    //    "(\"from_account_id\", \"to_account_id\", \"timestamp\", \"amount\") " +
                    //    "VALUES (@from_account, @to_account, @timestamp, @amount);";

                    //cmd.CommandText = "INSERT INTO \"public\".\"bank_user\" (\"first_name\", \"last_name\", \"pin_code\", \"role_id\", \"branch_id\"," +
                    //    " \"username\") VALUES (@firstname, @lastname, @pincode, @roleid, @branchid, @username);";

                    cmd.CommandText = "INSERT INTO \"public\".\"bank_user\" (\"first_name\", \"last_name\", \"pin_code\", \"role_id\", \"branch_id\", \"username\") " +
                        ($"VALUES ('{user.first_name}', '{user.last_name}', '{user.pin_code}', '{user.role_id}', '{user.branch_id}', '{user.username}');");


                    cmd.ExecuteNonQuery();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nNew user registered.\n");
                    Console.ResetColor();
                    Console.WriteLine("Press any key to return to the menu.");
                    Console.ReadKey();
                }
            }
        }

        public static void TakeLoan(bank_user user, double amount)
        {
            string connString = ConfigurationManager.ConnectionStrings["postgres"].ConnectionString;

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;

                    string interestRate = "0.12";

                    cmd.CommandText = "BEGIN; " +
                                      "INSERT INTO \"public\".\"bank_loan\" " +
                                      "(\"name\", \"interest_rate\", \"user_id\", \"amount\") " +
                                      ($"VALUES ('ExpressLoan', '{interestRate}', '{user.id}', '{amount}');") +
                                      ($"UPDATE bank_account SET balance = balance + '{amount}' WHERE bank_account.name = 'Savings' AND user_id = '{user.id}'; ") +
                                      "COMMIT;";

                    cmd.ExecuteNonQuery();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nNew loan registered.\n");
                    Console.ResetColor();
                    Console.WriteLine($"You will have to pay {amount * 0.12} SEK in interest for this loan.");
                    Console.WriteLine();
                    Console.WriteLine("Press any key to return to the menu.");
                    Console.ReadKey();
                }

            }
        }

        public static void ChangeCurrency(double newRate, int currencyID)
        {
            string connString = ConfigurationManager.ConnectionStrings["postgres"].ConnectionString;

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    
                    cmd.CommandText = $"UPDATE bank_currency SET exchange_rate = '{newRate}' WHERE id = '{currencyID}';";

                    cmd.ExecuteNonQuery();

                }
                
            }

        }
    }
}
                 