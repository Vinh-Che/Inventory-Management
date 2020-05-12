using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryApi.Interface;
using InventoryApi.Models;
using InventoryApi.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApi.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _categoryService;
        private readonly ApplicationDBContext _context;

        public CategoryController(ICategory categoryService,ApplicationDBContext context)
        {
            _categoryService=categoryService;
            _context = context;
        }

        [Route("GetAllCategory")]
        [HttpGet]
        public ActionResult Index()
        {
            var items = _categoryService.GetAll();
            return Ok( items);            
        }
        
        [Route("GetAll")]
        [HttpGet]
        public ActionResult Index1()
        {
            var model = List1();
            return Ok(model);
          
        }
        private List List1()
        {
            var cat = _categoryService.GetAll();
            var catss = cat.Select(cats => new Category
            {
                Id=cats.Id,
                Name=cats.Name
            });
            return new List
            {
                LatestPosts = catss
            };
        }
     

        [Route("Addcategory")]
        [HttpPost]
        public async Task<ActionResult<Category>> PostItem(Category category)
        {          
            await  _categoryService.Create(category);
            return Ok("Successfully created!!");
        }
      

        [Route("GetCategory/{id}")]
        [HttpGet("{id}")]
        public ActionResult<Category> Index2(int id)
        {        
            var item = _categoryService.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        

        [Route("UpdateCategory/{id}")]
        [HttpPut("{id}")]
        public async Task< ActionResult<Category>> UpdateCategory(Category category, int id)
        {
             var item = _context.Categories.Find(id);
             if (item == null)
             {
                 return NotFound();
             }
            await _categoryService.Edit(id, category.Name);
            return Ok("Successfully updated!!");
        }
        [Route("DeleteCategory/{id}")]
        [HttpDelete("{id}")]
        public async Task< ActionResult<Category>> DeleteCategory(int id)
        {
            var item = _context.Categories.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            await _categoryService.Delete(id);
            return Ok("Deleted Successfully");
        }
    }
}
