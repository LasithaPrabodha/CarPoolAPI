using CarPool.Application.Contracts;
using CarPool.Common;
using CarPool.Domain.Common;
using CarPool.Domain.Trips;
using CarPool.Domain.Users;
using MediatR;

namespace CarPool.Application.Trips.Commands;

public record CreateTripCommand(
    Address Origin,
    Address Destionation,
    DateTime DepartTime,
    int AvailableSeats,
    double PricePerSeat,
    Vehicle Vehicle) :IRequest<Result>
{

}

public class CreateTripCommandHandler : IRequestHandler<CreateTripCommand, Result>
{
    private readonly ITripRepository _tripRepository;
    private readonly IAuthenticatedUserService _authenticatedUserService;
    private readonly IApplicationUserService _applicationUserService;

    public CreateTripCommandHandler(ITripRepository tripRepository, IAuthenticatedUserService authenticatedUserService,
        IApplicationUserService applicationUserService)
    {
        _tripRepository = tripRepository;
        _authenticatedUserService = authenticatedUserService;
        _applicationUserService = applicationUserService;
    }

    public async Task<Result> Handle(CreateTripCommand request, CancellationToken cancellationToken)
    {
        var result = new Result();
        var userId = _authenticatedUserService.UserId;
        var user = await _applicationUserService.GetUser(userId);

        var trip = Trip.Create(
            driverId: UserId.Create(user.Data.Id.Value),
            origin: request.Origin,
            destination: request.Destionation,
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