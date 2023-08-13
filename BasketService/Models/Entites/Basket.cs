namespace BasketService.Models.Entites
{
    public class Basket
    {
        public Basket(string UserId)
        {
            this.UserId = UserId;
        }
        public Basket()
        {
            
        }
        public Guid id { get; set; }
        public string  UserId { get; set; }
        public Guid? DiscountId { get; set; }
        public List<BasketItem> Items { get; set; }=new List<BasketItem>();
       
      
    }
}
