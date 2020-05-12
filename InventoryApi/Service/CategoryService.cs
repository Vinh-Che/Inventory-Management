using InventoryApi.Interface;
using InventoryApi.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryApi.Service
{
    public class CategoryService : ICategory
    {
        private readonly ApplicationDBContext _context;

        public CategoryService(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task Create(Category category)
        {
            _context.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int categoryId)
        {
            var item = _context.Categories.Find(categoryId);
            _context.Categories.Remove(item);
            await _context.SaveChangesAsync();
            
        }

        public async Task Edit(int categoryId, string name)
        {
            var item = _context.Categories.Find(categoryId);
            item.Name = name;
             _context.Categories.Update(item);
           await _context.SaveChangesAsync();
            

        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories;
        }

        public Category GetById(int id)
        {
            return _context.Categories.Where(category => category.Id == id)
                
                .FirstOrDefault();
        }
    }
}
