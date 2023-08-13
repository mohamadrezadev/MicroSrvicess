using ProductsService.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;

namespace ProductsServiceTest.MockData
{
    public class CategoryMockData
    {
        List<CategoryDto> Categories=new List<CategoryDto>();
        public List<CategoryDto> GetCategories()
        {
            Categories.AddRange(new Filler<CategoryDto>().Create(20));
            return Categories;
        }
        public List<CategoryDto> Emptycategory()
        {
            return new List<CategoryDto>(new Filler<CategoryDto>().Create(0));
        }
    }

}
