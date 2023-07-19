using CarPool.Domain.Trips;

namespace CarPool.Application.Contracts;

public interface ITripRepository : IAggregateRepository<Trip, Guid>
{
}

