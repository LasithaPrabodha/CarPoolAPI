using CarPool.Domain.Common;
using CarPool.Domain.Users.Entities;

namespace CarPool.Domain.Users.Events;

public class DriverExpUpdatedEvent : DomainEvent
{
    public DriverExpUpdatedEvent(DriverProfile driverDetails)
    {
        DriverDetails = driverDetails;
    }

    public DriverProfile DriverDetails { get; }
}

