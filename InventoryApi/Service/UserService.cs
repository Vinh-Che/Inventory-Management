using InventoryApi.Interface;
using InventoryApi.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryApi.Service
{
    public class UserService : IUser
    {
        private readonly ApplicationDBContext _context;

        public UserService(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task Create(UserAdmin user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
        }

        public Task Delete(int userId)
        {
            throw new NotImplementedException();
        }

        public Task Edit(int userId, string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserAdmin> GetAll()
        {
            return _context.UserAdmins;
        }

        public UserAdmin GetById(int id)
        {
            return _context.UserAdmins.Where(category => category.Id == id)              
               .FirstOrDefault();
        }
    }
}
