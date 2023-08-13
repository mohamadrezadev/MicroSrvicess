using BasketService.Models.Dtos;

namespace BasketService.Models.Services.DiscountServices
{
    public interface IDiscountService
    {
        DiscountDto GetDicountById(Guid DiscountId);
        ResultDto<DiscountDto> GetDiscountBycode(string Code);
    }
}
