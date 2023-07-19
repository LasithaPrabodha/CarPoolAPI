using System;
using System.ComponentModel.DataAnnotations;
using CarPool.Domain.Users.Enums;

namespace CarPool.WebAPI.ViewModels;

public class RegisterUserViewModel
{
    [Required(ErrorMessage = "Field {0} is required")]
    [Display(Name = "Username")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Field {0} is required")]
    [Display(Name = "Password")]
    [MinLength(10, ErrorMessage = "Field {0} must be at least {1} characters long")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "Field {0} is required")]
    [Display(Name = "Name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Field {0} is required")]
    [Display(Name = "Email")]
    [EmailAddress(ErrorMessage = "Field {0} is not a valid email address")]
    public string Email { get; set; }

    [Display(Name = "PhoneNumber")]
    public string PhoneNumber { get; set; }

    [Display(Name = "Roles")]
    [Required(ErrorMessage = "Field {0} is required")]
    public IEnumerable<Roles> Roles { get; set; }
}

