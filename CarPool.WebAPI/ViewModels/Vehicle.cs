using System;
using System.ComponentModel.DataAnnotations;

namespace CarPool.WebAPI.ViewModels;

public class Vehicle
{

    [Required(ErrorMessage = "Field {0} is required")]
    [MinLength(20, ErrorMessage = "Field {0} must be at least {1} characters long")]
    public string Make { get; set; }

    [Required(ErrorMessage = "Field {0} is required")]
    [MinLength(20, ErrorMessage = "Field {0} must be at least {1} characters long")]
    public string Model { get; set; }

    [Required(ErrorMessage = "Field {0} is required")]
    [YearRange(1990, ErrorMessage = "The Year must be between 1900 and the current year.")]
    public int Year { get; set; }

    [Required(ErrorMessage = "Field {0} is required")]
    [MinLength(20, ErrorMessage = "Field {0} must be at least {1} characters long")]
    public string Colour { get; set; }

    [Required(ErrorMessage = "Field {0} is required")]
    [MinLength(7, ErrorMessage = "Field {0} must be at least {1} characters long")]
    public string License_Plate { get; set; }
}

