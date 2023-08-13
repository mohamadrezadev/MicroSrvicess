using Microservice.Web.FrontEnd.Models.Dtos;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace Microservice.Web.FrontEnd.Services.DiscountService
{
    public interface IDiscountService
    {
        ResultDto<DiscountDto> GetDiscountByCode(string code);
        ResultDto<DiscountDto> GetDiscountById(Guid Id);
        ResultDto UseDiscount(Guid DiscountId);
    }
}
