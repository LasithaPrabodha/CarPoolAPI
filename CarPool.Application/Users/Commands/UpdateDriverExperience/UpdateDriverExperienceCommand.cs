using System;
using CarPool.Application.Contracts;
using CarPool.Common;
using MediatR;

namespace CarPool.Application.Users.Commands.UpdateDriverExperience;

public record UpdateDriverExperienceCommand(int TotalKmsShared, int TotalPassengersDriven) : IRequest<Result>
{
}

public class UpdateDriverExperienceCommandHandler : IRequestHandler<UpdateDriverExperienceCommand, Result>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticatedUserService _user;

    public UpdateDriverExperienceCommandHandler(IUserRepository userRepository, IAuthenticatedUserService authenticatedUserService)
    {
        _userRepository = userRepository;
        _user = authenticatedUserService;
    }

    public async Task<Result> Handle(UpdateDriverExperienceCommand request, CancellationToken cancellationToken)
    {
        var userId = _user.UserId;
        var user = await _userRepository.GetSingleAsync(d => d.Id.ToString() == userId);

        user.DriverProfile?.UpdateExperience(
            totalKmsShared: user.DriverProfile.TotalKmsShared + request.TotalKmsShared,
            totalPassengersDriven: user.DriverProfile.TotalPassengersDriven + request.TotalPassengersDriven
       );

        _userRepository.Update(user);

        await _userRepository.SaveChangesAsync(cancellationToken);

        return new Result().Successful();
    }
}
