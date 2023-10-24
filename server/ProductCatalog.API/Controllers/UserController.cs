using IdentityServer4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.API.Controllers.Routes;
using ProductCatalog.API.Domain.Interfaces;
using ProductCatalog.API.DTO.Request;

namespace ProductCatalog.API.Controllers;

[Route("api/user/")]
[ApiController]
public class UserController : BaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    public async Task<IActionResult> Get()
    {
        var result = await _userService.GetUserInfo(LoggedInUserUserId);
        return ResultOf(result);
    }

    [HttpPost]
    [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    public async Task<IActionResult> Post([FromBody] ChangeUserInfoRequest userInfo)
    {
        var result = await _userService.SetUserInfo(userInfo, LoggedInUserUserId);
        return ResultOf(result);
    }

    [HttpGet]
    [Route(UserRoutes.GetAllUsers)]
    [Authorize(Roles = "Admin",
        AuthenticationSchemes = IdentityServerConstants.LocalApi.AuthenticationScheme)]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _userService.GetAllUsers();
        return ResultOf(result);
    }
}