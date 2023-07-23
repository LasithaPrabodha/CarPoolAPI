using System;
using CarPool.Application.DTOs;
using CarPool.Common;
using CarPool.Domain.Users.Enums;
using CarPool.Infrastructure.Identity;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace CarPool.Infrastructure.Mappings;

internal class UserProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<ApplicationUserRole, Roles>()
            .Map(dest => dest, src => src.Role.Name.ToEnum<Roles>());

        config.NewConfig<UserDTO, ApplicationUser>();

        config.NewConfig<IdentityError, ResultError>()
            .Map(dest => dest.Error, src => src.Description);
    }
}

