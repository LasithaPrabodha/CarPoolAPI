using System;
namespace CarPool.WebAPI.ViewModels;

public class AuthResponseViewModel
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
}

