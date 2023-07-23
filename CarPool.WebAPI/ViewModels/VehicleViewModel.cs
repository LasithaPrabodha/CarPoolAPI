using System;
using System.ComponentModel.DataAnnotations;

namespace CarPool.WebAPI.ViewModels;

public class VehicleViewModel
{

    [Required(ErrorMessage = "Field {0} is required")]
    [MaxLength(20, ErrorMessage = "Field {0} must be at least {1} characters long")]
    public string Make { get; set; }

    [Required(ErrorMessage = "Field {0} is required")]
    [MaxLength(20, ErrorMessage = "Field {0} must be at least {1} characters long")]
    public string Model { get; set; }

    [Required(ErrorMessage = "Field {0} is required")]
    [YearRange(1990, ErrorMessage = "The Year must be between 1900 and the current year.")]
    public int Year { get; set; }

    [Required(ErrorMessage = "Field {0} is required")]
    [MaxLength(20, ErrorMessage = "Field {0} must be at least {1} characters long")]
    public string Colour { get; set; }

    [Required(ErrorMessage = "Field {0} is required")]
    [MaxLength(7, ErrorMessage = "Field {0} must be at least {1} characters long")]
    public string LicensePlate { get; set; }
}

