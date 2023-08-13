using Microservice.Web.FrontEnd.Models.Dtos;
using System.Reflection.Metadata;

namespace Microservice.Web.FrontEnd.Services.BasketService
{
    public interface IBasketSerivce
    {
        BasketDto GetBasket(string UserId);
        ResultDto AddToBasket(AddToBasketDto dto, string UserId);
        ResultDto DeleteFromBasket(Guid Iditem);
        ResultDto UpdateQuantity(Guid basketitemid, int quantity);
        ResultDto ApplyDiscountToBasket(Guid basketitemid, Guid DiscountId);
        ResultDto Checkout(CheckoutDto checkout);

    }


}
