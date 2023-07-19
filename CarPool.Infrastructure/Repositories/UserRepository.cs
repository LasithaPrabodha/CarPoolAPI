using CarPool.Application.Contracts;
using CarPool.Domain.Users;
using CarPool.Infrastructure.Persistence;

namespace CarPool.Infrastructure.Repositories;

internal class UserRepository : EFRepository<User, Guid>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }
}

