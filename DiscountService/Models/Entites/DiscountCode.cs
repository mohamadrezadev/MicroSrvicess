namespace DiscountService.Models.Entites
{
    public class DiscountCode
    {
        public Guid  Id { get; set; }
        public double Amount { get; set; }
        public string Code { get; set; }
        public bool isUsed { get; set; }
    }
}
