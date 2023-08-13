namespace BasketService.Models.Services.BasketServices
{
    public class BasketDto
    {
        public Guid Id { get; set; }
        public Guid? DiscountId { get; set; }
        public string UserId { get; set; }
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
        public double Total()
        {
            if (Items.Count > 0)
            {
                var total = Items.Sum(p => p.UnitPrice * p.Quantity);
                return total;
            }
            return 0;
        }

    }
}
