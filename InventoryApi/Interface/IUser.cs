using InventoryApi.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryApi.Interface
{
   public interface IUser
    {
        UserAdmin GetById(int id);
        IEnumerable<UserAdmin> GetAll();
        Task Create(UserAdmin user);
        Task Edit(int userId, string name);

        Task Delete(int userId);

    }
}
