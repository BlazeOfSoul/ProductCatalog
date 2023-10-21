using ProductCatalog.API.DTO.Request;
using ProductCatalog.API.DTO.Response;

namespace ProductCatalog.API.Domain.Interfaces;

public interface IProductService
{
    public Task<List<ProductResponse>> GetAllProducts();

    Task AddProduct(ProductRequest productRequest);

    Task UpdateProduct(Guid productId, ProductRequest productRequest);

    Task DeleteProduct(Guid productId);
}