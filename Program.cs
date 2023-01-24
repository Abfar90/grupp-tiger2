namespace grupp_tiger2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var bankUsers = PostgresDataAccess.LoadBankUsers();

            foreach (var bankUser in bankUsers)
            {
                Console.WriteLine($"{bankUser.first_name} {bankUser.last_name} {bankUser.pin_code}");
            }
        }
    }
}