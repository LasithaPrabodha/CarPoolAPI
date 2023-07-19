using System;
using System.ComponentModel.DataAnnotations;
using CarPool.Domain.Common;

namespace CarPool.WebAPI.ViewModels;

public class CreateTripViewModel
{
    [Display(Name = "Origin Address")]
    public Address Origin { get; set; }

    [Display(Name = "Destination Address")]
    public Address Destination { get; set; }

    [Display(Name = "Depart Time")]
    public DateTime Depart_Time { get; set; }

    [Display(Name = "Available Seats")]
    public int Available_Seats { get; set; }

    [Display(Name = "Price Per Seat")]
    public double Price_Per_Seat { get; set; }

    [Display(Name = "Vehicle")]
    public Vehicle Vehicle { get; set; }

}

