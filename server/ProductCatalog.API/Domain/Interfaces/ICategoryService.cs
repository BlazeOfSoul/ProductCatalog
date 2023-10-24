using ProductCatalog.API.Data.Entities.Categories;
using ProductCatalog.API.DTO.Request;
using ProductCatalog.API.DTO.Response;

namespace ProductCatalog.API.Domain.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryResponse>> GetAllCategories();

    Task<Category> AddCategory(CategoryRequest request);

    Task UpdateCategory(CategoryRequest request);

    Task DeleteCategory(Guid categoryId);
}