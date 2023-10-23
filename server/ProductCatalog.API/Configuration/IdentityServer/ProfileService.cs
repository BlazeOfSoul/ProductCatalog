using System.Security.Claims;

using IdentityModel;

using IdentityServer4.Models;
using IdentityServer4.Services;

using Microsoft.AspNetCore.Identity;

using ProductCatalog.API.Data.Entities.Users;

public class ProfileService : IProfileService
{
    private readonly UserManager<User> _userManager;

    public ProfileService(
        UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        context.IssuedClaims.AddRange(context.Subject.Claims);

        var user = await _userManager.GetUserAsync(context.Subject);

        var roles = await _userManager.GetRolesAsync(user);

        foreach (var role in roles)
        {
            context.IssuedClaims.Add(new Claim(JwtClaimTypes.Role, role));
        }
    }

    public Task IsActiveAsync(IsActiveContext context)
    {
        return Task.CompletedTask;
    }
}
