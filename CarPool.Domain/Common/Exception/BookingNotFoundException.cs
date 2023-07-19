using System;
namespace CarPool.Domain.Common;

public class BookingNotFoundException : Exception
{
    public BookingNotFoundException(Guid bookingId) : base($"No booking found under Id \"{bookingId}\"")
    {
    }
}

