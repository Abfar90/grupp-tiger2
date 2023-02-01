using grupp_tiger2.Classes;
using grupp_tiger2.Data;

namespace grupp_tiger2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var bankUsers = PostgresDataAccess.LoadBankUsers();

            int loginCounter = 0;
            bool correctLogin = false;
            bool userFound = false;

            while (correctLogin == false)
            {
                Console.WriteLine("Welcome to the Tiger bank");

                Console.WriteLine("Please insert your username.");
                string userName = Console.ReadLine();

                Console.WriteLine("Please insert your password.");
                string password = Console.ReadLine();

                if (int.TryParse(password, out int passWord) && userName.Length == 4)
                {
                    foreach (var bankUser in bankUsers)
                    {
                        if (userName == bankUser.username && password == bankUser.pin_code)
                        {
                            userFound = true;
                            bank_user user = bankUser;
                            mainMenu(user);
                            correctLogin = true;
                            break;
                        }
                    }
                    if (userFound == false)
                    {
                        loginCounter++;
                        Console.WriteLine("Sorry, the entered username or password is incorrect.");
                        Console.WriteLine("Please try again.");
                        correctLogin = false;
                    }
                }
                else if (userName.Length != 4)
                {
                    Console.WriteLine("Sorry, your username must contain FOUR characters");
                    loginCounter++;
                }
                else if (!int.TryParse(password, out int pass_word))
                {
                    Console.WriteLine("Sorry, your password must only contain NUMBERS.");
                    loginCounter++;
                }
                if (loginCounter >= 3)
                {
                    Console.WriteLine("TOO MANY FAILED ATTEMPTS.");
                    Console.WriteLine("You are now prevented from logging in for 3 minutes.");
                    Thread.Sleep(6000);
                    loginCounter = 0;
                }
            }



            void mainMenu(bank_user user)
            {
                Console.Clear();

                List<string> main_Menu = new List<string>()
                {
                    "Your accounts and account balance",
                    "Transfer",
                    "Show transfer log",
                    "Return to login",
                    "Exit"
                };

                bool[] choices = { true, false, false, false, false };

                int x = 0;

                int userId = user.id;

                bool showMenu = true;
                while (showMenu)
                {
                    Console.WriteLine("Welcome " + user.first_name + " " + user.last_name + " to the Main Menu.");
                    Console.WriteLine("\n(You can navigate through the menu with the 'up' and 'down' arrow keys): \n");

                    //Booleans bestämmer vilket menyval som är markerat
                    if (choices[0] == true)
                    {
                        Console.WriteLine("[ " + main_Menu[0] + " ]");
                    }
                    else if (choices[0] == false)
                    {
                        Console.WriteLine(" " + " " + main_Menu[0]);
                    }
                    if (choices[1] == true)
                    {
                        Console.WriteLine("[ " + main_Menu[1] + " ]");
                    }
                    else if (choices[1] == false)
                    {
                        Console.WriteLine(" " + " " + main_Menu[1]);
                    }
                    if (choices[2] == true)
                    {
                        Console.WriteLine("[ " + main_Menu[2] + " ]");
                    }
                    else if (choices[2] == false)
                    {
                        Console.WriteLine(" " + " " + main_Menu[2]);
                    }
                    if (choices[3] == true)
                    {
                        Console.WriteLine("[ " + main_Menu[3] + " ]");
                    }
                    else if (choices[3] == false)
                    {
                        Console.WriteLine(" " + " " + main_Menu[3]);
                    }
                    if (choices[4] == true)
                    {
                        Console.WriteLine("[ " + main_Menu[4] + " ]");
                    }
                    else if (choices[4] == false)
                    {
                        Console.WriteLine(" " + " " + main_Menu[4]);
                    }

                    ConsoleKeyInfo key = Console.ReadKey();

                    // Navigering med 'upp' och 'ned' tangenter
                    if (key.Key == ConsoleKey.DownArrow)
                    {
                        if (x == 4)
                        {
                            choices[0] = true;
                            choices[x] = false;
                            x = 0;
                        }
                        else
                        {
                            choices[x + 1] = true;
                            choices[x] = false;
                            x++;
                        }

                    }
                    else if (key.Key == ConsoleKey.UpArrow)
                    {
                        if (x == 0)
                        {
                            choices[4] = true;
                            choices[x] = false;
                            x = 4;
                        }
                        else
                        {
                            choices[x - 1] = true;
                            choices[x] = false;
                            x--;
                        }

                    }

                    // Användaren väljer menyval med 'enter'
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        if (x == 0)
                        {
                            var bankAccounts = PostgresDataAccess.LoadBankAccounts();

                            foreach (var account in bankAccounts)
                            {
                                if (user.id == account.user_id)
                                {
                                    Console.WriteLine($"Your {account.name} balance is: {account.balance}");
                                }
                            }
                            Console.ReadKey();
                        }
                        else if (x == 1)
                        {
                            var bankAccounts = PostgresDataAccess.LoadBankAccounts();

                            int from_account = 0;
                            int to_account = 0;

                            foreach (var account in bankAccounts)
                            {
                                if (user.id == account.user_id && account.name == "Debit")
                                {
                                    Console.WriteLine("Your debit balance is " + account.balance);
                                }
                                if (user.id == account.user_id && account.name == "Savings")
                                {
                                    Console.WriteLine("Your savings balance is " + account.balance);
                                }
                            }

                            Console.Write("Please select account to transfer from: ");
                            string accountTransferFrom = Console.ReadLine();

                            if (accountTransferFrom == "Debit")
                            {
                                foreach (var account in bankAccounts)
                                {
                                    if (user.id == account.user_id && account.name == "Debit")
                                    {
                                        from_account = account.account_id;
                                    }
                                    if (user.id == account.user_id && account.name == "Savings")
                                    {
                                        to_account = account.account_id;
                                    }
                                }
                            }
                            else if (accountTransferFrom == "Savings")
                            {
                                foreach (var account in bankAccounts)
                                {
                                    if (user.id == account.user_id && account.name == "Debit")
                                    {
                                        to_account = account.account_id;
                                    }
                                    if (user.id == account.user_id && account.name == "Savings")
                                    {
                                        from_account = account.account_id;
                                    }
                                }
                            }
                            

                            Console.Write("Please select amount to transfer: ");
                            double amount = double.Parse(Console.ReadLine());
                            
                            PostgresDataAccess.Transfer(from_account, to_account, amount, userId);


                        }
                        else if (x == 2)
                        {
                            // Show transfer log
                        }
                        else if (x == 3)
                        {
                            // Return to login
                        }
                        else if (x == 4)
                        {
                            // Exit
                        }
                    }
                    Console.Clear();
                }
            }
            
                    
                    
        }
    }
}