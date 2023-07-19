using CarPool.Domain.Common;
using CarPool.Domain.Trips;

namespace CarPool.Domain.Trips.Events;

public class BookingMadeEvent : DomainEvent
{
    public BookingMadeEvent(Booking booking)
    {
        Booking = booking;
    }

    public Booking Booking { get; }
}

