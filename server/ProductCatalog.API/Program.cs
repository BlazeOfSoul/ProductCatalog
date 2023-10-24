using System.Security.Claims;
using Coravel;
using IdentityModel.AspNetCore.OAuth2Introspection;
using IdentityServer4;
using IdentityServer4.AccessTokenValidation;
using ProductCatalog.API.Configuration;
using ProductCatalog.API.Configuration.Identity;
using ProductCatalog.API.Configuration.IdentityServer;
using ProductCatalog.API.Invocables;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json")
    .AddJsonFile("appsettings.Development.json", true);

builder.Services.AddPersistenceInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServices();
builder.Services.AddIdentity();
builder.Services.AddHttpClient();
builder.Services.AddRepositoreis();
builder.Services.AddApplicationLayer();
builder.Services.AddPersistenceInfrastructure(builder.Configuration);
builder.Services.AddAutoMapper();
builder.Services.AddIdentityServerInfrastructure(builder.Configuration);

builder.Services.AddScheduler();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(IdentityServerConstants.LocalApi.PolicyName, policy =>
    {
        policy.AddAuthenticationSchemes(IdentityServerConstants.LocalApi.AuthenticationScheme)
            .RequireClaim("scope", "openid", "role");
    });

    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("Moderator", policy => policy.RequireRole("Moderator"));
    options.AddPolicy("User", policy => policy.RequireRole("User"));
});

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
    })
    .AddLocalApi(options =>
    {
        options.ExpectedScope = IdentityServerConstants.LocalApi.ScopeName;
        options.SaveToken = true;
    })
    .AddIdentityServerAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.Authority = builder.Configuration.GetValue<string>("ServerUrl");
        options.SupportedTokens = SupportedTokens.Jwt;
        options.RequireHttpsMetadata = false;
        options.RoleClaimType = ClaimTypes.Role;
        options.NameClaimType = ClaimTypes.Name;
        options.ApiName = IdentityServerConstants.LocalApi.ScopeName;
        options.TokenRetriever = req =>
        {
            var fromHeader = TokenRetrieval.FromAuthorizationHeader();
            var fromQuery = TokenRetrieval.FromQueryString();
            var result = fromHeader(req) ?? fromQuery(req);
            return result;
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.SetIsOriginAllowed(e => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

builder.Services.AddLogging(loggingBuilder =>
    loggingBuilder.AddSerilog(dispose: true));
builder.Services.AddLogging(loggingBuilder =>
    loggingBuilder.AddConsole());

LoggerCreation.ConfigureLogger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

ScopeCreation.ConfigureScope(app);

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();
app.UseRouting();
app.UseIdentityServer();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Services.UseScheduler(scheduler => { scheduler.Schedule<DollarExchangeRateChecker>().Daily().RunOnceAtStart(); });

app.Run();