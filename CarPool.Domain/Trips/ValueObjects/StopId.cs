using System;
using CarPool.Domain.Common;

namespace CarPool.Domain.Trips;

public sealed class StopId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private StopId(Guid value)
    {
        Value = value;
    }

    public static StopId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static StopId Create(Guid value)
    {
        return new(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private StopId() { }
}

