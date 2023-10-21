using AutoMapper;
using AutoMapper.QueryableExtensions;
using ProductCatalog.API.Data.Entities.Products;
using ProductCatalog.API.Domain.Interfaces;
using ProductCatalog.API.DTO.Request;
using ProductCatalog.API.DTO.Response;

namespace ProductCatalog.API.Domain.Services;

public class ProductService : IProductService
{
    private readonly IRepositoryProducts _repositoryProducts;
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IRepositoryProducts repositoryProducts, ICategoryService categoryService, IMapper mapper,
        ILogger<ProductService> logger)
    {
        _repositoryProducts = repositoryProducts;
        _categoryService = categoryService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<ProductResponse>> GetAllProducts()
    {
        var products = _repositoryProducts.GetAllQueryable();
        var productsResponse = products.ProjectTo<ProductResponse>(_mapper.ConfigurationProvider);
        return new List<ProductResponse>(productsResponse);
    }

    public async Task AddProduct(ProductRequest request)
    {
        var product = _mapper.Map<ProductRequest, Product>(request);
        product.Id = Guid.NewGuid();

        product.Category = await _categoryService.AddCategory(request.CategoryName);
        await _repositoryProducts.CreateAsync(product);
        _logger.LogInformation("Product with id - '{productId}' was added", product.Id);
    }

    public async Task UpdateProduct(Guid productId, ProductRequest productRequest)
    {
        var existingProduct = await _repositoryProducts.GetByAsync(p => p.Id == productId);
        _mapper.Map(productRequest, existingProduct);
        await _repositoryProducts.UpdateAsync(existingProduct);
        _logger.LogInformation("Product with id - '{productId}' was updated", existingProduct.Id);
    }

    public async Task DeleteProduct(Guid productId)
    {
        var existingProduct = await _repositoryProducts.GetByAsync(p => p.Id == productId);
        await _repositoryProducts.RemoveAsync(existingProduct);
        _logger.LogInformation("Product with id - '{productId}' was deleted", existingProduct.Id);
    }
}