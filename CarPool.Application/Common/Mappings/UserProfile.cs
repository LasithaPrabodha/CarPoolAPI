using System;
using CarPool.Application.DTOs;
using CarPool.Application.Users.Commands;
using CarPool.Domain.Users;
using Mapster;

namespace CarPool.Application.Common.Mappings;

public class UserProfile : IRegister
{
    

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterUserCommand, UserDTO>()
            .Map(dest=>dest.Id, src=> UserId.CreateUnique());
    }
}

