using Microsoft.AspNetCore.Mvc;
using ProductCatalog.API.Domain.Interfaces;
using ProductCatalog.API.DTO.Request;

namespace ProductCatalog.API.Controllers;

[Route("api/auth/")]
[ApiController]
public class AuthController : BaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpUserRequest signUpUser)
    {
        var result = await _authService.SignUp(signUpUser);
        return ResultOf(result);
    }

    [HttpGet]
    [Route("check-unique-email")]
    public async Task<IActionResult> CheckUniqueEmail([FromQuery] string email)
    {
        var result = await _authService.CheckUniqueEmail(email);
        return ResultOf(result);
    }

    [HttpGet]
    [Route("check-unique-user-name")]
    public async Task<IActionResult> CheckUniqueUserName([FromQuery] string userName)
    {
        var result = await _authService.CheckUniqueUserName(userName);
        return ResultOf(result);
    }
}