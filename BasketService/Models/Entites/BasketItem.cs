namespace BasketService.Models.Entites
{
    public class BasketItem
    {
        public Guid id { get; set; }

        public int  Quantity { get; set; }

        public Guid BasketId { get; set; }

        public Basket Basket { get; set; }

        public void SetQuantity(int Quantity)
        {
            this.Quantity +=  Quantity;
        }
        public Guid ProductId { get; set; }
        public Product   Product{ get; set; }
    }
}
