
using CarPool.Domain.Users.Enums;

namespace CarPool.Application.DTOs;

public class UpdateUserDetailsDTO
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime? DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; } 
    public Gender? Gender { get; set; }
    public string? Bio { get; set; }
    public bool? IsActive { get; set; }
}


