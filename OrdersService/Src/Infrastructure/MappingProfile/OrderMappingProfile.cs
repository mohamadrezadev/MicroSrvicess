using AutoMapper;
using OrdersService.Models.Entites;
using OrdersService.Models.Services.OrderServices;

namespace OrdersService.Infrastructure.MappingProfile
{
    public class OrderMappingProfile:Profile
    {
        public OrderMappingProfile()
        {
        
            CreateMap<Order,OrderDto>().ReverseMap();
            CreateMap<OrderLine,OrderLineDto>().ReverseMap();
            
        }
    }
}
