using ProductCatalog.API.Domain.Interfaces;
using ProductCatalog.API.Domain.Services;
using ProductCatalog.API.Invocables;

namespace ProductCatalog.API.Configuration;

public static class ServiceExtentions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddSingleton<DollarExchangeRateChecker>();
    }
}