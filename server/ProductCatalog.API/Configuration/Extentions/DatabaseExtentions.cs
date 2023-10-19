using Microsoft.EntityFrameworkCore;
using ProductCatalog.API.Data;

namespace ProductCatalog.API.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}