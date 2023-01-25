using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grupp_tiger2.Classes
{
    internal class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool is_admin { get; set; }
        public bool is_client { get; set; }

        public Role(int id, string name, bool is_admin, bool is_client)
        {
            Id = id;
            Name = name;
            this.is_admin = is_admin;
            this.is_client = is_client;
        }
    }
}
