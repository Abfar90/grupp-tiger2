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

                Console.WriteLine("Please enter your username.");
                string userName = Console.ReadLine();

                Console.WriteLine("Please enter your password.");
                string password = Console.ReadLine();

                if (int.TryParse(password, out int passWord) && userName.Length == 4)
                {
                    foreach (var bankUser in bankUsers)
                    {
                        if (userName == bankUser.username && password == bankUser.pin_code)
                        {
                            userFound = true;

                            if (bankUser.role_id == 1 || bankUser.role_id == 3)
                            {
                                adminMenu(bankUser);
                                break;
                            }

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
                    Console.WriteLine("You are now prohibited from logging in for 10 seconds.");
                    Thread.Sleep(10000);
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

                    else if (key.Key == ConsoleKey.Enter)
                    {
                        switch (x)
                        {
                            case 0:

                                var bankAccounts = PostgresDataAccess.LoadBankAccounts();

                                foreach (var account in bankAccounts)
                                {
                                    if (user.id == account.user_id)
                                    {
                                        Console.WriteLine($"Your {account.name} balance is: {account.balance}");
                                    }
                                }
                                Console.ReadKey();
                                break;

                            case 1:

                                bankAccounts = PostgresDataAccess.LoadBankAccounts();

                                int from_account = 0;
                                int to_account = 0;

                                Console.WriteLine("Press '1' or '2' for transfer: ");
                                Console.WriteLine("1.Between your accounts.");
                                Console.WriteLine("2.To other customers' accounts.");

                                key = Console.ReadKey();

                                if (key.Key == ConsoleKey.D1)
                                {
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
                                    Console.Write("\nPlease select account to transfer from: ");
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

                                    bool canTransfer = false;

                                    while (!canTransfer)
                                    {
                                        Console.Write("Please select amount to transfer: ");
                                        double amount = double.Parse(Console.ReadLine());

                                        foreach (var account in bankAccounts)
                                        {
                                            if (from_account == account.account_id)
                                            {
                                                if (amount > account.balance)
                                                {
                                                    Console.WriteLine("Sorry, you can't transfer more money than you have available.");
                                                    Console.WriteLine("Please try again.");
                                                }
                                                else
                                                {
                                                    PostgresDataAccess.Transfer(from_account, to_account, amount, userId);
                                                    canTransfer = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }

                                }
                                else if (key.Key == ConsoleKey.D2)
                                {
                                    bankAccounts = PostgresDataAccess.LoadBankAccounts();

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

                                    Console.Write("\nPlease enter account to transfer from: ");
                                    string accountTransferFrom = Console.ReadLine();

                                    foreach (var account in bankAccounts)
                                    {
                                        Console.WriteLine($"{account.account_id} --> {account.name}: {account.balance}");
                                    }

                                    Console.Write("\nPlease enter which customer id to transfer to: ");
                                    int userTransferTo = int.Parse(Console.ReadLine());

                                    Console.Write("\nAnd which of their accounts to transfer to: ");
                                    string accountTransferTo = Console.ReadLine();

                                    if (accountTransferFrom == "Debit")
                                    {
                                        foreach (var account in bankAccounts)
                                        {
                                            if (user.id == account.user_id && account.name == accountTransferFrom)
                                            {
                                                from_account = account.account_id;
                                            }
                                            if (userTransferTo == account.user_id && account.name == accountTransferTo)
                                            {
                                                to_account = account.account_id;
                                            }
                                        }
                                    }
                                    else if (accountTransferFrom == "Savings")
                                    {
                                        foreach (var account in bankAccounts)
                                        {
                                            if (user.id == account.user_id && account.name == accountTransferFrom)
                                            {
                                                from_account = account.account_id;
                                            }
                                            if (userTransferTo == account.user_id && account.name == accountTransferTo)
                                            {
                                                to_account = account.account_id;
                                            }
                                        }
                                    }

                                    bool canTransfer = false;
                                    
                                    while (!canTransfer)
                                    {
                                        Console.Write("Please select amount to transfer: ");
                                        double amount = double.Parse(Console.ReadLine());

                                        foreach (var account in bankAccounts)
                                        {
                                            if (from_account == account.account_id)
                                            {
                                                if (amount > account.balance)
                                                {
                                                    Console.WriteLine("Sorry, you can't transfer more money than you have available.");
                                                    Console.WriteLine("Please try again.");
                                                }
                                                else
                                                {
                                                    PostgresDataAccess.Transfer(from_account, to_account, amount, userId);
                                                    canTransfer = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }

                                break;

                            case 2:

                                // Show transfer log

                                break; 

                            case 3:

                                // Return to login

                                break;

                            case 4:

                                // Exit

                                break;
                        }
                    }
                    Console.Clear();
                }

            }

            void adminMenu(bank_user admin)
            {
                Console.Clear();

                List<string> main_Menu = new List<string>()
                {
                    "Show all customers",
                    "Create new customer account",
                    "Return to main menu",
                    "Exit"
                };
                bool[] choices = { true, false, false, false};

                int x = 0;

                bool showMenu = true;

                while (showMenu)
                {
                    Console.WriteLine("Welcome to the Admin Menu. From here you will be able to view all" + 
                        "customer accounts and create a new customer");
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

                    ConsoleKeyInfo key = Console.ReadKey();

                    // Navigering med 'upp' och 'ned' tangenter
                    if (key.Key == ConsoleKey.DownArrow)
                    {
                        if (x == 3)
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
                            choices[3] = true;
                            choices[x] = false;
                            x = 3;
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
                            var bankUsers = PostgresDataAccess.LoadBankUsers();

                            foreach (var customer in bankUsers)
                            {
                                    Console.WriteLine($"Customer: {customer.first_name}, {customer.last_name}");
 
                            }
                            Console.ReadKey();
                            // Show all accounts
                        }
                        else if (x == 1)
                        {
                            bank_user newUser = new bank_user();
                            newUser.role_id = 2;
                            newUser.branch_id = 1;

                            Console.WriteLine("Enter first name");
                            newUser.first_name = Console.ReadLine();

                            Console.WriteLine("Enter last name");
                            newUser.last_name = Console.ReadLine();

                            Console.WriteLine("Enter username");
                            newUser.username = Console.ReadLine();

                            Console.WriteLine("Enter pincode");
                            newUser.pin_code = Console.ReadLine();

                            PostgresDataAccess.CreateUser(newUser);
                        }
                        else if (x == 2)
                        {
                            mainMenu(admin);
                            // return to main menu
                        }
                        else if (x == 3)
                        {
                            Environment.Exit(0);
                            // exit
                        }
                    }
                    Console.Clear();
                }

            }
        }
    }
}