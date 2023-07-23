using System;
using System.ComponentModel.DataAnnotations;

namespace CarPool.WebAPI.ViewModels;

public class CreateTripViewModel
{
    [Display(Name = "Origin Address")]
    public AddressViewModel Origin { get; set; }

    [Display(Name = "Destination Address")]
    public AddressViewModel Destination { get; set; }

    [Display(Name = "Depart Time")]
    [Required(ErrorMessage = "Field {0} is required")]
    public DateTime DepartTime { get; set; }

    [Display(Name = "Available Seats")]
    [Required(ErrorMessage = "Field {0} is required")]
    public int AvailableSeats { get; set; }

    [Display(Name = "Price Per Seat")]
    [Required(ErrorMessage = "Field {0} is required")]
    public double PricePerSeat { get; set; }

    [Display(Name = "Vehicle")]
    public VehicleViewModel Vehicle { get; set; }

}

