using CarPool.Domain.Common;

namespace CarPool.Domain.Trips;

public class Stop : Entity<StopId>
{
    public Address Address { get; private set; }

    private Stop() { }

    private Stop(StopId stopId, Address address)
        : base(stopId)
    {
        Address = address;
    }

    public static Stop Create(Address address)
    {
        return new(
            StopId.CreateUnique(),
            address
        );
    }


}

