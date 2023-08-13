using BasketService.Models.Dtos;
using DiscountService.Protos;
using Grpc.Net.Client;

namespace BasketService.Models.Services.DiscountServices
{
    public class DiscountService : IDiscountService
    {
        private readonly GrpcChannel Channel;
        private readonly IConfiguration Configuration;
        public DiscountService(IConfiguration configuration)
        {

            Configuration = configuration;
            Channel =  GrpcChannel.ForAddress(Configuration["MicroservicAddress:DiscountService:Uri"]);

        }
        public DiscountDto GetDicountById(Guid DiscountId)
        {
            var grpc_DiscountService = new DiscountServiceProto.DiscountServiceProtoClient(Channel);
            var result=  grpc_DiscountService.GetDiscountById(new RequestGetDiscountById
            {
                Id = DiscountId.ToString()
            }) ;
            if (result.IsSuccess)
            {
                return new DiscountDto()
                {
                    Code = result.Data.Code,
                    Amount = result.Data.Amount,
                    Id = Guid.Parse(result.Data.Id),
                    Used = result.Data.Used,
                };
            }
            return null;
        }

        public ResultDto<DiscountDto> GetDiscountBycode(string Code)
        {
            var grpc_DiscountService = new DiscountServiceProto.DiscountServiceProtoClient(Channel);
            var result = grpc_DiscountService.GetDiscountByCode(new RequestGetDiscountByCode
            {
                Code = Code
            });
            if (result.IsSuccess)
            {
                return new ResultDto<DiscountDto>()
                {
                    IsSuccess = true,
                    Message = result.Message,
                    Data = new DiscountDto()
                    {
                        Id = Guid.Parse(result.Data.Id),
                        Amount = result.Data.Amount,
                        Code = result.Data.Code,
                        Used = result.Data.Used,

                    }
                };
            }
            return new ResultDto<DiscountDto>()
            {
                IsSuccess = false,
                Message = result.Message,
            };
        }
    }
}
