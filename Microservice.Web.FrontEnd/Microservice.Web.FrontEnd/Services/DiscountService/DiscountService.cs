using DiscountService.Protos;
using Grpc.Net.Client;
using Microservice.Web.FrontEnd.Models.Dtos;


namespace Microservice.Web.FrontEnd.Services.DiscountService
{
    public class DiscountService : IDiscountService
    {
        private readonly GrpcChannel Channel;
        private readonly IConfiguration configuration;
        public DiscountService(IConfiguration configuration)
        {
            this.configuration = configuration;
            Channel = GrpcChannel.ForAddress(configuration["MicroServiceAddress:DiscountService:Uri"]);
        }
        public ResultDto<DiscountDto> GetDiscountByCode(string code)
        {
            try
            {
                var grpc_DicountService = new DiscountServiceProto.DiscountServiceProtoClient(Channel);
                var result = grpc_DicountService.GetDiscountByCode(new RequestGetDiscountByCode()
                {
                    Code = code
                });
                if (result.IsSuccess)
                {
                    return new ResultDto<DiscountDto>()
                    {
                        IsSuccess = true,
                        Message = result.Message,
                        Data = new DiscountDto()
                        {
                            Amount = result.Data.Amount,
                            Code = result.Data.Code,
                            Id = Guid.Parse(result.Data.Id),
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
            catch (Exception)
            {

                throw;
            }

        }

        public ResultDto<DiscountDto> GetDiscountById(Guid Id)
        {
            var grpc_DicountService = new DiscountServiceProto.DiscountServiceProtoClient(Channel);
            var result = grpc_DicountService.GetDiscountById(new RequestGetDiscountById()
            {
               Id = Id.ToString(),
               
            });
            if (result.IsSuccess)
            {
                return new ResultDto<DiscountDto>()
                {
                    IsSuccess = true,
                    Message = result.Message,
                    Data = new DiscountDto()
                    {
                        Amount = result.Data.Amount,
                        Code = result.Data.Code,
                        Id = Guid.Parse(result.Data.Id),
                        Used = result.Data.Used,

                    }

                };
            }
            return new ResultDto<DiscountDto>()
            {
                IsSuccess = true,
                Message = result.Message,
            };
        }

        public ResultDto UseDiscount(Guid DiscountId)
        {
            var grpc_DicountService = new DiscountServiceProto.DiscountServiceProtoClient(Channel);
            var result = grpc_DicountService.UseDiscount(new RequestUseDiscount()
            {
                Id = DiscountId.ToString(),

            });
            return new ResultDto()
            {
                IsSuccess = result.IsSuccess,
            };
        }
    }
}
