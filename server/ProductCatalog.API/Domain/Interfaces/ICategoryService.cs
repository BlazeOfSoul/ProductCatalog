using ProductCatalog.API.Data.Entities.Categories;
using ProductCatalog.API.DTO.Request;
using ProductCatalog.API.DTO.Response;

namespace ProductCatalog.API.Domain.Interfaces;

public interface ICategoryService
{
    public Task<List<CategoryResponse>> GetAllCategories();

    public Task<Category> AddCategory(string categoryName);

    public Task UpdateCategory(Guid categoryId, CategoryRequest productRequest);

    public Task DeleteCategory(Guid productId);
}