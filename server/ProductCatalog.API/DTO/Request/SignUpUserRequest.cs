using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.API.DTO.Request;

public class SignUpUserRequest
{
    [Required] [EmailAddress] public string Email { get; set; }

    [Required] public string UserName { get; set; }

    [Required] public string Password { get; set; }

    [Required] [Compare("Password")] public string ConfirmPassword { get; set; }
}