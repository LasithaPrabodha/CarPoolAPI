using System;
using System.ComponentModel.DataAnnotations;

namespace CarPool.WebAPI.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Field {0} is required")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Field {0} is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}

