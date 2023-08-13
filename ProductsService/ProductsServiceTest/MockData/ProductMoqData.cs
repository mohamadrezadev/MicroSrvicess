using ProductsService.Models.Services;
using Tynamix.ObjectFiller;

namespace ProductsServiceTest.MockData
{
    public class ProductMoqData
    {
        List<ProductDto> products=new List<ProductDto>();
        
        public List<ProductDto> GetProducts()
        {
            products.AddRange(new Filler<ProductDto>().Create(10));
            return products;
        }
        public List<ProductDto> Emptyproducts()
        {
            return null;
        }
        public ProductDto ReturnProduct() 
        {
            return new Filler<ProductDto>().Create();
        }
        public ProductDto Emptyproduct() 
        {
            return null;
        }
        public bool UpdateProduct()
        {
            return true;
        }
        public bool notUpdateProduct()
        {
            return false;
        }
    }
}
