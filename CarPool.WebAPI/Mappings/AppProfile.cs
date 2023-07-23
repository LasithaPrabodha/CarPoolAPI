using CarPool.Application.Users.Commands;
using CarPool.WebAPI.ViewModels;
using Mapster;

namespace CarPool.WebAPI.Mappings;

public class AppProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<RegisterUserViewModel, RegisterUserCommand>()
            .Map(dest => dest.Roles, src => src.Roles.Select(r => r.ToString()));

    }
}

