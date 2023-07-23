using System;
using CarPool.Application.Contracts;
using CarPool.Common;
using CarPool.Domain.Common;
using MediatR;

namespace CarPool.Application.Users.Commands;

public record AddDriverLicenseCommand(string DriverLicenseNumber, DateTime DriverLicenseExpiryDate) : IRequest<Result>
{
}

public class AddDriverLicenseCommandHandler : IRequestHandler<AddDriverLicenseCommand, Result>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticatedUserService _user;

    public AddDriverLicenseCommandHandler(IUserRepository userRepository, IAuthenticatedUserService authenticatedUserService)
    {
        _userRepository = userRepository;
        _user = authenticatedUserService;
    }

    public async Task<Result> Handle(AddDriverLicenseCommand request, CancellationToken cancellationToken)
    {
        var driverLicense = new DriverLicense(
            driverLicenseNumber: request.DriverLicenseNumber,
            driverLicenseExpiryDate: request.DriverLicenseExpiryDate
        );
        var userId = _user.UserId;
        var user = await _userRepository.GetSingleAsync(d => d.Id.ToString() == userId);
        
        user.DriverProfile?.UpdateDriverLicense(driverLicense);

        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync(cancellationToken);

        return new Result().Successful();
    }
}