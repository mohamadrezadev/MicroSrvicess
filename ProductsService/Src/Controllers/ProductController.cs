using Microsoft.AspNetCore.Mvc;
using ProductsService.Models.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult Get()
        {
            var products= _productService.GetProducts();
            return Ok(products);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var product=_productService.GetProduct(id);
            return Ok(product);
           
        }
        [HttpGet("Verify/{Id}")]
        public IActionResult Verify(Guid Id)
        {
            var result = _productService.GetProduct(Id);
            return Ok(new ProductVerify(result.id,result.Name));
        }
       
       
    }
    public record ProductVerify(Guid Id,string Name);
}
