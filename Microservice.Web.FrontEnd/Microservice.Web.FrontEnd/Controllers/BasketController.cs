using Microservice.Web.FrontEnd.Models.Dtos;
using Microservice.Web.FrontEnd.Services.BasketService;
using Microservice.Web.FrontEnd.Services.DiscountService;
using Microservice.Web.FrontEnd.Services.ProductService;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System;

namespace Microservice.Web.FrontEnd.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketSerivce _basketSerivce;
        private readonly IProductService _productService;
        private readonly IDiscountService _discountService;
        private readonly string UserId = "1";
        public BasketController(IBasketSerivce basketSerivce,IProductService productService,IDiscountService discountService)
        {
            _basketSerivce = basketSerivce;
            _productService = productService;
            _discountService = discountService;
        }
        public IActionResult Index()
        {
            var basket = _basketSerivce.GetBasket(UserId);
            if (basket.discountId.HasValue)
            {
               var discount= _discountService.GetDiscountById(basket.discountId.Value);
                basket.discountInBasketDto = new DiscountInBasketDto()
                {
                    Amount = discount.Data.Amount,
                    code = discount.Data.Code,
                };
            }
            return View(basket);
        }
        public IActionResult Delete(Guid id)
        {
            _basketSerivce.DeleteFromBasket(id);
            return RedirectToAction("Index");
        }
        public IActionResult Addtobasket(Guid Productid)
        {
            var product=_productService.GetProductById(Productid);
            var basket = _basketSerivce.GetBasket(UserId);

            AddToBasketDto item=new AddToBasketDto()
            {
                basketId=basket.id,
                imageUrl=product.image,
                productid=product.id,
                productName=product.name,
                quantity=1,
                unitPrice=product.price
            };
            _basketSerivce.AddToBasket(item, UserId);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(Guid BasketItemId,int quantity)
        {
            _basketSerivce.UpdateQuantity(BasketItemId, quantity);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult ApplyDiscount(string DiscountCode)
        {
            if (string.IsNullOrEmpty(DiscountCode))
            {
                return Json(new ResultDto()
                {
                    IsSuccess = false,
                    Message="لطفا کد تخقیخ خود را وارد کنید "
                } );
            }
            var Result=_discountService.GetDiscountByCode(DiscountCode);
            if (Result.IsSuccess==true)
            {
                if (Result.Data.Used)
                {
                    return Json(new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "کد تخفیف قبلا استفاده شده است"
                    });
                }
                var basket=  _basketSerivce.GetBasket(UserId);
                _basketSerivce.ApplyDiscountToBasket(Guid.Parse(basket.id), Guid.Parse(Result.Data.Id.ToString()));
               
                _discountService.UseDiscount(Result.Data.Id);
                return Json(new ResultDto()
                {
                    IsSuccess = true,
                    Message = "کد تخفیف با موفقیت اعمال شد"
                });
            }
            else
            {
                return Json(new ResultDto()
                {
                    IsSuccess = false,
                    Message = Result.Message
                }) ;
            }
        }
    
        public IActionResult Checkout()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Checkout(CheckoutDto checkout)
        {
            checkout.UserId = UserId;
            checkout.BasketId = Guid.Parse(_basketSerivce.GetBasket(UserId).id);
            var result = _basketSerivce.Checkout(checkout);
            if (result.IsSuccess)
            {
                return RedirectToAction("OrderCreate");
            }
            ViewBag.message = "";
            return View(checkout);
        }
        public IActionResult OrderCreate()
        {
            return View();
        }
    }
}
