namespace Microservice.Web.FrontEnd.Services.ProductService
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetProducts();
        ProductDto GetProductById(Guid id);

    }

}
