using ProductCatalog.API.Data.Entities.Users;

namespace ProductCatalog.API.Configuration.IdentityServer;

public static class IdentityServerExtentions
{
    public static IServiceCollection AddIdentityServerInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        var identityServerSettingsSection = configuration.GetSection(nameof(IdentityServerSettings));
        var identityServerSettings = identityServerSettingsSection.Get<IdentityServerSettings>();

        services.Configure<IdentityServerSettings>(identityServerSettingsSection);

        services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
            .AddInMemoryClients(IdentityServerConfiguration.GetClients(identityServerSettings))
            .AddInMemoryIdentityResources(IdentityServerConfiguration.GetIdentityResources())
            .AddInMemoryApiResources(IdentityServerConfiguration.GetApis())
            .AddInMemoryApiScopes(IdentityServerConfiguration.GetApiScopes())
            .AddAspNetIdentity<User>()
            .AddResourceOwnerValidator<CustomResourceOwnerPasswordValidator>()
            .AddDeveloperSigningCredential();

        return services;
    }
}