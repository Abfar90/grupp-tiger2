using grupp_tiger2.Data;

namespace grupp_tiger2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Welcome to the Tiger bank");

            Console.WriteLine("Please insert your username.");
            string userName = Console.ReadLine();

            Console.WriteLine("Please insert your password.");
            int password = int.Parse(Console.ReadLine());


            var bankUsers = PostgresDataAccess.LoadBankUsers();

            foreach (var bankUser in bankUsers)
            {
                Console.WriteLine($"{bankUser.first_name} {bankUser.last_name} {bankUser.pin_code} {bankUser.username}");
            }

            mainMenu();

            void mainMenu()
            {
                Console.Clear();

                // Menyval
                List<string> main_Menu = new List<string>()
                {
                    "Your accounts and account balance",
                    "Transactions",
                    "Withdrawal",
                    "Return to login",
                    "Exit"
                };

                // Avgör vilket menyval man är på
                bool[] choices = { true, false, false, false, false };

                // Räknare
                int x = 0;

                // Loop körs för att behålla menyn på skärmen
                bool showMenu = true;
                while (showMenu)
                {
                    Console.WriteLine("- MAIN MENU -");
                    Console.ResetColor();
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

                        }
                        else if (x == 1)
                        {

                        }
                        else if (x == 2)
                        {

                        }
                        else if (x == 3)
                        {

                        }
                        else if (x == 4)
                        {

                        }
                    }
                    Console.Clear();
                }
            }


            mainMenu();

            void mainMenu()
            {


                // Menyval
                List<string> main_Menu = new List<string>()
                {
                    "Your accounts and account balance",
                    "Transactions",
                    "Withdrawal",
                    "Return to login",
                    "Exit"
                };

                // Avgör vilket menyval man är på
                bool[] choices = { true, false, false, false, false };

                // Räknare
                int x = 0;

                // Loop körs för att behålla menyn på skärmen
                bool showMenu = true;
                while (showMenu)
                {
                    Console.WriteLine("- MAIN MENU -");
                    Console.ResetColor();
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

                        }
                        else if (x == 1)
                        {

                        }
                        else if (x == 2)
                        {

                        }
                        else if (x == 3)
                        {

                        }
                        else if (x == 4)
                        {

                        }
                    }
                    Console.Clear();
                }
            }
        }
    }
}