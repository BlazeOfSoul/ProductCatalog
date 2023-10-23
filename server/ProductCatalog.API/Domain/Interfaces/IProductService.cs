using ProductCatalog.API.DTO.Request;
using ProductCatalog.API.DTO.Request.Product;
using ProductCatalog.API.DTO.Response;

namespace ProductCatalog.API.Domain.Interfaces;

public interface IProductService
{
    List<ProductResponse> GetAllProductsPartial();

    List<ProductResponse> GetAllProductsFull();

    List<ProductResponse> GetAllProductsByCategoryName(string categoryName);

    Task AddProduct(ProductRequest request);

    Task UpdateProduct(ProductRequest request);

    Task UpdateProductUser(ProductRequestUser request);

    Task DeleteProduct(Guid productId);
}