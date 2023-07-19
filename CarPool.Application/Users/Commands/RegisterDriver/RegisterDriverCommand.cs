using System;

using CarPool.Application.Contracts;
using CarPool.Common;
using MediatR;
using CarPool.Domain.Common;
using CarPool.Domain.Users.Entities;

namespace CarPool.Application.Users.Commands;

public record RegisterDriverCommand(string Username, string Name, string Email, string Password, List<string> Roles,
    RegisterVehicleCommand CreateVehicleCommand, AddDriverLicenseCommand CreateDriverLicenseCommand) : IRequest<Result>
{
}

public class RegisterDriverCommandHandler : IRequestHandler<RegisterDriverCommand, Result>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticatedUserService _authenticatedUserService;

    public RegisterDriverCommandHandler(IUserRepository userRepository, IAuthenticatedUserService authenticatedUserService)
    {
        _userRepository = userRepository;
        _authenticatedUserService = authenticatedUserService;
    }

    public async Task<Result> Handle(RegisterDriverCommand request, CancellationToken cancellationToken)
    {
        var userId = _authenticatedUserService.UserId;
        var user = await _userRepository.GetSingleAsync(u => u.Id.ToString() == userId);

        var vehicle = new Vehicle(
            make: request.CreateVehicleCommand.Make,
            model: request.CreateVehicleCommand.Model,
            year: request.CreateVehicleCommand.Year,
            colour: request.CreateVehicleCommand.Colour,
            licensePlate: request.CreateVehicleCommand.LicensePlate
        );

        var driverLicense = new DriverLicense(
            driverLicenseNumber: request.CreateDriverLicenseCommand.DriverLicenseNumber,
            driverLicenseExpiryDate: request.CreateDriverLicenseCommand.DriverLicenseExpiryDate
        );

        user.AddDriverDetails(vehicle, driverLicense);

        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync(cancellationToken);

        return new Result().Successful();
    }
}
