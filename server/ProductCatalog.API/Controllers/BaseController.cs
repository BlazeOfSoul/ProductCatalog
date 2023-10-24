using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.API.DTO.Response;

namespace ProductCatalog.API.Controllers;

public class BaseController : ControllerBase
{
    public string? LoggedInUserUserId
    {
        get
        {
            return User.Identity!.IsAuthenticated ? User.FindFirst(i => i.Type == JwtClaimTypes.Subject).Value : null;
        }
    }

    protected IActionResult ResultOf<TModel>(ServiceResponse<TModel> answer)
    {
        if (answer.Success) return Ok(answer.ModelRequest);

        return BadRequest(answer.Error);
    }

    protected IActionResult ResultOf(ServiceResponse answer)
    {
        if (answer.Success) return Ok();

        return BadRequest(answer.Error);
    }

    protected bool IsUserAdminOrModerator()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity!;
        return claimsIdentity.HasClaim("role", "Admin") || claimsIdentity.HasClaim("role", "Moderator");
    }
}