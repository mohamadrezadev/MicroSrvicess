using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrdersService.Models.Services.ProductServices;

namespace OrdersService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "OrderService.AccessUser")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IVerifyProductService _verifyProductService;

        public ProductController(IProductService productService,IVerifyProductService verifyProductService)
        {
            _productService = productService;
            _verifyProductService = verifyProductService;
        }
        [HttpGet]
        public IActionResult Get(Guid Id) 
        {
            var product = _productService.GetProduct(new ProductDto() { ProductId=Id});
            return Ok(_verifyProductService.VerifyProduct(new ProductDto()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
            }));
        }
    }
}
