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
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public CustomerController(ApplicationDBContext context)
        {
            _context = context;
        }
        [Route("AddCustomer")]
        [HttpPost]
        public ActionResult<Customer> AddData(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return Ok("Successfully created!");
        }

        [Route("GetAllCustomers")]
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Index()
        {
            return _context.Customers;
        }
        [Route("GetCustomerById/{id}")]
        [HttpGet("{id}")]
        public ActionResult<Customer> GetCystmer(int id)
        {

            var item = _context.Customers.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        [Route("UpdateCustomer/{id}")]
        [HttpPut("{id}")]
        public ActionResult<Customer> UpdateCustomer(Customer customer, int id)
        {
            var item = _context.Customers.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            item.Name = customer.Name;
            item.Email = customer.Email;
            item.Phone = customer.Phone;          
            _context.Customers.Update(item);
            _context.SaveChanges();
            return Ok("Successfully updated!!");
        }
        [Route("DeleteCustomer/{id}")]
        [HttpDelete("{id}")]
        public ActionResult<Customer> PostCommandItems1(int id)
        {
            var item = _context.Customers.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            _context.Customers.Remove(item);
            _context.SaveChanges();
            return Ok("Deleted Successfully");
        }
    }
}