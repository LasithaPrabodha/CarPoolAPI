using CarPool.Domain.Common;
using CarPool.Domain.Trips;

namespace CarPool.Domain.Trips.Events;

public class BookingAcceptedEvent : DomainEvent
{
    public BookingAcceptedEvent(Booking booking)
    {
        Booking = booking;
    }

    public Booking Booking { get; }
}

