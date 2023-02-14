using grupp_tiger2.Classes;
using grupp_tiger2.Data;
using grupp_tiger2.UI;
using System.Media;
using Spectre.Console;
using System.Security.Principal;

namespace grupp_tiger2.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BankApp newApp = new BankApp();
            newApp.Run();
        }
    }
}