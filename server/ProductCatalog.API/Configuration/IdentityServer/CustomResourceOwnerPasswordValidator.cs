﻿using IdentityModel;
using IdentityServer4.Events;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using ProductCatalog.API.Data.Entities.Users;

namespace ProductCatalog.API.Configuration.IdentityServer;

public class CustomResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IEventService _events;
    private readonly ILogger<CustomResourceOwnerPasswordValidator> _logger;

    public CustomResourceOwnerPasswordValidator(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IEventService events,
        ILogger<CustomResourceOwnerPasswordValidator> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _events = events;
        _logger = logger;
    }

    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        var user = await _userManager.FindByEmailAsync(context.UserName);
        if (user != null)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(user, context.Password, true);
            if (result.Succeeded)
            {
                await SendSuccessResponse(context, user);
            }
            else if (result.IsLockedOut)
            {
                await SendFailureResponse(context, "Authentication failed for username: {username}, reason: locked out",
                    "lockedOut");
            }
            else if (result.IsNotAllowed)
            {
                await SendFailureResponse(context,
                    "Authentication failed for username: {username}, reason: not allowed", "notAllowed");
            }
            else if (result.RequiresTwoFactor)
            {
                await SendFailureResponse(context,
                    "Authentication failed for username: {username}, reason: require two factor", "requiresTwoFactor");
            }
            else
            {
                await SendFailureResponse(context,
                    "Authentication failed for username: {username}, reason: invalid credentials",
                    "invalidCredentials");
            }
        }
        else
        {
            await SendFailureResponse(context,
                "Authentication failed for username: {username}, reason: invalid credentials", "invalidCredentials");
        }
    }

    private async Task SendFailureResponse(ResourceOwnerPasswordValidationContext context, string information,
        string error)
    {
        _logger.LogInformation(information, context.UserName);
        await _events.RaiseAsync(new UserLoginFailureEvent(context.UserName, error, interactive: false));
        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, error);
    }

    private async Task SendSuccessResponse(ResourceOwnerPasswordValidationContext context, User user)
    {
        var userID = await _userManager.GetUserIdAsync(user);
        _logger.LogInformation("Credentials validated for username: {username}", context.UserName);
        await _events.RaiseAsync(new UserLoginSuccessEvent(context.UserName, userID, context.UserName,
            interactive: false));
        context.Result = new GrantValidationResult(userID, OidcConstants.AuthenticationMethods.Password);
    }
}