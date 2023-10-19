using AutoMapper;
using ProductCatalog.API.Domain.Mapping;

namespace ProductCatalog.API.Configuration;

public static class AutoMapperExtentions
{
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new UserProfile()); });
        var mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }
}