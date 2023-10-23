﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProductCatalog.API.Data.Entities.Users;
using ProductCatalog.API.Domain.Interfaces;
using ProductCatalog.API.DTO.Request;
using ProductCatalog.API.DTO.Response;

namespace ProductCatalog.API.Domain.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public UserService(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<UserInfoResponse>> GetUserInfo(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var roles = await _userManager.GetRolesAsync(user);
        var userInfo = _mapper.Map<User, UserInfoResponse>(user);
        userInfo.Roles = roles;
        return new ServiceResponse<UserInfoResponse>(userInfo);
    }

    public async Task<ServiceResponse<UserInfoResponse>> SetUserInfo(ChangeUserInfoRequest userInfo, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (!string.IsNullOrEmpty(userInfo.NewPassword))
        {
            var identityChangePasswordResult =
                await _userManager.ChangePasswordAsync(user, userInfo.OldPassword, userInfo.NewPassword);
            if (!identityChangePasswordResult.Succeeded)
                return new ServiceResponse<UserInfoResponse>("oldPassword mustMatch");
        }

        return new ServiceResponse<UserInfoResponse>(_mapper.Map<User, UserInfoResponse>(user));
    }
}