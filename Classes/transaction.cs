namespace grupp_tiger2.Classes
{
    internal class transaction
    {
        public int id { get; set; }
        public double transaction_type { get; set; }
        public int from_account_id { get; set; }
        public int to_account_id { get; set; }
        public int account_id { get; set; }
        public string timestamp { get; set; }
        public double amount { get; set; }

        public transaction(int id, int from_account_id, int to_account_id, string timestamp, double amount)
        {
            this.id = id;
            this.from_account_id = from_account_id;
            this.to_account_id = to_account_id;
            this.timestamp = timestamp;
            this.amount = amount;
        }
    }
}
