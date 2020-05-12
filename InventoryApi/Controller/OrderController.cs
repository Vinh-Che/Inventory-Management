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
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public OrderController(ApplicationDBContext context)
        {
            _context = context;
        }
        [Route("AddOrder")]
        [HttpPost]
        public  ActionResult<Order> AddData(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return Ok("Success");
        }

        [Route("GetOrders")]
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetData()
        {
            return _context.Orders;
        }
        [Route("GetOrderByCustomer/{id}")]
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Order>> GetOrder(int id)
        {
            var item= _context.Orders.Where(items => items.CustomerId == id).ToList();
            if(item ==null)
            {
                return Ok("No data found");
            }
            return Ok(item);
        }
        [Route("DeleteOrder/{id}")]
        [HttpDelete("{id}")]
        public ActionResult<Order> PostCommandItems1(int id)
        {
            var item = _context.Orders.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            _context.Orders.Remove(item);
            _context.SaveChanges();
            return Ok("Deleted Successfully");
        }
    }
}
