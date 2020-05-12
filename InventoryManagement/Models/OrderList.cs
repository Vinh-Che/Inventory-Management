using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement
{
  public  class OrderList
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Amount { get; set; }
        public string Created { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }

        public OrderList(int id,string productname,int quantity,double amount,string created,int custid,int pid)
        {
            Id = id;
            ProductName = productname;
            Quantity = quantity;
            Amount = amount;
            Created = created;
            CustomerId = custid;
            ProductId = pid;
        }
    }
}
