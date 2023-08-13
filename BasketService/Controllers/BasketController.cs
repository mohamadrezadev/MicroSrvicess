using BasketService.Models.Services.BasketServices;
using BasketService.Models.Services.DiscountServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BasketService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }
        // GET: api/<BasketController>
        [HttpGet]
        public IActionResult Get(string Userid)
        {
            var basket=_basketService.GetOrCreateBasketForUser(Userid);
            return Ok(basket);
        }

   
        [HttpPost]
        public IActionResult AddItemToBasket(AddItemToBasketDto Request,string Userid )
        {
            var basket = _basketService.GetOrCreateBasketForUser(Userid);
            Request.basketId = basket.Id;
            _basketService.AddItemToBasket(Request);
            var basketData = _basketService.GetBasket(Userid);
            return Ok(basketData);

        }
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _basketService.RemoveItemFromBasket(id);
            return Ok();
        }
        [HttpPut]
        public IActionResult SetQuantity(Guid basketitemid,int quantity)
        {
            _basketService.SetQuantities(basketitemid,quantity);
            return Ok();
        }
        [HttpPut("{BasketId}/{discountId}")]
        public IActionResult ApplyDiscountToBasket(Guid BasketId,Guid discountId)
        {
            _basketService.ApplyDiscountTobasket(BasketId,discountId);
            return Accepted();
        }
        [HttpPost("CheckoutBaskt")]
        public IActionResult CheckoutBaskt(CheckoutBasketDto checkoutBasket, [FromServices] IDiscountService discountService) 
        {
           var result= _basketService.CheckoutBasket(checkoutBasket, discountService);
            if(result.IsSuccess)
                return Ok(result);
            else
                return StatusCode(404,result);

        }
    }
}
