using BasketService.Models.Dtos;
using BasketService.Models.Services.DiscountServices;

namespace BasketService.Models.Services.BasketServices
{
    public interface IBasketService
    {
        BasketDto GetOrCreateBasketForUser(string userId);
        BasketDto GetBasket(string userid);
        void AddItemToBasket(AddItemToBasketDto Item);
        void RemoveItemFromBasket(Guid ItemId);
        void SetQuantities(Guid itemid, int quantity);
        void TransferBasket(string anonymousId, string userId);
        void ApplyDiscountTobasket(Guid BasketId, Guid DiscountId);
        ResultDto CheckoutBasket(CheckoutBasketDto checkoutBasket, IDiscountService discountService);
    }
}
