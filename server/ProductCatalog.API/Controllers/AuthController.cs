using Microsoft.AspNetCore.Mvc;
using ProductCatalog.API.Controllers.Routes;
using ProductCatalog.API.Domain.Interfaces;
using ProductCatalog.API.DTO.Request;

namespace ProductCatalog.API.Controllers;

[ApiController]
public class AuthController : BaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route(AuthRoutes.SignUp)]
    public async Task<IActionResult> SignUp([FromBody] SignUpUserRequest signUpUser)
    {
        var result = await _authService.SignUp(signUpUser);
        return ResultOf(result);
    }

    [HttpGet]
    [Route(AuthRoutes.ChechUserEmail)]
    public async Task<IActionResult> CheckUniqueEmail([FromQuery] string email)
    {
        var result = await _authService.CheckUniqueEmail(email);
        return ResultOf(result);
    }

    [HttpGet]
    [Route(AuthRoutes.ChechUniqueUserName)]
    public async Task<IActionResult> CheckUniqueUserName([FromQuery] string userName)
    {
        var result = await _authService.CheckUniqueUserName(userName);
        return ResultOf(result);
    }
}