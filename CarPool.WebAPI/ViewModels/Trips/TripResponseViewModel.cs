using System;
using CarPool.Application.DTOs;

namespace CarPool.WebAPI.ViewModels;

public class TripResponseViewModel
{

    public AddressViewModel Origin { get; set; }
    public AddressViewModel Destination { get; set; }
    public DateTime DepartTime { get; set; }
    public int AvailableSeats { get; set; }
    public double PricePerSeat { get; set; }
}

