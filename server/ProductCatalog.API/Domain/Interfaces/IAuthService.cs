using ProductCatalog.API.DTO.Request;
using ProductCatalog.API.DTO.Response;

namespace ProductCatalog.API.Domain.Interfaces;

public interface IAuthService
{
    Task<ServiceResponse<SignUpUserRequest>> SignUp(SignUpUserRequest signUpUser);

    Task<ServiceResponse> CheckUniqueEmail(string email);

    Task<ServiceResponse> CheckUniqueUserName(string userName);
}