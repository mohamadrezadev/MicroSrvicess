namespace Microservice.Web.FrontEnd.Services.BasketService
{
    public class BasketDto
    {
        public string id { get; set; }
        public Guid? discountId { get; set; }
        public string userId { get; set; }
        public DiscountInBasketDto discountInBasketDto { get; set; }
        public List<BaketItem> items { get; set; } = new List<BaketItem>();
        public double TotalPrice()
        {
            double result = items.Sum(p => p.unitPrice * p.quantity);
            if (discountId.HasValue)
            {
                result = result - discountInBasketDto.Amount;
            }
            return result;
        }
    }
    public class DiscountInBasketDto
    {
        public double Amount { get; set; }
        public string code { get; set; }
    }


}
