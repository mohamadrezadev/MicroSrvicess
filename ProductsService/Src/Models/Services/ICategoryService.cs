using ProductsService.Infrastructure.Contexts;
using ProductsService.Models.Entites;

namespace ProductsService.Models.Services
{
    public interface ICategoryService
    {
        List<CategoryDto> GetCategories();
        Guid AddNewCategory(CategoryDto category);
    }
    public class CategoryService : ICategoryService
    {
        private readonly ProductsDatabaseContext _context;

        public CategoryService(ProductsDatabaseContext context)
        {
            _context = context;
        }
        public Guid AddNewCategory(CategoryDto category)
        {
            Category newCategory = new Category
            {
                Description = category.Description,
                Name = category.Name,
                Title = category.Title,

            };
            _context.Categories.Add(newCategory);
            _context.SaveChanges();
            return newCategory.id;

        }

        public List<CategoryDto> GetCategories()
        {
            var data=_context.Categories
                .OrderBy(c => c.Name)
                .Select(p=>new CategoryDto 
                { 
                    Name=p.Name,
                    Description=p.Description,
                    Title=p.Title,
                    Id=p.id
                })
                .ToList();
            return data;
        }
    }
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
}
