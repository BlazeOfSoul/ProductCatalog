using ProductCatalog.API.Data.Entities.Categories;
using ProductCatalog.API.Data.Entities.Products;
using ProductCatalog.API.Data.Entities.Users;
using ProductCatalog.API.Data.Repositories;

namespace ProductCatalog.API.Configuration;

public static class RepositoryExtentions
{
    public static void AddRepositoreis(this IServiceCollection services)
    {
        services.AddScoped<IRepositioryUsers, RepositoryUsers>();
        services.AddScoped<IRepositoryProducts, RepositoryProducts>();
        services.AddScoped<IRepositoryCategories, RepositoryCategories>();
    }
}