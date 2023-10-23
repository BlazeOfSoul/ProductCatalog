using AutoMapper;
using AutoMapper.QueryableExtensions;
using ProductCatalog.API.Data.Entities.Products;
using ProductCatalog.API.Domain.Interfaces;
using ProductCatalog.API.DTO.Request;
using ProductCatalog.API.DTO.Request.Product;
using ProductCatalog.API.DTO.Response;
using ProductCatalog.API.Invocables;

namespace ProductCatalog.API.Domain.Services;

public class ProductService : IProductService
{
    private readonly IRepositoryProducts _repositoryProducts;
    private readonly ICategoryService _categoryService;
    private readonly DollarExchangeRateChecker _dollarExchangeRateChecker;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IRepositoryProducts repositoryProducts, ICategoryService categoryService, DollarExchangeRateChecker dollarExchangeRateChecker, IMapper mapper,
        ILogger<ProductService> logger)
    {
        _repositoryProducts = repositoryProducts;
        _categoryService = categoryService;
        _dollarExchangeRateChecker = dollarExchangeRateChecker;
        _mapper = mapper;
        _logger = logger;
    }

    public List<ProductResponse> GetAllProductsPartial()
    {
        var products = _repositoryProducts.GetAllQueryable().ProjectTo<ProductResponse>(_mapper.ConfigurationProvider).ToList();
        products.ForEach(p => p.SpecialNote = null);

        ApplyDollarRate(products);
        _logger.LogInformation("Getted all products for user");

        return products;
    }

    public List<ProductResponse> GetAllProductsFull()
    {
        var products = _repositoryProducts.GetAllQueryable().ProjectTo<ProductResponse>(_mapper.ConfigurationProvider).ToList();

        ApplyDollarRate(products);
        _logger.LogInformation("Getted all products for admin or moderator");

        return products;
    }

    public List<ProductResponse> GetAllProductsByCategoryName(string categoryName)
    {
        var products = _repositoryProducts.GetAllByQueryable(p => p.Category!.Name == categoryName).ProjectTo<ProductResponse>(_mapper.ConfigurationProvider).ToList();
        ApplyDollarRate(products);
        _logger.LogInformation("Getted all products with category name - '{name}'", categoryName);

        return products;
    }

    public async Task AddProduct(ProductRequest request)
    {
        var product = _mapper.Map<ProductRequest, Product>(request);
        product.Id = Guid.NewGuid();

        product.Category = await _categoryService.AddCategory(new CategoryRequest { Name = request.CategoryName });
        await _repositoryProducts.CreateAsync(product);
        _logger.LogInformation("Product with id - '{productId}' was added", product.Id);
    }

    public async Task UpdateProduct(ProductRequest productRequest)
    {
        var existingProduct = await _repositoryProducts.GetByAsync(p => p.Id == productRequest.Id);
        existingProduct.Category = await _categoryService.AddCategory(new CategoryRequest { Name = productRequest.CategoryName });

        _mapper.Map(productRequest, existingProduct);
        await _repositoryProducts.UpdateAsync(existingProduct);
        _logger.LogInformation("Product with id - '{productId}' was updated", existingProduct.Id);
    }

    public async Task UpdateProductUser(ProductRequestUser productRequest)
    {
        var existingProduct = await _repositoryProducts.GetByAsync(p => p.Id == productRequest.Id);
        existingProduct.Category = await _categoryService.AddCategory(new CategoryRequest { Name = productRequest.CategoryName });

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

    private void ApplyDollarRate(List<ProductResponse> products)
    {
        var dollarRate = _dollarExchangeRateChecker.GetDollarRate();
        products.ForEach(p => p.PriceInDollars = Math.Round(p.PriceInRubles / dollarRate, 2));
    }
}