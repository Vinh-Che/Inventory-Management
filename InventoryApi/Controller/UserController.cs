using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryApi.Interface;
using InventoryApi.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApi.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IUser _userService;
        public UserController(IUser userService, ApplicationDBContext context)
        {
            _userService = userService;
            _context = context;
        }

        [Route("GetAllUser")]
        [HttpGet]
        public ActionResult<IEnumerable<UserAdmin>> Index()
        {
            return _context.UserAdmins;
        }
        [Route("AddAdmin")]
        [HttpPost]
        public ActionResult<UserAdmin> AddData(UserAdmin userAdmin)
        {
            if(_context.UserAdmins.Any(users=>users.UserName==userAdmin.UserName))
            {
                return Ok("Username already exists!!");
            }
            _context.UserAdmins.Add(userAdmin);
            _context.SaveChanges();
            return Ok("Success");
        }
        [Route("UpdateUser/{id}")]
        [HttpPut("{id}")]
        public ActionResult<UserAdmin> UpdateUser(UserAdmin userAdmin, int id)
        {
            var item = _context.UserAdmins.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            if (_context.UserAdmins.Any(users => users.UserName == userAdmin.UserName))
            {
                return Ok("Username already exists!!");
            }
            item.Name = userAdmin.Name;
            item.UserName = userAdmin.UserName;
            item.Password = userAdmin.Password;
            item.Email = userAdmin.Email;
            item.Phone = userAdmin.Phone;
            _context.UserAdmins.Update(item);
            _context.SaveChanges();
            return Ok("Successfully updated!!");
        }
        [Route("DeleteUser/{id}")]
        [HttpDelete("{id}")]
        public ActionResult<UserAdmin> DeleteUser(int id)
        {
            var item = _context.UserAdmins.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            _context.UserAdmins.Remove(item);
            _context.SaveChanges();
            return Ok("Deleted Successfully");
        }

    
        [Route("CheckUser/{username}/{password}")]
        [HttpGet("{username}/{password}")]
        public ActionResult UpdateUser1( string username,string password)
        {
            var item = _context.UserAdmins.
                Where(items=>items.UserName==username).FirstOrDefault();


            if (item == null)
            {
                return Ok("User does not exist");
            }
            
            else
            {
                if (item.UserName == username && item.Password == password)
                return Ok("success");
                else
                    return Ok("Invalid username or password!!");
            }
        }
    }
}