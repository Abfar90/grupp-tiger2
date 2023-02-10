using grupp_tiger2.Classes;
using grupp_tiger2.Data;
using System.Media;
using Spectre.Console;

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
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("_____  _   __    ____  ___       ___    __    _      _    \r\n | |  | | / /`_ | |_  | |_)     | |_)  / /\\  | |\\ | | |_/ \r\n |_|  |_| \\_\\_/ |_|__ |_| \\     |_|_) /_/--\\ |_| \\| |_| \\ ");
                Console.ResetColor();
                Console.WriteLine("\nWelcome!");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\n- LOGIN -");
                Console.ResetColor();
                Console.Write("\nUsername: ");
                string userName = Console.ReadLine();

                Console.Write("\nPassword: ");
                string password = Console.ReadLine();

                if (int.TryParse(password, out int passWord) && userName.Length == 4)
                {
                    foreach (var bankUser in bankUsers)
                    {
                        if (userName == bankUser.username && password == bankUser.pin_code)
                        {
                            SoundPlayer musicPlayer = new SoundPlayer();
                            musicPlayer.SoundLocation = @"C:\Users\Timpa\source\repos\grupp-tiger2\Music\start-computer.wav";

                            musicPlayer.Play();

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
                        Console.Clear();
                        loginCounter++;
                        Console.WriteLine("Sorry, the username/password is incorrect.");
                        Console.WriteLine("Please try again.\n");
                        correctLogin = false;
                    }
                }
                else if (userName.Length != 4)
                {
                    Console.Clear();
                    Console.Write("Sorry, your username must only contain ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("FOUR ");
                    Console.ResetColor();
                    Console.Write("characters.\n");
                    loginCounter++;
                }
                else if (!int.TryParse(password, out int pass_word))
                {
                    Console.Clear();
                    Console.Write("Sorry, your password must only contain ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("NUMBERS\n");
                    Console.ResetColor();
                    loginCounter++;
                }
                if (loginCounter >= 3)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("TOO MANY FAILED ATTEMPTS.");
                    Console.ResetColor();
                    Console.WriteLine("You are now prohibited from logging in for 10 seconds.\n");
                    Thread.Sleep(10000);
                    loginCounter = 0;
                }
            }



            void mainMenu(bank_user user)
            {
                Console.Clear();

                List<string> main_Menu = new List<string>()
                {
                    "Your accounts and balance",
                    "Transfer",
                    "Show transfer log",
                    "Open a saving account",
                    "Loans",
                    "Exit"
                };


                bool[] choices = { true, false, false, false, false, false };

                SoundPlayer musicPlayer = new SoundPlayer();
                musicPlayer.SoundLocation = @"C:\Users\Timpa\source\repos\grupp-tiger2\Music\lovely-boot.wav";

                musicPlayer.Play();

                int x = 0;

                int userId = user.id;

                bool showMenu = true;
                while (showMenu)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("                         __,,,,_\r\n          _ __..-;''`--/'/ /.',-`-.\r\n      (`/' ` |  \\ \\ \\\\ / / / / .-'/`,_\r\n     /'`\\ \\   |  \\ | \\| // // / -.,/_,'-,\r\n    /<7' ;  \\ \\  | ; ||/ /| | \\/    |`-/,/-.,_,/')\r\n   /  _.-, `,-\\,__|  _-| / \\ \\/|_/  |    '-/.;.\\'\r\n   `-`  f/ ;      / __/ \\__ `/ |__/ |\r\n        `-'      |  -| =|\\_  \\  |-' |\r\n              __/   /_..-' `  ),'  //\r\n             ((__.-'((___..-'' \\__.'");
                    Console.ResetColor();
                    Console.WriteLine();
                    Console.Write("Welcome ");
                    if (user.role_id == 1 || user.role_id == 3)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    Console.Write(user.first_name + " " + user.last_name);
                    Console.ResetColor();
                    Console.Write(" to the ");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("Main Menu\n");
                    Console.ResetColor();
                    Console.WriteLine("\n(You can navigate through the menu with the 'up' and 'down' arrow keys) \n");

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
                    if (choices[5] == true)
                    {
                        Console.WriteLine("[ " + main_Menu[5] + " ]");
                    }
                    else if (choices[5] == false)
                    {
                        Console.WriteLine(" " + " " + main_Menu[5]);
                    }

                    ConsoleKeyInfo key = Console.ReadKey();

                    if (key.Key == ConsoleKey.DownArrow)
                    {
                        if (x == 5)
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
                            choices[5] = true;
                            choices[x] = false;
                            x = 5;
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
                                Console.WriteLine();
                                foreach (var account in bankAccounts)
                                {
                                    if (user.id == account.user_id)
                                    {
                                        Console.WriteLine($"Your {account.name} account balance: {account.balance}");
                                    }
                                }
                                Console.ReadKey();
                                break;

                            case 1:

                                bankAccounts = PostgresDataAccess.LoadBankAccounts();

                                int from_account = 0;
                                int to_account = 0;
                                Console.WriteLine();
                                Console.WriteLine("Press '1' or '2' for transfer:\n");
                                Console.WriteLine("1. Between your accounts.");
                                Console.WriteLine("2. To other customers' accounts.");

                                key = Console.ReadKey(true);

                                if (key.Key == ConsoleKey.D1)
                                {
                                    foreach (var account in bankAccounts)
                                    {
                                        if (user.id == account.user_id)
                                        {
                                            Console.WriteLine($"Your {account.name} account balance: {account.balance}");
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

                                var transactions = PostgresDataAccess.LoadTransactions();
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.WriteLine("\nRegistered transactions:\n");
                                Console.ResetColor();
                                foreach (var trans in transactions)
                                {
                                    Console.WriteLine($"{trans.amount} SEK was transferred at: {trans.timestamp}, From account: {trans.from_account_id}, To account: {trans.to_account_id}");
                                }
                                Console.ReadKey();

                                break;

                            case 3:
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\n\nSmartSave – 1% for one year.");
                                Console.ResetColor();
                                Console.WriteLine("\nPlease enter the amount you want to deposit in your savingaccount: ");
                                double savingAmount = double.Parse(Console.ReadLine());

                                PostgresDataAccess.CreateSavingsAccount(userId, savingAmount);
                                // Return to login
                                

                                break;

                            case 4:
                                bankAccounts = PostgresDataAccess.LoadBankAccounts();

                                double totalBalance = 0;
                                bool canTakeLoan = false;

                                foreach (var account in bankAccounts)
                                {
                                    if (user.id == account.user_id)
                                    {
                                        totalBalance += account.balance;
                                    }
                                }

                                while (canTakeLoan == false)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Would you like to take a loan?\n");
                                    Console.WriteLine("1. Yes.");
                                    Console.WriteLine("2. No.");

                                    key = Console.ReadKey(true);

                                    if (key.Key == ConsoleKey.D1)
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        Console.Write("\nNOTE: ");
                                        Console.ResetColor();
                                        Console.Write("You can only loan as much money as FIVE times your total balance.");
                                        Console.Write("\nEnter loan amount: ");
                                        double loanAmount = double.Parse(Console.ReadLine());
                                        if (loanAmount > (totalBalance * 5))
                                        {
                                            Console.WriteLine("\nSorry, the amount you entered is above your allowance.");
                                        }
                                        else
                                        {
                                            PostgresDataAccess.TakeLoan(user, loanAmount);
                                            canTakeLoan = true;
                                            break;
                                        }

                                    }
                                    else if (key.Key == ConsoleKey.D2)
                                    {
                                        break;
                                    }
                                }
                                break;

                            case 5:
                                Environment.Exit(0);
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
                    "Create new customer",
                    "Customer main menu",
                    "Exit"
                };
                bool[] choices = { true, false, false, false };

                int x = 0;

                bool showMenu = true;

                while (showMenu)
                {

                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("  __    ___   _      _   _          _      ____  _      _    \r\n / /\\  | | \\ | |\\/| | | | |\\ |     | |\\/| | |_  | |\\ | | | | \r\n/_/--\\ |_|_/ |_|  | |_| |_| \\|     |_|  | |_|__ |_| \\| \\_\\_/ ");
                    Console.ResetColor();
                    Console.WriteLine();

                    Console.Write("\nCurrently logged in as: ");
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.Write(admin.first_name + " " + admin.last_name);
                    Console.ResetColor();
                    Console.WriteLine("\n");


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
                            Console.WriteLine();
                            foreach (var customer in bankUsers)
                            {

                                Console.WriteLine($"Customer: {customer.first_name}, {customer.last_name}");


                                if (customer.role_id == 2)
                                {
                                    Console.WriteLine($"Customer: {customer.first_name}, {customer.last_name}");
                                }
                                else
                                {
                                    Console.WriteLine($"Admin: {customer.first_name}, {customer.last_name}");
                                }

                            }
                            Console.ReadKey();
                            // Show all accounts
                        }
                        else if (x == 1)
                        {
                            bank_user newUser = new bank_user();
                            newUser.role_id = 2;
                            newUser.branch_id = 1;
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Creating new user.");
                            Console.ResetColor();
                            Console.Write("\nSet first name: ");
                            newUser.first_name = Console.ReadLine();

                            Console.Write("\nSet last name: ");
                            newUser.last_name = Console.ReadLine();

                            Console.Write("\nSet username: ");
                            newUser.username = Console.ReadLine();

                            Console.Write("\nSet password: ");
                            newUser.pin_code = Console.ReadLine();

                            PostgresDataAccess.CreateUser(newUser);
                        }
                        else if (x == 2)
                        {
                            mainMenu(admin);
                        }
                        else if (x == 3)
                        {
                            Environment.Exit(0);
                        }
                    }
                    Console.Clear();
                }

            }
        }
    }
}