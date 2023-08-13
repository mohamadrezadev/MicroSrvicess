using AutoMapper;
using BasketService.Models.Entites;
using BasketService.Models.Services.BasketServices;
using BasketService.Models.Services.BasketServices.MessageDto;

namespace BasketService.Infrastructure.MappingProfile
{
    public class BasketMappingProfile:Profile
    {
        public BasketMappingProfile()
        {
            CreateMap<BasketItem,AddItemToBasketDto>().ReverseMap();
            CreateMap<Productdto,Product>().ReverseMap();
            CreateMap<AddItemToBasketDto,Productdto>().ReverseMap();
            CreateMap<CheckoutBasketDto,BasketCheckoutMessage>().ReverseMap();
        }
    }
}
