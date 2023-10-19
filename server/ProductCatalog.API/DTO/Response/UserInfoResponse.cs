using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.API.DTO.Response;

public class UserInfoResponse
{
    [Required] [EmailAddress] public string Email { get; set; }

    [Required] public string UserName { get; set; }
}