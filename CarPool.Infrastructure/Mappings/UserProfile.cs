using System;
using AutoMapper;
using CarPool.Common;
using CarPool.Domain.Users;
using CarPool.Domain.Users.Enums;
using CarPool.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace CarPool.Infrastructure.Mappings;

internal class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<ApplicationUserRole, Roles>()
            .ConvertUsing(r => r.Role.Name.ToEnum<Roles>());

        CreateMap<User, ApplicationUser>()
            .ForMember(target => target.Roles, opt => opt.Ignore())
            .ForMember(target => target.Id, opt => opt.MapFrom(source => source.Id.Value));

        CreateMap<ApplicationUser, User>();

        //CreateMap<ApplicationUser, UserReadModel>();

        CreateMap<IdentityError, ResultError>()
            .ForMember(target => target.Error, opt => opt.MapFrom(source => source.Description));
    }
}

