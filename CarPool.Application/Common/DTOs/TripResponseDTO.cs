using System;
using CarPool.Domain.Common;

namespace CarPool.Application.DTOs;

public class TripResponseDTO
{
    public AddressDTO Origin { get;  set; }
    public AddressDTO Destination { get;  set; }
    public DateTime DepartTime { get;  set; }
    public int AvailableSeats { get;  set; }
    public double PricePerSeat { get;  set; }
}

