using AutoMapper;
using ProductCatalog.API.Data.Entities.Users;
using ProductCatalog.API.DTO.Response;

namespace ProductCatalog.API.Domain.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserInfoResponse>()
            .ForMember(user => user.Email, map => map.MapFrom(userInfoResponse => userInfoResponse.Email))
            .ForMember(user => user.UserName, map => map.MapFrom(userInfoResponse => userInfoResponse.UserName));
    }
}