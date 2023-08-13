using AutoMapper;
using DiscountService.Models.Entites;
using DiscountService.Models.Services;
using static DiscountService.Models.Services.DiscountService;

namespace DiscountService.Infrastructure.MappingProfile
{
    public class DiscountMappingProfile:Profile
    {
        public DiscountMappingProfile()
        {
            CreateMap<DiscountCode, DiscountDto>().ReverseMap();
        }
    }
}
