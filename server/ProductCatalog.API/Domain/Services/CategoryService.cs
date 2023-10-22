using AutoMapper;
using AutoMapper.QueryableExtensions;
using Azure.Core;

using ProductCatalog.API.Data.Entities.Categories;
using ProductCatalog.API.Domain.Interfaces;
using ProductCatalog.API.DTO.Request;
using ProductCatalog.API.DTO.Response;

namespace ProductCatalog.API.Domain.Services;

public class CategoryService : ICategoryService
{
    private readonly IRepositoryCategoies _repositoryCategories;
    private readonly IMapper _mapper;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(IRepositoryCategoies repositoryCategories,
        IMapper mapper, ILogger<CategoryService> logger)
    {
        _repositoryCategories = repositoryCategories;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<CategoryResponse>> GetAllCategories()
    {
        var categories = _repositoryCategories.GetAllQueryable();
        var categoryResponse = categories.ProjectTo<CategoryResponse>(_mapper.ConfigurationProvider);
        _logger.LogInformation("Getted all categories");

        return new List<CategoryResponse>(categoryResponse);
    }

    public async Task<Category> AddCategory(CategoryRequest request)
    {
        var findCategory = _repositoryCategories.GetAllByQueryable(c => c.Name == request.Name).FirstOrDefault();

        if (findCategory == null)
        {
            var category = new Category { Id = Guid.NewGuid(), Name = request.Name, };
            _logger.LogInformation("Category with id - '{categoryId}' was added", category.Id);
            return await _repositoryCategories.CreateAsync(category);
        }

        _logger.LogInformation("Category with id - '{categoryId}' already exist", findCategory.Id);
        return findCategory;
    }

    public async Task UpdateCategory(CategoryRequest request)
    {
        var existingCategory = await _repositoryCategories.GetByAsync(p => p.Id == request.Id);
        _mapper.Map(request, existingCategory);
        await _repositoryCategories.UpdateAsync(existingCategory);
        _logger.LogInformation("Category with id - '{categoryId}' was updated", existingCategory.Id);
    }

    public async Task DeleteCategory(Guid categoryId)
    {
        var existingCategory = await _repositoryCategories.GetByAsync(p => p.Id == categoryId);
        await _repositoryCategories.RemoveAsync(existingCategory);
        _logger.LogInformation("Category with id - '{categoryId}' was deleted", existingCategory.Id);
    }
}