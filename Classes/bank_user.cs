namespace grupp_tiger2.Classes
{
    internal class bank_user
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string pin_code { get; set; }
        public string username { get; set; }
        public int role_id { get; set; }
        public int branch_id { get; set; }

        public bank_user(string first_name, string last_name, string pin_code, string username, int roleID, int branchID)
        {
            this.first_name = first_name;
            this.last_name = last_name;
            this.pin_code = pin_code;
            this.username = username;
            this.role_id = roleID;
            this.branch_id = branchID;
        }

        public bank_user()
        {
        }
    }
}