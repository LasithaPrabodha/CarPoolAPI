using CarPool.Application.Contracts;
using CarPool.Domain.Trips;
using CarPool.Infrastructure.Persistence;

namespace CarPool.Infrastructure.Repositories;

internal class TripRepository : EFRepository<Trip, Guid>, ITripRepository
{
    public TripRepository(ApplicationDbContext context) : base(context)
    {
    }
}

