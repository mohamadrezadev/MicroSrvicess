namespace ProductsService.Models.Services
{
    public interface IProductService
    {
        List<ProductDto> GetProducts();
        ProductDto GetProduct(Guid id);
        Guid AddNewProduct(AddNewProductDto product);
        bool UpdateProduct(UpdateProductDto updateProduct);
    }
        
    public record UpdateProductDto(Guid ProductId, string Name,double Price);
    public class ProductDto
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public ProductCategoryDto productCategory { get; set;}
    }
    public class ProductCategoryDto
    {
        public Guid CategoryId { get; set; }
        public string Category { get; set; }
    }
    public class AddNewProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public Guid CategoryId { get; set; }
    }
}
