using ProductCatalog.API.DTO.Request;
using ProductCatalog.API.DTO.Response;

namespace ProductCatalog.API.Domain.Interfaces;

public interface IUserService
{
    Task<ServiceResponse<UserInfoResponse>> GetUserInfo(string userId);

    Task<ServiceResponse<UserInfoResponse>> SetUserInfo(ChangeUserInfoRequest userInfo, string userId);

    Task<ServiceResponse<List<UserInfoResponse>>> GetAllUsers();
}