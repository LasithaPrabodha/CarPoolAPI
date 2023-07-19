using System;
using CarPool.Application.Contracts;
using CarPool.Common;
using CarPool.Domain.Common;
using MediatR;

namespace CarPool.Application.Users.Commands;

public record RegisterVehicleCommand(string Make, string Model, int Year, string Colour, string LicensePlate)
    : IRequest<Result>
{

}

public class RegisterVehicleCommandHandler : IRequestHandler<RegisterVehicleCommand, Result>
{

    private readonly IUserRepository _userRepository;
    private readonly IAuthenticatedUserService _authenticatedUserService;

    public RegisterVehicleCommandHandler(IUserRepository userRepository, IAuthenticatedUserService authenticatedUserService)
    {
        _userRepository = userRepository;
        _authenticatedUserService = authenticatedUserService;
    }

    public async Task<Result> Handle(RegisterVehicleCommand request, CancellationToken cancellationToken)
    {
        var userId = _authenticatedUserService.UserId;
        var user = await _userRepository.GetSingleAsync(d => d.Id.ToString() == userId);

        var vehicle = new Vehicle(
          make: request.Make,
          model: request.Model,
          year: request.Year,
          colour: request.Colour,
          licensePlate: request.LicensePlate
        );

        user.DriverProfile?.UpdateVehicle(vehicle);
        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync(cancellationToken);

        return new Result().Successful();
    }
}
