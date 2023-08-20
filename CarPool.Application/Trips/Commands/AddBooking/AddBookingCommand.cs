using System;
using CarPool.Application.Contracts;
using CarPool.Common;
using CarPool.Domain.Common;
using CarPool.Domain.Trips;
using CarPool.Domain.Users;
using MediatR;

namespace CarPool.Application.Trips.Commands;

public record AddBookingCommand(TripId TripId, UserId UserId, int RequiredSeats, string? Description) : IRequest<Result<Guid>>
{
}

public class AddBookingCommandHandler : IRequestHandler<AddBookingCommand, Result<Guid>>
{

    private readonly ITripRepository _tripRepository;


    public AddBookingCommandHandler(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }

    public async Task<Result<Guid>> Handle(AddBookingCommand request, CancellationToken cancellationToken)
    {
        var result = new Result<Guid>();

        var trip = await _tripRepository.GetSingleAsync(t => t.Id == request.TripId);

        if (trip is null)
            return result.Failed().WithException(new TripNotFoundException(request.TripId.Value));


        var bookingId = trip.AddBooking(userId: request.UserId, requiredSeats: request.RequiredSeats, description: request.Description);
        _tripRepository.Update(trip);
        await _tripRepository.SaveChangesAsync(cancellationToken);

        result.Data = bookingId.Value;

        return result.Successful();
    }
}