using CarPool.Domain.Users.Enums;

namespace CarPool.Application.Contracts;

public interface IAuthenticatedUserService
{
    string UserId { get; }
    public string Username { get; }
    public IEnumerable<Roles> Roles { get; }
}

