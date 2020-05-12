using InventoryApi.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryApi.Models
{
    public class List
    {
        public IEnumerable<Category> LatestPosts { get; set; }
    }
}
