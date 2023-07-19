using AutoMapper;
using CarPool.Application.DTOs;
using CarPool.Application.Trips.Commands;
using CarPool.Application.Users.Commands;
using CarPool.WebAPI.ViewModels;

namespace CarPool.WebAPI.Mappings;

public class AppProfile : Profile
{
    public AppProfile()
    {
        CreateMap<RegisterUserViewModel, RegisterUserCommand>()
            .ForMember(target => target.Roles, opt => opt.MapFrom(m => m.Roles.Select(r => r.ToString())));

        CreateMap<CreateTripViewModel, CreateTripCommand>()
            .ForMember(target => target.AvailableSeats, opt => opt.MapFrom(m => m.Available_Seats))
            .ForMember(target => target.PricePerSeat, opt => opt.MapFrom(m => m.Price_Per_Seat))
            .ForMember(target => target.DepartTime, opt => opt.MapFrom(m => m.Depart_Time));

        CreateMap<LoginViewModel, LoginRequestDTO>();

        CreateMap<AuthResponseDTO, AuthResponseViewModel>();
    }
}

