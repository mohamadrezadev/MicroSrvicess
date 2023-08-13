namespace ProductsService.Models.Entites
{
    public class Category
    {
        public Category()
        {
            Products= new List<Product>();
        }
        public Guid id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
