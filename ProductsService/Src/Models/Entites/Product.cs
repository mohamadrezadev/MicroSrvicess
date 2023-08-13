namespace ProductsService.Models.Entites
{
    public class Product
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public double Price  { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public void UpdatePrice( double newprice)
        {
            if (newprice == 0) throw new Exception("value can not be 0");
            
            this.Price = newprice;
        }
    }
}
