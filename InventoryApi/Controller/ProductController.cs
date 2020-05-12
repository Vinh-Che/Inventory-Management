using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryApi.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApi.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public ProductController(ApplicationDBContext context)
        {
            _context = context;
        }
        [Route("AddProduct")]
        [HttpPost]
        public ActionResult<Product> AddData(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok("Successfully created!!");
        }
        [Route("GetAllProducts")]
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Index()
        {
            return _context.Products;
        }

        [Route("ProductsByCategory/{id}")]
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Product>> Index2(int id)
        {
      
            return _context.Products.Where(items => items.CategoryId == id).ToList();
        }

        [Route("UpdateProduct/{id}")]
        [HttpPut("{id}")]
        public ActionResult<Product> PostCommandItems(Product product, int id)
        {
            var item = _context.Products.Where(items => items.Id == id).FirstOrDefault();
            if (item == null)
            {
                return NotFound();
            }
            item.Name = product.Name;
            item.Description = product.Description;
            item.Price = product.Price;
            item.Quantity = product.Quantity;
            item.CategoryId = product.CategoryId;
            _context.Products.Update(item);
            _context.SaveChanges();
            return Ok("Successfully updated!!");
        }
        [Route("UpdateQuantity/{id}/{quantity}")]
        [HttpPost("{id}/{quantity}")]
        public ActionResult<Product> PostQuantity( int id,int quantity)
        {
            var item = _context.Products.Where(items => items.Id == id).FirstOrDefault();
            if (item == null)
            {
                return NotFound();
            }
           
            item.Quantity += quantity;
        
            _context.Products.Update(item);
            _context.SaveChanges();
            return Ok("Successfully updated!!");
        }

        [Route("DeleteProduct/{id}")]
        [HttpDelete("{id}")]
        public ActionResult<Product> PostCommandItems1(int id)
        {
            var item = _context.Products.Find(id);
            if (item == null)
            {
                return NotFound();
            }
           _context.Products.Remove(item);
            _context.SaveChanges();
            return Ok("Deleted Successfully");
        }
    }
}