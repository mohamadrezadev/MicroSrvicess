using AutoMapper;
using DiscountService.Infrastructure.Contexts;
using DiscountService.Models.Entites;
using Microsoft.EntityFrameworkCore;
using static DiscountService.Models.Services.DiscountService;

namespace DiscountService.Models.Services
{
    public interface IDiscountService
    {
        DiscountDto GetDiscountBycode(string code);
        DiscountDto GetDiscountById(Guid id);
        bool UseDiscount(Guid id);
        bool AddnewDiscount(string code,double Amount);
    }
    public class DiscountService : IDiscountService
    {
        private readonly DiscountDatabaseContext _context;
        private readonly IMapper _mapper;

        public DiscountService(DiscountDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool AddnewDiscount(string code, double Amount)
        {
            DiscountCode discountCode = new DiscountCode()
            {
                Amount = Amount,
                Code = code,
                isUsed = false
            };
            _context.discountCodes.Add(discountCode);
            _context.SaveChanges();
            return true;
        }

        public  DiscountDto GetDiscountBycode(string code)
        {
            var discountCode =_context.discountCodes.SingleOrDefault(p => p.Code.Equals(code));

            if (discountCode == null)
                return null;
            var result = _mapper.Map<DiscountDto>(discountCode);
            return result;
            

        }

        public DiscountDto GetDiscountById(Guid id)
        {
            var discountcode = _context.discountCodes.SingleOrDefault(p => p.Id == id);
            if (discountcode == null) return null;
            var result = _mapper.Map<DiscountDto>(discountcode);
            return result;
        }

        public bool UseDiscount(Guid Id)
        {
            var discountCode = _context.discountCodes.Find(Id);
            if (discountCode == null)
                throw new Exception("Discouint Not Found....");
            discountCode.isUsed = true;
            _context.SaveChanges();
            return true;
        }
        public class DiscountDto
        {
            public Guid Id { get; set; }
            public double Amount { get; set; }
            public string Code { get; set; }
            public bool isUsed { get; set; }
        }
    }
}

