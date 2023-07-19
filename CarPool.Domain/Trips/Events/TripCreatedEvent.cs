using CarPool.Domain.Common;

namespace CarPool.Domain.Trips.Events;

public class TripCreatedEvent : DomainEvent
{
    public TripCreatedEvent(Trip trip)
    {
        Trip = trip;
    }

    public Trip Trip { get; }
}

