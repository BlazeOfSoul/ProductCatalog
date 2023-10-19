﻿using Microsoft.AspNetCore.Identity;
using ProductCatalog.API.Data;
using ProductCatalog.API.Data.Entities.Users;

namespace ProductCatalog.API.Configuration.Identity;

public static class IdentityExtention
{
    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = false;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(900);
                options.Lockout.MaxFailedAccessAttempts = 20;
            })
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddUserManager<UserManager<User>>()
            .AddDefaultTokenProviders();

        return services;
    }
}