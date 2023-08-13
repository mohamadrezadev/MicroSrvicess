using Microsoft.AspNetCore.Mvc;
using ProductsService.Models.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var data=_categoryService.GetCategories();
            return Ok(data);    
        }
        [HttpPost]
        public IActionResult Post([FromBody] CategoryDto category )
        {
            var result=_categoryService.AddNewCategory(category);
            return Created($"api/Category/{result}",result);
        }
    }
}
