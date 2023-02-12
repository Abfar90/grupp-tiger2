using grupp_tiger2.Classes;
using grupp_tiger2.Data;
using System.Media;
using Spectre.Console;
using System.Security.Principal;

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
                var table = new Table();
                table.Border = TableBorder.Simple;
                
                AnsiConsole.Live(table)
                    .Start(ctx =>
                    {
                        table.AddColumn("[olive]WELCOME[/]");
                        ctx.Refresh();
                        Thread.Sleep(1000);

                        table.AddColumn("[olive]TO[/]");
                        ctx.Refresh();
                        Thread.Sleep(1000);

                        table.AddColumn("[olive]THE[/] ");
                        ctx.Refresh();
                        Thread.Sleep(1000);
                    });

                AnsiConsole.Markup("[slowblink][yellow]_____  _   __    ____  ___       ___    __    _      _    \r\n | |  | | / /`_ | |_  | |_)     | |_)  / /\\  | |\\ | | |_/ \r\n |_|  |_| \\_\\_/ |_|__ |_| \\     |_|_) /_/--\\ |_| \\| |_| \\ [/][/]");
                AnsiConsole.Markup("\n[slowblink][yellow].........................................................[/][/]");
                Console.WriteLine();
                Console.WriteLine();
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
                            //SoundPlayer musicPlayer = new SoundPlayer();
                            //musicPlayer.SoundLocation = @"C:\Users\abdir\source\repos\grupp-tiger2\Music\start-computer.wav";

                            //musicPlayer.Play();

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
                    "Open a savings account",
                    "Loans",
                    "Exit"
                };


                bool[] choices = { true, false, false, false, false, false };

                //SoundPlayer musicPlayer = new SoundPlayer();
                //musicPlayer.SoundLocation = @"C:\Users\Timpa\source\repos\grupp-tiger2\Music\lovely-boot.wav";

                //musicPlayer.Play();

                int x = 0;

                int userId = user.id;

                bool showMenu = true;
                while (showMenu)
                {
                    AnsiConsole.Markup("[slowblink][yellow]                         __,,,,_\r\n          _ __..-;''`--/'/ /.',-`-.\r\n      (`/' ` |  \\ \\ \\\\ / / / / .-'/`,_\r\n     /'`\\ \\   |  \\ | \\| // // / -.,/_,'-,\r\n    /<7' ;  \\ \\  | ; ||/ /| | \\/    |`-/,/-.,_,/')\r\n   /  _.-, `,-\\,__|  _-| / \\ \\/|_/  |    '-/.;.\\'\r\n   `-`  f/ ;      / __/ \\__ `/ |__/ |\r\n        `-'      |  -| =|\\_  \\  |-' |\r\n              __/   /_..-' `  ),'  //\r\n             ((__.-'((___..-'' \\__.'[/][/]");
                    Console.WriteLine();
                    Console.Write("\nWelcome ");
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

                                var showTable = new Table();
                                showTable.Border = TableBorder.HeavyEdge;
                                showTable.AddColumn("[cyan2]Account[/]");
                                showTable.AddColumn(new TableColumn("[deeppink2]Balance[/]").Centered());
                                Console.WriteLine();
                                foreach (var account in bankAccounts)
                                {
                                    if (user.id == account.user_id)
                                    {
                                        showTable.AddRow($"{account.name}", $"{account.balance}");
                                    }
                                }
                                AnsiConsole.Write(showTable);
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


                                //Transfer inom egna konton
                                if (key.Key == ConsoleKey.D1)
                                {
                                    Console.WriteLine();
                                    foreach (var account in bankAccounts)
                                    {
                                        if (user.id == account.user_id)
                                        {
                                            Console.WriteLine($"Your {account.name} account balance: {account.balance}");
                                        }
                                    }
                                    Console.Write("\nPlease enter account to transfer from: ");
                                    string accountTransferFrom = Console.ReadLine();

                                    Console.Write("\nAnd account to transfer to: ");
                                    string accountTransferTo = Console.ReadLine();

                                    if (accountTransferFrom == "Debit")
                                    {
                                        foreach (var account in bankAccounts)
                                        {
                                            if (user.id == account.user_id && account.name == accountTransferFrom)
                                            {
                                                from_account = account.account_id;
                                            }
                                            if (user.id == account.user_id && account.name == accountTransferTo)
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
                                            if (user.id == account.user_id && account.name == accountTransferTo)
                                            {
                                                to_account = account.account_id;
                                            }
                                        }
                                    }
                                    else if (accountTransferFrom == "Travel")
                                    {
                                        foreach (var account in bankAccounts)
                                        {
                                            if (user.id == account.user_id && account.name == accountTransferFrom)
                                            {
                                                from_account = account.account_id;
                                            }
                                            if (user.id == account.user_id && account.name == accountTransferTo)
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
                                                    if (account.currency_id == 2 || account.currency_id == 3 || account.currency_id == 4)
                                                    {
                                                        amount = bank_transactions.exchange(amount, account.currency_id);
                                                    }
                                                    PostgresDataAccess.Transfer(from_account, to_account, amount, userId);
                                                    canTransfer = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }

                                }

                                //Transfer till andra konton
                                else if (key.Key == ConsoleKey.D2)
                                {
                                    bankAccounts = PostgresDataAccess.LoadBankAccounts();
                                    bankUsers = PostgresDataAccess.LoadBankUsers();

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

                                        if (user.id == account.user_id && account.name == "Travel")
                                        {
                                            Console.WriteLine("Your savings balance is " + account.balance);
                                        }
                                    }

                                    Console.Write("\nPlease enter account to transfer from: ");
                                    string accountTransferFrom = Console.ReadLine();

                                    Console.Write("\nPlease enter which username to transfer to: ");
                                    string userTransferTo = Console.ReadLine();

                                    Console.Write("\nAnd which of their accounts to transfer to: ");
                                    string accountTransferTo = Console.ReadLine();

                                    var receivers = PostgresDataAccess.GetUser(userTransferTo);
                                    int id=0;
                                    foreach (var receiver in receivers)
                                    {
                                        id = receiver.id;
                                        
                                    }
                                    var receiverAccounts = PostgresDataAccess.GetUserAccount(id, accountTransferTo);

                                    foreach (var receiverAccount in receiverAccounts)
                                    {
                                        to_account = receiverAccount.account_id;
                                    }


                                    if (accountTransferFrom == "Debit")
                                    {
                                        foreach (var account in bankAccounts)
                                        {
                                            if (user.id == account.user_id && account.name == accountTransferFrom)
                                            {
                                                from_account = account.account_id;
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
                                        }
                                    }
                                    else if (accountTransferFrom == "Travel")
                                    {
                                        foreach (var account in bankAccounts)
                                        {
                                            if (user.id == account.user_id && account.name == accountTransferFrom)
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
                                                    if (account.currency_id == 2 || account.currency_id == 3 || account.currency_id == 4)
                                                    {
                                                        amount = bank_transactions.exchange(amount, account.currency_id);
                                                    }
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
                                var accountNames = PostgresDataAccess.LoadBankAccounts();

                                var transferTable = new Table();
                                transferTable.Border = TableBorder.Minimal;
                                transferTable.AddColumn("[deeppink2]Amount[/]");
                                transferTable.AddColumn(new TableColumn("[skyblue2]Currency[/]").Centered());
                                transferTable.AddColumn(new TableColumn("[darkorange]Time[/]").Centered());
                                transferTable.AddColumn(new TableColumn("[steelblue]From account[/]").Centered());
                                transferTable.AddColumn(new TableColumn("[orchid2]To account[/]").Centered());

                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.WriteLine("\n10 most recent transactions:\n");
                                Console.ResetColor();
                                string fromAccount = "";
                                string toAccount = "";
                                string fromUser = "";
                                string toUser = "";
                                
                                foreach (var trans in transactions)
                                {
                                    foreach (var n in accountNames)
                                    {
                                        foreach(var u in bankUsers)
                                        {
                                            if (trans.from_account_id == n.account_id && n.user_id == u.id)
                                            {
                                                fromAccount = n.name;
                                                fromUser = u.first_name;
                                            }
                                            else if (trans.to_account_id == n.account_id && n.user_id == u.id)
                                            {
                                                toAccount = n.name;
                                                toUser = u.first_name;
                                            }
                                        }
                                    }

                                    transferTable.AddRow($"{trans.amount}", "SEK", $"{trans.timestamp}", $"{fromUser}: {fromAccount}", $"{toUser}: {toAccount}");
                                    //Console.WriteLine($"{trans.amount} SEK was transferred at: {trans.timestamp}, From account: {trans.from_account_id}, To account: {trans.to_account_id}");

                                }
                                AnsiConsole.Write(transferTable);
                                Console.ReadKey();

                                break;

                            case 3:
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\n\nSmartSave – 1% interest for one year.");
                                Console.ResetColor();
                                Console.Write("\nPlease enter the amount you want to deposit to your new savings account: ");
                                double savingAmount = double.Parse(Console.ReadLine());

                                PostgresDataAccess.CreateSavingsAccount(userId, savingAmount);                                

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
                    "Update exchange rate",
                    "Customer main menu",
                    "Exit"
                };
                bool[] choices = { true, false, false, false, false };

                int x = 0;

                bool showMenu = true;

                while (showMenu)
                {

                    AnsiConsole.Markup("[slowblink][magenta]  __    ___   _      _   _          _      ____  _      _    \r\n / /\\  | | \\ | |\\/| | | | |\\ |     | |\\/| | |_  | |\\ | | | | \r\n/_/--\\ |_|_/ |_|  | |_| |_| \\|     |_|  | |_|__ |_| \\| \\_\\_/ [/][/]");
                    AnsiConsole.Markup("\n[slowblink][magenta]............................................................[/][/]");
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

                    // Användaren väljer menyval med 'enter'
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        if (x == 0)
                        {
                            var bankUsers = PostgresDataAccess.LoadBankUsers();
                            Console.WriteLine();
                            var showTable = new Table();
                            showTable.Border = TableBorder.Minimal;
                            showTable.AddColumn("[purple]Admin[/]");
                            showTable.AddColumn(new TableColumn("[aqua]Customer[/]"));
                            Console.WriteLine();
                            
                            foreach (var customer in bankUsers)
                            {
                                if (customer.role_id == 2)
                                {
                                    showTable.AddRow("", $"{customer.first_name} {customer.last_name}");
                                }
                                else
                                {
                                    showTable.AddRow($"{customer.first_name} {customer.last_name}", $"{customer.first_name} {customer.last_name}");
                                }
                            }
                            AnsiConsole.Write(showTable);
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
                            Console.WriteLine("Enter id of the currency you would like to update (GBP = 2, EUR = 3, USD = 4)");
                            int currencyID = int.Parse(Console.ReadLine());
                            string currency = "";

                            switch (currencyID)
                            {
                                case 2:
                                    currency = "GBP";
                                    break;

                                case 3:
                                    currency = "EUR";
                                    break;
                                case 4:
                                    currency = "USD";
                                    break;

                            }

                            Console.WriteLine("Enter new exchange rate in relation to SEK");
                            double newRate = double.Parse(Console.ReadLine());

                            PostgresDataAccess.ChangeCurrency(newRate, currencyID);

                            Console.Clear();

                            Console.WriteLine($"{currency} succesfully updated to {newRate} SEK/GBP!");
                            Console.ReadLine();
                        }
                        else if (x == 3)
                        {
                            mainMenu(admin);
                        }
                        else if (x == 4)
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