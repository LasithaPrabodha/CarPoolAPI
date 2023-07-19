using System;
using CarPool.Domain.Common;

namespace CarPool.Domain.Users;

public sealed class DriverProfileId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private DriverProfileId(Guid value)
    {
        Value = value;
    }

    public static DriverProfileId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static DriverProfileId Create(Guid value)
    {
        return new(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private DriverProfileId() { }
}

