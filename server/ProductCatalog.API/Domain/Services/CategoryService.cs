using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.API.Data.Entities.Categories;
using ProductCatalog.API.Domain.Interfaces;
using ProductCatalog.API.DTO.Request;
using ProductCatalog.API.DTO.Response;

namespace ProductCatalog.API.Domain.Services;

public class CategoryService : ICategoryService
{
    private readonly ILogger<CategoryService> _logger;
    private readonly IMapper _mapper;
    private readonly IRepositoryCategories _repositoryCategories;

    public CategoryService(IRepositoryCategories repositoryCategories, IMapper mapper, ILogger<CategoryService> logger)
    {
        _repositoryCategories = repositoryCategories;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<CategoryResponse>> GetAllCategories()
    {
        var categories = _repositoryCategories.GetAllQueryable();
        var categoryResponse = categories.ProjectTo<CategoryResponse>(_mapper.ConfigurationProvider).ToList();
        _logger.LogInformation("Fetched all categories");

        return categoryResponse;
    }

    public async Task<Category> AddCategory(CategoryRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ArgumentException("Category name is required", nameof(request.Name));

        var existingCategory = await _repositoryCategories
            .GetAllByQueryable(c => c.Name == request.Name)
            .SingleOrDefaultAsync();

        if (existingCategory != null)
        {
            _logger.LogInformation("Category with id - '{categoryId}' already exists", existingCategory.Id);
            return existingCategory;
        }

        var category = new Category { Id = Guid.NewGuid(), Name = request.Name, };
        var addedCategory = await _repositoryCategories.CreateAsync(category);
        _logger.LogInformation("Category with id - '{categoryId}' was added", category.Id);
        return addedCategory;
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