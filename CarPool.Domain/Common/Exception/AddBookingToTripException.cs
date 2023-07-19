using System;
namespace CarPool.Domain.Common;

public class AddBookingToTripException : Exception
{
    public AddBookingToTripException(string errorMessage) : base(errorMessage)
    {
    }
}

