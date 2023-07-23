using System;
namespace CarPool.Domain.Common;

public class TripNotFoundException : Exception
{
    public TripNotFoundException(Guid bookingId) : base($"No trip found under Id \"{bookingId}\"")
    {
    }
}

