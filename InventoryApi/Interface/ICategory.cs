using InventoryApi.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryApi.Interface
{
    public interface ICategory
    {
        Category GetById(int id);

        IEnumerable<Category> GetAll();

        Task Create(Category category);
        Task Edit(int categoryId, string name);

        Task Delete(int categoryId);
    }
}
