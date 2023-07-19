using CarPool.Domain.Users;

namespace CarPool.Application.Contracts;

public interface IUserRepository : IAggregateRepository<User, Guid>
{
}

