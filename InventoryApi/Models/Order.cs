using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryApi.Models.Data
{
    public class Order
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public float Amount { get; set; }
        public DateTime Created { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
    }
}
