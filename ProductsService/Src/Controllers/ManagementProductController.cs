using Microsoft.AspNetCore.Mvc;
using ProductsService.MessageingBus.Messages;
using ProductsService.MessageingBus.SendMessage;
using ProductsService.Models.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagementProductController : ControllerBase
    {
        private readonly IProductService _productService;
      

        public ManagementProductController(IProductService productService)
        {
            _productService = productService;
            
        }


        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult Get()
        {
            var products = _productService.GetProducts();
            return Ok(products);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var product = _productService.GetProduct(id);
            return Ok(product);

        }

        // POST api/<ProductAdminController>
        [HttpPost]
        public IActionResult Post([FromBody] AddNewProductDto addNewProduct)
        {
           var result= _productService.AddNewProduct(addNewProduct);
            return Created($"api/ManagementProduct/{result}",result);
        }
        [HttpPut]
        public IActionResult Put(UpdateProductDto productDto, [FromServices] IMessagesBus messageBus
            , [FromServices] IConfiguration configuration)
        {
           var res= _productService.UpdateProduct(productDto);
            if (res)
            {
                messageBus.SendMessage(new UpdateProductNameMessage()
                { 
                    CreateTime = DateTime.Now,
                    ProductId = productDto.ProductId,
                    NewName=productDto.Name,
                    Price= productDto.Price,
                    MessageId=Guid.NewGuid()
                });
                return Ok(res);
            }
            return NotFound();
        }

    }
}
