namespace grupp_tiger2.Classes
{
    internal class bank_transactions
    {
        public int id { get; set; }
        public double transaction_type { get; set; }
        public int from_account_id { get; set; }
        public int to_account_id { get; set; }
        public int account_id { get; set; }
        public string timestamp { get; set; }
        public double amount { get; set; }

        public bank_transactions(int id, int from_account_id, int to_account_id, string timestamp, double amount)
        {
            this.id = id;
            this.from_account_id = from_account_id;
            this.to_account_id = to_account_id;
            this.timestamp = timestamp;
            this.amount = amount;
        }

        public bank_transactions()
        {
        }

        public static double exchange(double amount, int currencyID)
        {
            double newAmount = 0;
            switch (currencyID)
            {
                case 2:
                    newAmount = 12.73 * amount;
                    break;
                case 3:
                    newAmount = 11.38 * amount;
                    break;
                case 4:
                    newAmount = 10.57 * amount;
                    break;
            }
            return newAmount;
        }

    }
    
  
}
