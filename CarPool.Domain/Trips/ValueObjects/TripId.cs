using System;
using CarPool.Domain.Common;

namespace CarPool.Domain.Trips;

public sealed class TripId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private TripId(Guid value)
    {
        Value = value;
    }

    public static TripId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static TripId Create(Guid value)
    {
        return new(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private TripId() { }
}


