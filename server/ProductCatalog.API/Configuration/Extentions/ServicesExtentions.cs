using ProductCatalog.API.Domain.Interfaces;
using ProductCatalog.API.Domain.Services;

namespace ProductCatalog.API.Configuration;

public static class ServicesExtentions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}