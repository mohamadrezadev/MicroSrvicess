using Microservice.Web.FrontEnd.Services.ProductService;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Web.FrontEnd.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            var product=_productService.GetProducts();
            return View(product);
        }
        public IActionResult Details(Guid Id)
        {
            var product=_productService.GetProductById(Id);
            return View(product);
        }
    }
}
