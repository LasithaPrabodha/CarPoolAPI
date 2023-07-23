using System;
namespace CarPool.Application.DTOs;

public class UserDTO
{
    public string Id { get; set; }
    public string Username { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string? PhoneNumber { get; set; }

}

