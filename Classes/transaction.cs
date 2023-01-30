namespace grupp_tiger2.Classes
{
    internal class transaction
    {
        public int id { get; set; }
        public double transaction_type { get; set; }
        public double from_account_id { get; set; }
        public double to_account_id { get; set; }
        public int account_id { get; set; }
        public int timestamp { get; set; }
    }
}
