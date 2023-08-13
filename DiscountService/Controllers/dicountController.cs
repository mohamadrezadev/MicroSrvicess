using DiscountService.Models.Entites;
using DiscountService.Models.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiscountService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class dicountController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public dicountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }
        [HttpPost]
        public IActionResult Create(string code, double Amount)
        {
         
           var result= _discountService.AddnewDiscount(code, Amount);
            return Ok(result);
        }
    }
}
