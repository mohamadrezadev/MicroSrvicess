using MicroServicesMangementAdmin.Front.ViewServices.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace MicroServicesMangementAdmin.Front.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductManagementService _productManagementService;

        public ProductController(IProductManagementService productManagementService)
        {
            _productManagementService = productManagementService;
        }
        public IActionResult Index()
        {
            return View(_productManagementService.GetProductList());
        }
        [HttpPost]
        public IActionResult UpdateName(Guid ProductId, string Name,double Price)
        {
            var result=_productManagementService.Update(new UpdateProductName(ProductId, Name,Price));
            return RedirectToAction(nameof(Index));
        }
    }
}
