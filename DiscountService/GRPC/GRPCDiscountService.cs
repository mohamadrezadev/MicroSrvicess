using DiscountService.Infrastructure.Contexts;
using DiscountService.Models.Services;
using DiscountService.Protos;
using Grpc.Core;
using System.Text.RegularExpressions;

namespace DiscountService.GRPC
{
    public class GRPCDiscountService:DiscountServiceProto.DiscountServiceProtoBase
    {
        
        private readonly IDiscountService _discountService;

        public GRPCDiscountService(IDiscountService discountService)
        {
            _discountService = discountService;
        }
        public override   Task<ResultGetDiscountByCode> GetDiscountByCode(RequestGetDiscountByCode request, ServerCallContext context)
        {
            var data = _discountService.GetDiscountBycode(request.Code);
            if (data==null)
            {
                return Task.FromResult( new ResultGetDiscountByCode
                {
                    IsSuccess = false,
                    Message = " کد تخفیف یافت نشد",
                    Data = null,

                }) ;
            }
            return Task.FromResult(new ResultGetDiscountByCode
            {
                IsSuccess = true,
                Message=" ",
                Data=new DiscountInfo()
                {
                    Code = data.Code,
                    Amount = data.Amount,
                    Id = data.Id.ToString(),
                    Used = data.isUsed
                }
                
            }) ;
        }
        public override Task<ResultUseDiscount> UseDiscount(RequestUseDiscount request, ServerCallContext context)
        {
       
            var result = _discountService.UseDiscount(Guid.Parse(request.Id));

            return Task.FromResult(new ResultUseDiscount
            {
                IsSuccess = result
            }); ;
        }
        public override Task<ResultAddNewDiscount> AddNewDiscount(RequestAddNewDiscount request, ServerCallContext context)
        {

            var result = _discountService.AddnewDiscount(request.Code, request.Amount);
            return Task.FromResult(new ResultAddNewDiscount
            {
                IsSuccess = result
            });
        }
        public override Task<ResultGetDiscountByCode> GetDiscountById(RequestGetDiscountById request, ServerCallContext context)
        {
            var data=_discountService.GetDiscountById(Guid.Parse(request.Id));
            if (data == null)
            {
                return Task.FromResult(new ResultGetDiscountByCode
                {
                    IsSuccess = false,
                    Message = " کد تخفیف یافت نشد",
                    Data = null,

                });
            }
            return Task.FromResult(new ResultGetDiscountByCode
            {
                IsSuccess = true,
                Message = " ",
                Data = new DiscountInfo()
                {
                    Code = data.Code,
                    Amount = data.Amount,
                    Id = data.Id.ToString(),
                    Used = data.isUsed
                }

            });

        }
    }
}
