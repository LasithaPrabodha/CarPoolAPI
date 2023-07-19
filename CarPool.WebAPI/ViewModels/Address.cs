using System;
using System.ComponentModel.DataAnnotations;

namespace CarPool.WebAPI.ViewModels;

public class Address
{
    [Required(ErrorMessage = "Field {0} is required")]
    [MinLength(128, ErrorMessage = "Field {0} must be at least {1} characters long")]
    public string Street { get; set; }

    [Required(ErrorMessage = "Field {0} is required")]
    [MinLength(20, ErrorMessage = "Field {0} must be at least {1} characters long")]
    public string City { get; set; }

    [Required(ErrorMessage = "Field {0} is required")]
    [MinLength(10, ErrorMessage = "Field {0} must be at least {1} characters long")]
    public string Province { get; set; }

    [Required(ErrorMessage = "Field {0} is required")]
    [MinLength(7, ErrorMessage = "Field {0} must be at least {1} characters long")]
    public string PostalCode { get; set; }
}

