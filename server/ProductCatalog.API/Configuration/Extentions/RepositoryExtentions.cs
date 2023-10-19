using ProductCatalog.API.Data.Entities.Users;
using ProductCatalog.API.Data.Repositories;

namespace ProductCatalog.API.Configuration;

public static class RepositoryExtentions
{
    public static void AddRepositoreis(this IServiceCollection services)
    {
        services.AddScoped<IRepositioryUsers, RepositoryUsers>();
    }
}