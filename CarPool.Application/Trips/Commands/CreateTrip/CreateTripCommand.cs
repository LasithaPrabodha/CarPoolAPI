using CarPool.Application.Contracts;
using CarPool.Common;
using CarPool.Domain.Common;
using CarPool.Domain.Trips;
using CarPool.Domain.Users;
using MediatR;

namespace CarPool.Application.Trips.Commands;

public record CreateTripCommand(
    Address Origin,
    Address Destination,
    DateTime DepartTime,
    int AvailableSeats,
    double PricePerSeat,
    Vehicle Vehicle) :IRequest<Result>
{

}

public class CreateTripCommandHandler : IRequestHandler<CreateTripCommand, Result>
{
    private readonly ITripRepository _tripRepository;
    private readonly IAuthenticatedUserService _user;
    private readonly IApplicationUserService _applicationUserService;

    public CreateTripCommandHandler(ITripRepository tripRepository, IAuthenticatedUserService authenticatedUserService,
        IApplicationUserService applicationUserService)
    {
        _tripRepository = tripRepository;
        _user = authenticatedUserService;
        _applicationUserService = applicationUserService;
    }

    public async Task<Result> Handle(CreateTripCommand request, CancellationToken cancellationToken)
    {
        var result = new Result();
        var userId = _user.UserId;
        var user = await _applicationUserService.GetUser(userId);

        
        var trip = Trip.Create(
            driverId: UserId.Create(Guid.Parse(user.Data!.Id)),
            origin: request.Origin,
            destination: request.Destination,
            departTime: request.DepartTime,
            availableSeats: request.AvailableSeats,
            pricePerSeat: request.PricePerSeat,
            vehicle: request.Vehicle
        );

        _tripRepository.Add(trip);
        await _tripRepository.SaveChangesAsync(cancellationToken);

        return result.Successful();
    }
}