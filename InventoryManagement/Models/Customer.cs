using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement
{
   public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public Customer(int id,string name, string email,string phone)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
        }

    }
}
