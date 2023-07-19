using CarPool.Domain.Common;
using CarPool.Domain.Trips;

namespace CarPool.Domain.Trips.Events;

public class BookingDeclinedEvent : DomainEvent
{
    public BookingDeclinedEvent(Booking booking)
    {
        Booking = booking;
    }

    public Booking Booking { get; }
}

